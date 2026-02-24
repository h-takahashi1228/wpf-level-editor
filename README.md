# 🎨 WPF Level Editor

![Level Editor Thumbnail](Screenshots/thumbnail.png)

WPF を用いて制作した **3D グリッドベースのレベルエディタ** です。  
プランナーがレベルを構築し、そのデータを Houdini → Unreal Engine へ連携することを想定して設計しました。

---

## 📌 概要

- **制作期間:** 約1ヶ月  
- **担当:** 個人制作  
- **対象:** Windows（WPF + .NET）  
- **技術スタック:** WPF, C#, JSON, Python（Houdini用CSV変換スクリプト）

---

## 🎯 開発の目的

- WPF を用いた 3D ツール開発の基礎習得
- **WPF → Houdini → UE を想定したデータ連携設計の理解**
- レベル編集データを外部ツールで活用可能な形式で出力する設計
- Houdiniの基本的な使用方法と簡単なプロシージャル生成の習得

---

## 🛠 機能

### ■ レベル編集
- 3D Viewport 上でのブロック・オブジェクト配置 / 削除 / 編集
- 整数グリッドベースの座標管理
- カメラ移動・回転操作
- Undo / Redo 機能
- レベルデータの Import / Export（JSON）

### ■ データ連携
- JSON 形式でレベルデータを出力
- Python スクリプトで Houdini 用 CSV へ変換
- Houdini 上で CSV を読み込み、同等の 3D モデルを自動生成可能

### ■ 想定パイプライン
WPF（レベル作成）  
↓ JSON Export  
Python（CSV 変換）  
↓  
Houdini（モデル生成）  
↓  
Unreal Engine（ゲーム利用想定）

---

## 🖼 デモ
