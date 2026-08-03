# PRD: Smart Salary & Asset Allocation Router (Sci-Fi Financial Command Center)

**Versi Dokumen:** 2.0  
**Tanggal:** 3 Agustus 2026  
**Status:** Active — Production Master Architecture  
**Tech Stack:** C# + .NET 10 + Blazor WebAssembly + Interactive Canvas/SVG Graphics  

---

## 1. Visi Produk & Konsep Utama

**AssetRouter** adalah platform **Sci-Fi Financial Command Center** yang membalik paradigma pencatatan keuangan tradisional (reaktif: belanja dulu, sisa baru diinvestasikan) menjadi **alokasi otomatis proaktif & futuristik berbasis Node Engine** begitu gaji/pendapatan diterima.

Analogi utama: Aplikasi ini berfungsi seperti **router jaringan pintar**, tetapi merutekan **rupiah** ke berbagai "tangki/bucket" instrumen investasi melalui **Node-Graph Interaktif** dengan logika *Fluid Dynamic Overflow*, *Macroeconomic Stress Testing*, dan *Parallel Universe Projections*.

---

## 2. Fitur Utama Masterpiece (4 Core Futuristic Pillars)

---

### 🕸️ 2.1 Node-Based Money Flow Canvas (Visual Arus Uang Interaktif)
- **Visual:** Antarmuka utama berbasis **Interactive Node Graph Canvas** (seperti Unreal Engine Blueprints / Blender Shader Nodes / Figma Canvas).
- **Komponen Node:**
  - **Income Node (Input):** Node sumber rupiah utama (`[Gaji / Income In]`).
  - **Routing Pipes (Garis Hubung):** Kabel/pipa yang menghubungkan Income Node ke Bucket Nodes dengan efek animasi partikel arus uang (*animated particle flow*).
  - **Bucket Nodes (Tujuan):** Node tujuan alokasi (`[Dana Darurat]`, `[Emas]`, `[Saham Fundamental]`, `[Kripto/Risk]`, `[Kebutuhan]`).
- **Interaktivitas:** User dapat menghubungkan/memutus pipa (*drag-and-drop link*), mengatur persentase alokasi per node secara langsung, serta melihat animasi arus uang mengalir secara real-time.

---

### 🌊 2.2 Dynamic Liquid Tank & Overflow Pipeline Engine (Alokasi Cair Otomatis)
- **Konsep:** Setiap Node Bucket direpresentasikan sebagai **Tangki Cairan** dengan batas kapasitas nominal (*Cap Target*).
- **Logika Overflow:**
  - *Contoh:* Tangki **Dana Darurat** dipasang *target cap* sebesar Rp 30.000.000 (6x pengeluaran).
  - Ketika Tangki Dana Darurat mencapai 100% (penuh), **katup pipa meluap (*overflow valve*)** secara otomatis terbuka dengan efek animasi menyala.
  - Sisa rupiah yang masuk ke alokasi Dana Darurat secara otomatis dialirkan (*waterfall overflow*) ke **Tangki Saham / Emas** tanpa membutuhkan perubahan manual dari user.

---

### 🛡️ 2.3 Macroeconomic Crisis & Stress Tester (Simulasi Ketahanan Portofolio)
- **Panel Control:** Terletak di bagian atas Canvas (`Top Control Bar`).
- **Mode Simulasi:** Menampilkan tombol **"Run Crisis Simulator"** yang menguji ketahanan alokasi user terhadap 3 skenario makroekonomi:
  1. 📉 **Resesi & Stagflasi:** Inflasi 8% + Pasar Saham drop 30%.
  2. 🏥 **Skenario PHK / Emergency:** Pendapatan terhenti total selama 6–12 bulan.
  3. 🚀 **Bull Market / Tech Boom:** Pertumbuhan instrumen investasi maksimal.
- **Output Analysis:** 
  - **Survival Rate Score (0–100):** Indikator daya tahan keuangan.
  - **Red Alert Crisis Visual:** Canvas berubah warna menjadi mode siaga krisis.
  - **Actionable AI Rebalancing Recommendation:** Saran otomatis penyesuaian katup pipa (misal: *"Pindahkan 5% dari Kripto ke Emas untuk mengamankan runway 12 bulan"*).

---

