# Smart.Converter レビュー対応 実装プラン（GPT-5.5）

`レビュー結果GPT-5.5.md` の指摘に対する実装プランです。各項目はチェックリスト化し、上から1つずつ独立して進められるよう順序付けしています。

**本プランは「パフォーマンス優先」方針で構成しています。** 性能に寄与しない防御的な例外チェック（公開 API の null ガード追加、変換演算子の例外挙動テスト）は対象から除外しました。一方、`try/catch` を事前チェックに置き換えて JIT 最適化（インライン化）を改善する変更は積極的に採用します。

## 確定方針（着手前に合意済み）

| テーマ | 方針 |
|---|---|
| 範囲外変換の例外（`try/catch` → 事前チェック） | **採用・優先実施**。`try/catch` はホットパスの JIT 最適化を阻害するため、値の事前チェックに置換。挙動（範囲外は `default`/`null`）は不変 |
| コレクション変換の割り当て・列挙削減 | **採用**（性能）。`__★パフォーマンス改善候補.md` を正典として参照 |
| カルチャ依存（数値・decimal・日時の文字列⇔値） | **`InvariantCulture` に統一**（決定論化・確定挙動変更）＋カルチャ切替テスト＋ドキュメント明記 |
| 負例キャッシュ診断 | 追加する（非破壊・加算のみ） |
| 数値の unchecked 折り返し | **維持**（性能優先）。仕様として明記＋境界値テスト。checked/TryConvert は追加しない |
| `SetFactories` 実行時差し替えと並行変換 | **現状維持**（同期強化なし）＋並行実行テストのみ追加 |
| 変換演算子の blanket catch（`ConversionOperatorConverterFactory`） | **対象外**（現状維持・テストも追加しない）。例外チェック系のため除外 |
| 公開 API の null ガード | **対象外**。例外チェック系のため除外 |

## 全体の進め方

- **計測ファースト。** 性能変更の前後で BenchmarkDotNet により効果を確認する（フェーズ1で基盤整備）。
- **1フェーズ＝1まとまりの変更**として、着手→実装→ビルド→テスト（→計測）の順で進める。
- 各フェーズ完了時に必ず以下を実行し、**警告ゼロ**・**全テスト成功**を確認してからコミットする。
  - ビルド: `dotnet build Smart.Converter/Smart.Converter.csproj -c Release`
  - テスト: `dotnet test Smart.Converter.Tests/Smart.Converter.Tests.csproj`
- コーディング規約（`AGENTS.md` / `.editorconfig`）厳守。メンバ変数に `_` プレフィックスを付けない。
- ビルド構成: ライブラリは `net10.0/net9.0/net8.0`、テストは `net10.0`。`AnalysisMode=All`・`AnalysisLevel=latest`・CA1305 有効・`WarningsAsErrors=nullable`。
- **警告抑制（`#pragma warning disable` 等）を新規追加する必要が生じた場合は、適用前に必ず確認を取る**（AGENTS.md）。既存抑制が不要になった場合は削除する。

---

## フェーズ 5: 負例キャッシュの診断情報追加（非破壊・加算のみ）

**対象:** `Smart.Converter/Converter/TypePairHashArray.cs` / `Smart.Converter/Converter/ObjectConverter.cs`
**レビュー対応:** 「中: 変換不可もキャッシュするためキャッシュが肥大化」「低: 診断情報の内訳が少ない」
**狙い:** 正例/負例の内訳を可視化し、未知型ペアが多い環境でのキャッシュ肥大の原因分析を容易にする。挙動（負例もキャッシュ）は維持。

### 実装詳細

- `TypePairHashArray` に負例件数（`Converter is null` のノード数）を管理。
  - フィールド `negativeCount` を追加。非リサイズ分岐で追加ノードが負例なら `negativeCount++`。リサイズ／`Clear` で `CalculateNegativeCount(Node[])` により再計算／リセット。
  - `DiagnosticsInfo` に `NegativeCount`（必要なら `PositiveCount = Count - NegativeCount`）を追加。既存 `Width/Depth/Count` は維持しコンストラクタを拡張（内部生成のみ）。
- `ObjectConverter.DiagnosticsInfo` に `NegativeCacheCount` を追加し `Diagnostics` で反映。既存プロパティは維持。
- 併せて `AddNode` の `CalculateHash` 二重呼び出し（`__★パフォーマンス改善候補.md` #6）を index 変数化（軽微最適化）。

### チェックリスト

- [x] `TypePairHashArray` に `negativeCount` 管理を追加
- [x] `CalculateNegativeCount(Node[])` を追加し、リサイズ/Clear で整合
- [x] `TypePairHashArray.DiagnosticsInfo` に `NegativeCount`（＋任意 `PositiveCount`）を追加
- [x] `ObjectConverter.DiagnosticsInfo` に `NegativeCacheCount` を追加
- [x] `AddNode` の `CalculateHash` 二重呼び出しを index 変数化
- [x] テスト追加: 未知型ペアを多数 `CanConvert` し `NegativeCacheCount` が増加することを検証（レビュー テスト提案 5）
- [x] ビルド警告ゼロ（フェーズ5分）・全テスト成功 ※`ToHashSet.cs` の IDE0028 は別途要相談

