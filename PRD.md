# PRD: Smart Salary & Asset Allocation Router (Sci-Fi Financial Command Center)

**Versi Dokumen:** 2.1  
**Tanggal:** 3 Agustus 2026  
**Status:** Active — Production Master Architecture  
**Tech Stack:** C# + .NET 10 + Blazor WebAssembly + Interactive Canvas & Pure Vanilla CSS  

---

## 1. Visi Produk & Konsep Utama

**AssetRouter** adalah platform **Sci-Fi Financial Command Center** yang membalik paradigma pencatatan keuangan tradisional (reaktif: belanja dulu, sisa baru diinvestasikan) menjadi **alokasi otomatis proaktif & futuristik berbasis Node Engine** begitu gaji/pendapatan diterima.

Analogi utama: Aplikasi ini berfungsi seperti **router jaringan pintar**, tetapi merutekan **rupiah** ke berbagai "tangki/bucket" instrumen investasi melalui **Node-Graph Interaktif** dengan logika *Fluid Dynamic Overflow*, *Macroeconomic Stress Testing*, *Parallel Universe Projections*, serta modul pendukung terintegrasi.

---

## 2. Modul Fitur Utama Platform (4 Core Pillars + 3 Dedicated Modules)

---

### 🕸️ 2.1 Node-Based Money Flow Canvas (Visual Arus Uang Interaktif)
- **Visual:** Antarmuka utama berbasis **Interactive Node Graph Canvas** (seperti Unreal Engine Blueprints / Blender Shader Nodes / Figma Canvas).
- **Komponen Node:**
  - **Income Node (Input):** Node sumber rupiah utama (`[Gaji / Income In]`).
  - **Routing Pipes (Garis Hubung):** Pipa dengan efek animasi partikel arus uang (*animated particle flow*).
  - **Bucket Nodes (Tujuan):** Node tujuan alokasi (`[Dana Darurat]`, `[Emas]`, `[Saham Fundamental]`, `[Kripto/Risk]`, `[Kebutuhan]`).
- **Interaktivitas:** Drag-and-drop node positioning, penyesuaian slider persentase real-time.

---

### 🌊 2.2 Dynamic Liquid Tank & Overflow Pipeline Engine (Alokasi Cair Otomatis)
- **Konsep:** Setiap Node Bucket direpresentasikan sebagai **Tangki Cairan** dengan batas kapasitas nominal (*Cap Target*).
- **Logika Overflow:**
  - Tangki **Dana Darurat** dipasang *target cap* sebesar Rp 30.000.000 (6x pengeluaran).
  - Ketika Tangki Dana Darurat mencapai 100% (penuh), **katup pipa meluap (*overflow valve*)** secara otomatis terbuka dengan efek animasi menyala, menyalurkan sisa rupiah ke **Tangki Saham / Emas** tanpa alokasi manual.

---

### 🛡️ 2.3 Macroeconomic Crisis & Stress Tester (Simulasi Ketahanan Portofolio)
- **Panel Control:** Terletak di bagian atas Canvas (`Top Control Bar`).
- **Mode Simulasi:** Menguji ketahanan alokasi pengguna terhadap 3 skenario makroekonomi:
  1. 📉 **Resesi & Inflasi Tinggi** (Inflasi 8% + Pasar Saham drop 30%).
  2. 🏥 **Krisis PHK / Kehilangan Income** (Pendapatan terhenti 100% selama 6–12 bulan).
  3. 🚀 **Bull Market / Economic Expansion** (Pertumbuhan ekonomi positif).
- **Output Analysis:** Survival Rate Score (0–100), Runway Months, dan AI Rebalancing Advice.

---

### 🔮 2.4 "Parallel Universe" Financial Timeline Traveler (Proyeksi Masa Depan Multi-Mata)
- **Panel Control:** Terletak di bagian bawah Canvas (`Bottom Timeline Bar`).
- **Interaktivitas Slider:** Pengguna dapat menggeser **Timeline Slider (Tahun 2026 s.d. 2045)** untuk melihat pertumbuhan dana secara animasi instan pada 3 alam semesta finansial (Universe A: Status Quo, Universe B: Balanced Router, Universe C: Aggressive FIRE Router).