### 🔮 2.4 "Parallel Universe" Financial Timeline Traveler (Proyeksi Masa Depan Multi-Mata)
- **Panel Control:** Terletak di bagian bawah Canvas (`Bottom Timeline Bar`).
- **Interaktivitas Slider:** Pengguna dapat menggeser **Timeline Slider (Tahun 2026 s.d. 2045)** untuk melihat pertumbuhan dana secara animasi instan pada node-node tangki canvas.
- **Perbandingan 3 Alam Semesta Keuangan:**
  - **Universe A (Status Quo / Kebiasaan Lama):** Belanja 80%, investasi sisa 5%.
  - **Universe B (Balanced Router):** Alokasi seimbang 50/30/20.
  - **Universe C (Aggressive FIRE Router):** Alokasi disiplin (40% Investasi Pertumbuhan + Overflow Pipeline).
- **Efek Visual:** Menggeser slider akan memperbesar/memperkecil volume tangki uang pada canvas secara real-time memperlihatkan proyeksi kekayaan puluhan tahun ke depan.

---

## 3. User Flow Utama (Unified Experience)

```
1. User membuka AssetRouter → Masuk ke Sci-Fi Command Center Canvas
2. User menginput nominal gaji bulanan pada [Income Node]
3. Canvas merender partikel arus uang yang mengalir melalui [Routing Pipes] ke [Bucket Nodes]
4. Sistem mengecek batas kapasitas tangki:
   - Jika Tangki Dana Darurat < 100% → Alokasi masuk ke Dana Darurat.
   - Jika Tangki Dana Darurat = 100% → Katup Overflow menyala & merutekan kelebihan uang ke Tangki Saham/Emas.
5. User menekan "Run Crisis Simulator" di Top Bar → Menguji daya tahan portofolio & melihat Survival Score.
6. User menggeser "Timeline Slider" di Bottom Bar (2026 - 2045) → Melihat animasi pertumbuhan tangki kekayaan di masa depan pada 3 Universe berbeda.
7. User menyimpan/mengabadikan konfigurasi alokasi bulan ini.
```

---

## 4. Functional Requirements (FR)

| ID | Requirement | Prioritas |
|---|---|---|
| FR-1 | Form/Node Input Nominal Gaji & Income Sources | Must Have |
| FR-2 | Interactive Canvas Node Graph (Render nodes & animated flow lines) | Must Have |
| FR-3 | Kalkulasi alokasi per bucket secara instan berbasis persentase & nominal | Must Have |
| FR-4 | Dynamic Liquid Tank Capacity & Auto-Overflow Valve logic | Must Have |
| FR-5 | Macroeconomic Crisis & Stress Testing Engine (Calculation & Survival Score) | Must Have |
| FR-6 | Parallel Universe Timeline Traveler Projection Engine (Compounding math slider) | Must Have |
| FR-7 | Filter Kandidat Saham Fundamental (Metrik PER, DER, ROE) pada Node Saham | Should Have |
| FR-8 | Histori Alokasi Bulanan & Persistence Storage (IndexedDB/LocalStorage) | Must Have |
| FR-9 | Export Shareable Infographic Card ("Proof of Discipline") | Could Have |

---

## 5. Non-Functional Requirements (NFR)

| ID | Requirement |
|---|---|
| NFR-1 | Performan Canvas Rendering: Smooth 60 FPS animation saat menggeser node/slider |
| NFR-2 | Perhitungan Real-Time: Kalkulasi alokasi & compounding projection selesai dalam < 100ms |
| NFR-3 | Offline-First (PWA): Berjalan 100% di browser via Blazor WebAssembly tanpa dependency backend mandatory |
| NFR-4 | Responsif: Dapat diakses dengan nyaman di Desktop Canvas maupun Mobile Viewport |
| NFR-5 | Clean Architecture: Terpisah tegas antara Domain Engine, Application Use Cases, dan Canvas Presentation Components |

---

## 6. Arsitektur Software Clean Architecture

```
/AssetRouter
  /Core (Domain Entities, Value Objects, Calculation Engines)
    - AllocationNode.cs
    - OverflowValve.cs
    - StressTestEngine.cs
    - TimelineProjectionEngine.cs
  /Application (Interfaces, DTOs, Use Cases)
    - CalculateAllocationUseCase.cs
    - RunStressTestUseCase.cs
  /Infrastructure (Local Persistence, Mock Data Providers)
    - LocalStorageRepository.cs
  /Presentation (Blazor WASM Components & Canvas UI)
    - CommandCenterCanvas.razor
    - NodeGraphComponent.razor
    - TopCrisisBar.razor
    - BottomTimelineSlider.razor
```

---

## 7. Legal Disclaimer (Wajib Tampil di UI)

> "Aplikasi ini adalah alat bantu simulasi visual dan alokasi aset untuk tujuan edukasi serta perencanaan keuangan pribadi. Bukan merupakan rekomendasi investasi atau nasihat keuangan resmi."

---

*Dokumen PRD ini adalah acuan resmi arsitektur produk AssetRouter v2.0.*