> 補足: 「負例キャッシュ無効化オプション」「上限付きキャッシュ」は確定方針（保守的・挙動維持）に沿って今回は診断追加に留める。

---

## フェーズ 6: unchecked 数値キャストの仕様明記＋境界テスト

**対象:** `NumericCastConverterFactory.cs` / `EnumConverterFactory.cs` / `README.md`
**レビュー対応:** 「中: 数値キャストが unchecked で桁あふれを丸める」
**狙い:** 性能優先のため unchecked キャストを維持し、`256 -> byte == 0` 等の折り返しを**意図された仕様**として明記・固定する。checked/TryConvert は追加しない。

### 実装詳細

- README に「数値間のキャスト変換はキャスト互換であり、範囲外は C# の unchecked キャストと同様に折り返す（wrap-around）」旨を明記。
- `NumericCastConverterFactory` の境界・オーバーフロー・符号変換テストを追加（レビュー テスト提案 3）。
  - `(int)256 -> byte == 0`、`(int)-1 -> byte == 255`、`(int)-1 -> uint == uint.MaxValue`、`(long)0x1_0000_0000 -> int == 0` など。

### チェックリスト

- [ ] README に unchecked 折り返し仕様を明記
- [ ] `NumericCastConverterFactory` 境界値テスト（範囲外の折り返し）
- [ ] 負数→ unsigned 変換テスト
- [ ] long→int 等の上位ビット切り捨てテスト
- [ ] （該当すれば）Enum 数値キャストの境界テスト
- [ ] ビルド警告ゼロ・全テスト成功

---

## フェーズ 7: `SetFactories` / `Reset` と変換の並行実行テスト（テストのみ）

**対象:** 新規 `Smart.Converter.Tests/Converter/ObjectConverterConcurrencyTest.cs`
**レビュー対応:** 「高: ファクトリ差し替えと変換実行の並行利用」→ 方針は現状維持。並行実行で破綻しないことの回帰テストを追加。
**狙い:** 実装は変更しないが、並行 `Convert` 中に `SetFactories`/`Reset` を呼んでも `TypePairHashArray` のロックにより致命的破綻（例外・デッドロック）が起きないことを担保する。

### 実装詳細

- 単一 `ObjectConverter` を複数スレッドで同時利用。
  - 群A: `Convert<int>("123")` / `CanConvert(typeof(string), typeof(int))` をループ。
  - 群B: 一定間隔で `Reset()`（および `SetFactories(DefaultObjectFactories.Create())`）。
- 期待: 例外なし／デッドロックなし。`Reset` 版は妥当値（`123`）を期待、`SetFactories` 差し替え版は「致命的破綻が無いこと」までを担保（値の厳密一致は問わない）。
- 反復回数を固定し、`Assert` は「記録された例外が無いこと」を中心に組む。
- （任意）`SetFactories` の想定用途（初期化時。共有インスタンスでの実行時差し替えは並行変換と組み合わせると可視性非保証）を XML コメント／README に明記。

### チェックリスト

- [ ] 並行 `Convert` × `Reset` テスト（例外・デッドロックなし）
- [ ] 並行 `Convert` × `SetFactories` テスト（致命的破綻なし）
- [ ] （任意）`SetFactories` の想定用途を XML コメント／README に明記
- [ ] ビルド警告ゼロ・全テスト成功

---

## レビュー「テスト面の改善提案」との対応表

| レビュー提案 | 対応フェーズ |
|---|---|
| 1. `SetFactories`/`Reset` と `Convert` の並行実行 | フェーズ7 |
| 2. `CurrentCulture` を ja-JP/en-US/fr-FR に切替えた文字列パース | フェーズ4g |
| 3. `NumericCastConverterFactory` の境界・オーバーフロー・符号変換 | フェーズ6 |
| 4. 範囲外変換の事前チェック化後の境界値 | フェーズ2e |
| 5. 大量の未知型ペアでのキャッシュ増加確認 | フェーズ5 |
| 6. コレクション変換の null 要素ケース | フェーズ3 |

## 完了の定義（Definition of Done）

- [ ] フェーズ1〜7を実装
- [ ] `dotnet build -c Release` で**警告ゼロ**（全 TFM）
- [ ] `dotnet test` で**全テスト成功**
- [ ] 性能変更（フェーズ2・3）はベンチで改善・非劣化を確認
- [ ] 挙動変更（カルチャ Invariant 統一）を README に明記
- [ ] 新規の警告抑制を追加していない（やむを得ず必要な場合は事前合意済み）
- [ ] 不要になった `#pragma warning disable CA1305` を整理済み
