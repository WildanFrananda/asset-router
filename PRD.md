# PRD: Smart Salary & Asset Allocation Router (Sci-Fi Financial Command Center)

**Versi Dokumen:** 2.4  
**Tanggal:** 4 Agustus 2026  
**Status:** Active — Production Master Architecture  
**Tech Stack:** C# + .NET 10 + Blazor WebAssembly + Interactive Canvas, Pure Vanilla CSS  

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
- **Interaktivitas:** Drag-and-drop node positioning, penyesuaian slider persentase real-time, dan **Dynamic Add/Edit Node**.

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
- **Live IDX Data:** Terhubung secara live ke **Yahoo Finance Public API** (`https://query1.finance.yahoo.com/v8/finance/chart/{tickerSymbol}`) untuk 10 saham teratas BEI (BBCA, BBRI, BMRI, TLKM, ASII, ICBP, UNVR, ADRO, AMRT, GOTO).
- **Visualisasi:** **Spider/Radar Chart** interaktif yang membandingkan metrik saham individual terhadap rata-rata sektornya.

---

### 📈 2.6 Payday History & Discipline Analytics Module (`/analytics`)
- **Fungsi:** Halaman analitik histori gajian bulanan pengguna.
- **Fitur:** Grafik tren akumulasi kekayaan, riwayat kalkulasi historis riil dari local storage/SQLite, dan **Discipline Streak Counter**.

---

### 🎨 2.7 Proof of Discipline Social Card Generator Module (`/card-generator`)
- **Fungsi:** Halaman pembuat kartu infografis estetik (*Spotify Wrapped style*) dari alokasi gajian bulan ini.
- **Live Integration:** Membaca data riil dari susunan Node, Survival Score, dan Streak yang sedang aktif di session user.

---

## 3. Dynamic Data & Un-Hardcoding Master Plan (v2.3)

| Tahap | Area | Rencana Un-Hardcoding | Status |
|---|---|---|---|
| **Tahap 1** | **Node Canvas & Persistence** | Hubungkan `LocalNodeRepository` dengan `LocalStorage` / local memory browser untuk menyimpan posisi X/Y, custom nodes, & target caps. Sediakan modal Add/Delete Node & Reset Layout. | ✅ **COMPLETED** |
| **Tahap 2** | **Stock Screener Dataset** | Hubungkan `HttpStockDataSource` secara live ke Yahoo Finance Public API untuk emiten saham IDX riil (BBCA, BBRI, BMRI, TLKM, ASII, ICBP, UNVR, ADRO, AMRT, GOTO). | ✅ **COMPLETED** |
| **Tahap 3** | **Social Card Integration** | Sambungkan `CardGeneratorPage` langsung ke `CommandCenterService` untuk membaca data riil aktif (node %, survival score, streak). | ✅ **COMPLETED** |
| **Tahap 4** | **Payday History Persistence** | Simpan snapshot otomatis ke SQLite/IndexedDB setiap kali user menyimpan alokasi di Command Center agar grafik `/analytics` ter-update live. | ✅ **COMPLETED** |

---

## 4. Functional Requirements (FR)

| ID | Requirement | Prioritas | Status |
|---|---|---|---|
| FR-1 | Form/Node Input Nominal Gaji & Income Sources | Must Have | ✅ Implemented |
| FR-2 | Interactive Canvas Node Graph (Render nodes & animated flow lines) | Must Have | ✅ Implemented |
| FR-3 | Dynamic Liquid Tank Capacity & Auto-Overflow Valve logic | Must Have | ✅ Implemented |
| FR-4 | Macroeconomic Crisis & Stress Testing Engine (Calculation & Survival Score) | Must Have | ✅ Implemented |
| FR-5 | Parallel Universe Timeline Traveler Projection Engine (Compounding math slider) | Must Have | ✅ Implemented |
| FR-6 | Fundamental Stock Screener with Spider/Radar Chart (`/screener`) | Must Have | ✅ Implemented |
| FR-7 | Payday History & Discipline Analytics Page (`/analytics`) | Must Have | ✅ Implemented |
| FR-8 | Shareable Social Infographic Card Generator (`/card-generator`) | Must Have | ✅ Implemented |
| FR-9 | Dynamic Node Management (Add, Edit Cap, Delete Node) & Local Persistence | Must Have | ✅ Implemented |