---

### 🔍 2.5 Fundamental Stock Screener & Asset Radar Module (`/screener`)
- **Fungsi:** Halaman analisis mendalam kandidat saham yang lolos kriteria fundamental (PER, DER, ROE, Dividend Yield).
- **Visualisasi:** **Spider/Radar Chart** interaktif yang membandingkan metrik saham individual terhadap rata-rata sektornya.

---

### 📈 2.6 Payday History & Discipline Analytics Module (`/analytics`)
- **Fungsi:** Halaman analitik histori gajian bulanan pengguna.
- **Fitur:** Grafik tren akumulasi kekayaan, riwayat kalkulasi historis, dan **Discipline Streak Counter** (misal: "3 Bulan Berturut-turut Disiplin Alokasi").

---

### 🎨 2.7 Proof of Discipline Social Card Generator Module (`/card-generator`)
- **Fungsi:** Halaman pembuat kartu infografis estetik (*Spotify Wrapped style*) dari alokasi gajian bulan ini.
- **Fitur:** Menyembunyikan nominal asli (hanya persentase & visual chart), opsi kustomisasi warna tema, dan tombol export/copy image siap share ke Instagram/LinkedIn.

---

## 3. User Flow Utama

```
1. Top Navbar Navigation:
   - [/] Command Center Canvas
   - [/screener] Stock Screener & Radar
   - [/analytics] Payday History & Discipline Analytics
   - [/card-generator] Proof of Discipline Social Card

2. Core Flow di Command Center:
   - Input Gaji -> Canvas Partikel Flow -> Liquid Tank Cap Evaluation -> Overflow Valve Trigger -> Crisis Stress Test -> Timeline Slider Projections.
```

---

## 4. Functional Requirements (FR)

| ID | Requirement | Prioritas |
|---|---|---|
| FR-1 | Form/Node Input Nominal Gaji & Income Sources | Must Have |
| FR-2 | Interactive Canvas Node Graph (Render nodes & animated flow lines) | Must Have |
| FR-3 | Dynamic Liquid Tank Capacity & Auto-Overflow Valve logic | Must Have |
| FR-4 | Macroeconomic Crisis & Stress Testing Engine (Calculation & Survival Score) | Must Have |
| FR-5 | Parallel Universe Timeline Traveler Projection Engine (Compounding math slider) | Must Have |
| FR-6 | Fundamental Stock Screener with Spider/Radar Chart (`/screener`) | Must Have |
| FR-7 | Payday History & Discipline Analytics Page (`/analytics`) | Must Have |
| FR-8 | Shareable Social Infographic Card Generator (`/card-generator`) | Must Have |

---

## 5. Non-Functional Requirements (NFR)

| ID | Requirement |
|---|---|
| NFR-1 | Performan Canvas Rendering: Smooth 60 FPS animation |
| NFR-2 | Pure Vanilla CSS Design System (Bebas dari kebergantungan Tailwind/utility framework) |
| NFR-3 | Separasi Komponen UI 3-File (Markup `.razor`, Code-Behind `.razor.cs`, Style `.razor.css`) |
| NFR-4 | Offline-First (PWA): Berjalan 100% di browser via Blazor WebAssembly |
| NFR-5 | Bahasa Codebase: 100% Bahasa Inggris |

---

## 6. Arsitektur Software Clean Architecture

```
/AssetRouter
  /Core (Domain Entities, Value Objects, Calculation Engines)
  /Application (Interfaces, DTOs, Services)
  /Infrastructure (Local Persistence, Mock Data Providers)
  /Presentation
    /Components (TopCrisisBar, BottomTimelineSlider, NodeGraphCanvas, dll.)
    /Pages (CommandCenter, StockScreenerPage, AnalyticsPage, CardGeneratorPage)
    /Pages/Layout (MainLayout)
```

---

*Dokumen PRD ini adalah acuan resmi arsitektur produk AssetRouter v2.1.*