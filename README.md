# ☯ 靈籤小築 OracleLot

## 專案簡介

靈籤小築（OracleLot）是一套以 Visual C# Windows Forms 開發之桌面求籤應用程式，提供使用者透過數位化方式體驗傳統求籤文化。

本系統採用單機版架構設計，不需資料庫即可執行，所有籤詩內容皆內建於系統中，具備快速部署、操作簡單與資源需求低等特色。

使用者可透過系統進行求籤、解籤與歷史紀錄查詢，並搭配動畫效果提升互動體驗。

---

## 專案特色

### 🎋 傳統文化數位化

將傳統寺廟求籤流程轉換為數位互動模式。

### 🎲 隨機求籤機制

透過亂數演算法模擬實際抽籤體驗。

### 📖 智慧解籤

每支籤提供：

* 籤詩內容
* 籤運等級
* 整體運勢解析
* 事業運勢
* 感情運勢
* 學業運勢
* 建議事項

### 📜 歷史紀錄

自動記錄本次執行期間的求籤結果。

### ✨ 動畫互動效果

模擬籤筒搖動過程：

* 抽籤動畫
* 卡片閃爍效果
* 結果呈現動畫

### 💻 無資料庫設計

無需安裝：

* SQL Server
* MySQL
* PostgreSQL
* SQLite

下載即可執行。

---

## 系統架構

OracleLot
│
├─ UI Layer
│ ├─ MainForm
│ ├─ Result Card
│ └─ History Panel
│
├─ Business Logic
│ ├─ Random Draw Engine
│ ├─ Fortune Service
│ └─ Animation Controller
│
└─ Data Layer
├─ Fortune Objects
└─ In-Memory Collection

---

## 開發技術

### 開發工具

* Microsoft Visual Studio 2022

### 程式語言

* C#

### 開發框架

* Windows Forms
* .NET Framework 4.7.2

### UI 技術

* GDI+
* Gradient Background
* Custom Rounded Panel
* Double Buffer Rendering

---

## 系統畫面功能

### 首頁

顯示：

* 系統標題
* 求籤按鈕
* 清除紀錄按鈕

---

### 求籤結果

顯示：

* 籤號
* 籤運等級
* 籤詩
* 解籤內容
* 事業分析
* 感情分析
* 學業分析
* 行動建議

---

### 歷史紀錄

顯示本次執行期間：

* 求籤時間
* 籤號
* 籤運等級

---

## 執行需求

### 作業系統

* Windows 10
* Windows 11

### 執行環境

* .NET Framework 4.7.2 以上

---

## 建置方式

1. 開啟 OracleLotApp.sln

2. 切換至 Release 模式

3. 建置方案

4. 於以下目錄取得執行檔：

bin\Release\OracleLotApp.exe

---

## 專案展示目的

本專案主要作為：

* Windows Forms UI 設計展示
* C# 桌面應用程式開發展示
* 物件導向程式設計實作展示
* 傳統文化數位轉型案例展示

---

## 作者資訊

姓名：陳德恩

學校：國立雲林科技大學 資訊管理系

技術領域：

* C#
* Java
* PHP
* ASP.NET Core
* MySQL
* Windows Server
* Linux
* 資訊安全

---

© 2026 OracleLot 靈籤小築