---

## 5. Non-Functional Requirements (NFR)

| ID | Requirement |
|---|---|
| NFR-1 | Performan Canvas Rendering: Smooth 60 FPS animation |
| NFR-2 | Pure Vanilla CSS Design System (Bebas dari kebergantungan Tailwind/utility framework) |
| NFR-3 | Separasi Komponen UI 3-File (Markup `.razor`, Code-Behind `.razor.cs`, Style `.razor.css`) |
| NFR-4 | Offline-First (PWA): Berjalan 100% di browser via Blazor WebAssembly & IndexedDB / Local Storage |
| NFR-5 | Bahasa Codebase: 100% Bahasa Inggris |

---

## 6. Arsitektur Software Clean Architecture

```
/AssetRouter
  /Core (Domain Entities, Value Objects, Calculation Engines)
  /Application (Interfaces, DTOs, Services)
  /Infrastructure (Local Persistence, Repositories, Live Stock Data Providers)
  /Presentation
    /Components (TopCrisisBar, BottomTimelineSlider, NodeGraphCanvas, CyberneticCoreReactor)
    /Pages (CommandCenter, StockScreenerPage, AnalyticsPage, CardGeneratorPage)
    /Pages/Layout (MainLayout)
  /wwwroot
    /css (app.css)
```

---

## 7. User Retention Risk Analysis & Future Roadmap Backlog (v3.0 Roadmap)

Analisis komprehensif mengenai **10 potensi hambatan adopsi pengguna** beserta rencana fitur mitigasi untuk iterasi pengembangan versi berikutnya (PR v3.0):

| No | Hambatan Adopsi / Alasan User Berhenti | Dampak & Risiko | Solusi Mitigasi & Roadmap v3.0 |
|---|---|---|---|
| 1 | **Kurva Pembelajaran Tinggi untuk Awam** | User awam bingung dengan istilah *Node Routing/Overflow*. | Sediakan **Interactive Onboarding Wizard & "Simple 50-30-20 View" Toggle**. |
| 2 | **Tidak Ada Eksekusi Otomatis Transaksi Bank** | User harus transfer manual 5x ke tiap rekening/broker. | Sediakan **Deep-Link Auto-Fill & Export Format CSV/Qris Routing**. |
| 3 | **Mobile Canvas View Terlalu Sempit** | Kurang nyaman di layar HP kecil (<400px). | Bangun **Mobile-Optimized Vertical Card Flow View** sebagai fallback. |
| 4 | **Input Manual Mengundang Payday Fatigue** | User malas menginput persentase & gaji tiap bulan. | Integrasikan **WhatsApp / Telegram Payday Bot Reminder** & OCR Slip Gaji. |
| 5 | **Beban Emosional Status "Meltdown"** | User yang keuangan sulit merasa dihakimi/anxious. | Ganti istilah alarm menjadi **Empathetic AI Financial Health Guidance**. |
| 6 | **Waktu Cold Start Blazor WASM Initial Load** | Delay beberapa detik saat download runtime WASM. | Terapkan **Lazy Loading Assemblies & Service Worker Caching**. |
| 7 | **Dianggap Over-Engineered Dibandingkan Sheet** | User merasa spreadsheet lebih cepat & simpel. | Sediakan **Quick Express Allocation Preset (1-Click Route)**. |
| 8 | **Risiko Kehilangan Data di IndexedDB Browser** | Data hilang jika user hapus cache/ganti device. | Tambahkan **Encrypted JSON Export/Import & Optional Cloud Sync**. |
| 9 | **Tidak Ada Notifikasi Pengingat Gajian** | User lupa membuka aplikasi setelah bulan ke-2. | Tambahkan **PWA Web Push Notification API Support**. |
| 10 | **Terbatas Hanya pada Saham IDX (IDR)** | Investor saham US / Crypto tidak terakomodasi. | Tambahkan **Multi-Currency (USD/SGD) & Global Asset Data Provider**. |

---

*Dokumen PRD ini adalah acuan resmi arsitektur produk AssetRouter v2.4.*