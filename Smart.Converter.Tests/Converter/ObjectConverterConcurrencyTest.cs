namespace Smart.Converter;

using Smart.Converter.Converters;

public sealed class ObjectConverterConcurrencyTest
{
    [Fact]
    public void ConvertConcurrentlyWithResetDoesNotBreak()
    {
        RunConcurrently(static c => c.Reset(), assertValue: true);
    }

    [Fact]
    public void ConvertConcurrentlyWithSetFactoriesDoesNotBreak()
    {
        RunConcurrently(static c => c.SetFactories(DefaultObjectFactories.Create()), assertValue: false);
    }

    private static void RunConcurrently(Action<ObjectConverter> mutate, bool assertValue)
    {
        var converter = new ObjectConverter(DefaultObjectFactories.Create());
        const int workerCount = 4;
        const int iterations = 20_000;
        using var workersDone = new CountdownEvent(workerCount);

        var workers = new Task[workerCount];
        for (var i = 0; i < workerCount; i++)
        {
            workers[i] = Task.Run(() =>
            {
                try
                {
                    for (var n = 0; n < iterations; n++)
                    {
                        var value = converter.Convert<int>("123");
                        if (assertValue && (value != 123))
                        {
                            throw new InvalidOperationException($"Unexpected value: {value}");
                        }

                        _ = converter.CanConvert(typeof(string), typeof(int));
                    }
                }
                finally
                {
                    // ReSharper disable once AccessToDisposedClosure
                    workersDone.Signal();
                }
            });
        }

        var mutator = Task.Run(() =>
        {
            // ReSharper disable once AccessToDisposedClosure
            while (!workersDone.IsSet)
            {
                mutate(converter);
                Thread.Yield();
            }
        });

        var all = workers.Append(mutator).ToArray();
        var completed = Task.WaitAll(all, TimeSpan.FromSeconds(30));

        Assert.True(completed, "Concurrent execution did not finish in time (possible deadlock).");
    }
}
