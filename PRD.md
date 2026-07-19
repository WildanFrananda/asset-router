# PRD: Smart Salary & Asset Allocation Router

**Versi Dokumen:** 1.0
**Tanggal:** 17 Juli 2026
**Status:** Draft — Hackathon Scope
**Tech Stack:** C# + .NET 10 + Blazor WebAssembly

---

## 1. Latar Belakang & Masalah

Aplikasi pencatat keuangan (expense tracker) sudah jenuh di pasar — semuanya sebatas mencatat "uang masuk, uang keluar". Yang belum banyak tersedia adalah alat yang membantu user membuat **keputusan alokasi** begitu gaji diterima, sebelum uang tersebut habis dibelanjakan atau mengendap tanpa arah di rekening tabungan.

Kebiasaan umum: gaji masuk → dibelanjakan reaktif → sisa (jika ada) baru dipikirkan untuk investasi. Aplikasi ini membalik urutan tersebut: **alokasi dulu, di depan, otomatis, berbasis aturan yang sudah ditentukan user sendiri.**

## 2. Tujuan Produk

### 2.1 Tujuan Utama (Hackathon Goal)
Satu antarmuka utama di mana user:
1. Memasukkan nominal gaji bulanan.
2. Sistem secara instan memecah nominal tersebut ke beberapa keranjang instrumen investasi sesuai aturan persentase yang telah di-set.
3. Hasil alokasi ditampilkan dalam bentuk grafik (chart) secara real-time/seketika.

### 2.2 Non-Goals (Di Luar Scope Hackathon)
- Tidak melakukan eksekusi transaksi riil ke exchange, sekuritas, atau bank (bukan aplikasi trading).
- Tidak terintegrasi dengan API bank/payroll secara live untuk fase ini (input manual).
- Tidak menyediakan rekomendasi saham individual secara real-time dari market data live (fase awal menggunakan data/kriteria statis atau mock).
- Tidak ada multi-user/multi-tenant auth kompleks — cukup single-user profile untuk demo.

## 3. Target Pengguna

| Persona | Deskripsi | Kebutuhan |
|---|---|---|
| Karyawan muda produktif | Gaji bulanan tetap, ingin mulai berinvestasi tapi bingung porsi alokasi | Aturan otomatis, tidak perlu hitung manual tiap bulan |
| Freelancer/pekerja dengan income tidak tetap | Pendapatan bervariasi tiap "gajian" | Sistem tetap bisa hitung ulang alokasi dari nominal berapapun |

## 4. Konsep Inti: "Router" Alokasi Aset

Analogi: aplikasi ini seperti **router jaringan**, tapi alih-alih merutekan paket data, ia merutekan **rupiah** ke beberapa "jalur" instrumen berdasarkan tabel aturan (rule table) yang bisa dikonfigurasi user.

### 4.1 Keranjang Instrumen (Default Buckets)

| Bucket | Deskripsi | Default % | Kriteria/Catatan |
|---|---|---|---|
| Dana Darurat | Kas/tabungan likuid | 20% | Prioritas hingga target 6x pengeluaran bulanan tercapai |
| Emas | Logam mulia (fisik/digital) | 15% | Instrumen lindung nilai (hedging) |
| Saham (Fundamental) | Saham dengan filter metrik tertentu | 30% | Difilter berdasarkan metrik fundamental (lihat 4.2) |
| Kripto | Aset kripto (BTC/ETH mayoritas) | 15% | Alokasi risiko tinggi, dibatasi persentase kecil |
| Kebutuhan Hidup/Sisa | Sisa untuk pengeluaran rutin | 20% | Bukan instrumen investasi, tapi tetap direpresentasikan di chart agar total 100% masuk akal |

> Catatan: Angka default di atas adalah *starting point* yang **wajib bisa diubah user** di halaman pengaturan (lihat FR-2).

### 4.2 Kriteria Metrik Fundamental untuk Saham (Fase Hackathon)

Karena tidak ada integrasi data market live di scope hackathon, gunakan **rule-based scoring dari dataset statis/mock** dengan metrik umum:

- **P/E Ratio** (Price to Earnings) — di bawah rata-rata sektor
- **DER** (Debt to Equity Ratio) — di bawah ambang batas tertentu (misal < 1)
- **ROE** (Return on Equity) — di atas ambang batas tertentu (misal > 15%)
- **Dividend Yield** — opsional sebagai bobot tambahan

Output dari fitur ini bukan "beli saham X", melainkan **daftar kandidat saham yang lolos filter**, sebagai referensi — bukan rekomendasi finansial yang mengikat (lihat Disclaimer di §9).

## 5. User Flow (Hackathon Demo Flow)

```
1. User membuka aplikasi → Landing/Dashboard
2. User klik "Input Gaji Baru"
3. User memasukkan nominal (misal: Rp 10.000.000)
4. User (opsional) mengecek/mengubah aturan alokasi persentase
5. User klik "Hitung & Alokasikan"
6. Sistem menghitung breakdown per bucket
7. Sistem menampilkan:
   - Pie chart / Donut chart alokasi
   - Tabel breakdown nominal per bucket
   - (Untuk bucket Saham) daftar kandidat saham yang lolos filter fundamental
8. User bisa simpan histori alokasi bulan ini
9. User bisa lihat riwayat alokasi bulan-bulan sebelumnya (opsional, jika waktu hackathon cukup)
```

## 6. Functional Requirements (FR)

| ID | Requirement | Prioritas |
|---|---|---|
| FR-1 | User dapat menginput nominal gaji bulanan melalui form input | Must Have |
| FR-2 | User dapat mengatur/mengubah persentase alokasi per bucket (dengan validasi total = 100%) | Must Have |
| FR-3 | Sistem menghitung nominal rupiah per bucket berdasarkan gaji × persentase | Must Have |
| FR-4 | Sistem menampilkan hasil alokasi dalam bentuk grafik (pie/donut chart) secara instan setelah submit | Must Have |
| FR-5 | Sistem menampilkan tabel rincian nominal per bucket | Must Have |
| FR-6 | Sistem menyediakan daftar kandidat saham hasil filter metrik fundamental (dari dataset statis) | Should Have |
| FR-7 | User dapat menyimpan histori input gaji & alokasi (per bulan) | Should Have |
| FR-8 | User dapat melihat grafik tren alokasi dari beberapa bulan (line/bar chart historis) | Could Have |
| FR-9 | Sistem memberi peringatan jika Dana Darurat sudah mencapai target (misal 6x pengeluaran bulanan) dan menyarankan realokasi | Could Have |
| FR-10 | Export hasil alokasi ke PDF/gambar untuk dibagikan | Won't Have (fase ini) |

## 7. Non-Functional Requirements (NFR)

| ID | Requirement |
|---|---|
| NFR-1 | Rendering grafik hasil alokasi harus tampil dalam < 500ms setelah user submit (sesuai goal "seketika") |
| NFR-2 | Aplikasi berjalan penuh di client-side (Blazor WebAssembly) tanpa dependency backend wajib untuk fitur inti kalkulasi |
| NFR-3 | UI responsif — dapat digunakan di layar desktop maupun mobile (untuk keperluan demo di berbagai device) |
| NFR-4 | Data user (histori gaji) tersimpan secara lokal minimal (browser storage) untuk demo tanpa perlu backend database wajib |
| NFR-5 | Kode terstruktur modular agar mudah di-extend pasca-hackathon (misal: integrasi API market data live) |

## 8. Arsitektur & Tech Stack

### 8.1 Stack Utama
- **Frontend/Client:** Blazor WebAssembly (.NET 10) — full C# tanpa perlu JS framework terpisah untuk logic utama
- **Charting:** Library chart yang kompatibel Blazor WASM (misal: `Blazor-ApexCharts`, `ChartJs.Blazor`, atau native SVG rendering untuk kontrol penuh dan performa ringan)
- **State Management:** Built-in Blazor state container / sederhana menggunakan `Cascading Parameters` atau `Fluxor` jika kompleksitas meningkat
- **Data Persistence (Hackathon Scope):** `IndexedDB`/`LocalStorage` via Blazor interop (misal `Blazored.LocalStorage`) — cukup untuk demo, tidak perlu backend penuh
- **(Opsional) Backend API:** ASP.NET Core Minimal API (.NET 10) jika tim butuh endpoint terpisah untuk kalkulasi/skoring saham dari dataset — disarankan **hanya jika waktu memungkinkan**, karena goal hackathon bisa dicapai full client-side

### 8.2 Struktur Modul (Disarankan)

```
/SmartSalaryRouter
  /Client (Blazor WASM)
    /Pages
      - Dashboard.razor
      - SalaryInput.razor
      - AllocationSettings.razor
      - History.razor
    /Components
      - AllocationChart.razor
      - BucketBreakdownTable.razor
      - StockCandidateList.razor
    /Services
      - AllocationCalculatorService.cs
      - StockFilterService.cs
      - LocalStorageService.cs
    /Models
      - SalaryInput.cs
      - AllocationRule.cs
      - AllocationResult.cs
      - StockMetric.cs
  /Shared (jika ada backend terpisah)
    - DTOs
  /Server (opsional, ASP.NET Core Minimal API)
    - StockDataController.cs (serve dataset statis metrik saham)
```

### 8.3 Model Data Inti (Contoh)

```csharp
public class AllocationRule
{
    public string BucketName { get; set; } // "Dana Darurat", "Emas", dst.
    public decimal Percentage { get; set; } // harus total 100% across all rules
}

public class SalaryInput
{
    public DateTime PeriodDate { get; set; }
    public decimal Amount { get; set; }
}

public class AllocationResult
{
    public string BucketName { get; set; }
    public decimal Percentage { get; set; }
    public decimal AllocatedAmount { get; set; }
}

public class StockMetric
{
    public string Ticker { get; set; }
    public decimal PriceToEarnings { get; set; }
    public decimal DebtToEquity { get; set; }
    public decimal ReturnOnEquity { get; set; }
    public bool PassesFilter { get; set; }
}
```

## 9. Legal & Disclaimer (Wajib Ditampilkan di UI)

Karena aplikasi menyentuh ranah rekomendasi investasi, wajib ada disclaimer yang jelas di UI:

> "Aplikasi ini adalah alat bantu simulasi alokasi aset untuk tujuan edukasi dan perencanaan pribadi. Bukan merupakan nasihat keuangan atau rekomendasi investasi resmi. Keputusan investasi sepenuhnya menjadi tanggung jawab pengguna."

Ini penting terutama untuk fitur filter saham fundamental (§4.2) agar tidak dianggap sebagai rekomendasi finansial mengikat.

## 10. Metrik Keberhasilan (Untuk Demo/Judging Hackathon)

| Metrik | Target |
|---|---|
| Waktu dari input gaji → grafik tampil | < 1 detik (terasa "instan") |
| Kelengkapan alur demo end-to-end | Input → Alokasi → Grafik → (opsional) Histori berjalan tanpa error |
| Kejelasan visual grafik | Juri dapat langsung memahami breakdown tanpa penjelasan tambahan |
| Konfigurabilitas aturan | Juri dapat mengubah persentase dan melihat grafik ter-update real-time |

## 11. Timeline Hackathon (Contoh Estimasi)

| Fase | Durasi | Output |
|---|---|---|
| Setup project Blazor WASM + struktur folder | 1-2 jam | Skeleton project jalan |
| Model data + AllocationCalculatorService | 2 jam | Logic kalkulasi alokasi selesai (unit-testable) |
| UI: SalaryInput + AllocationSettings | 2-3 jam | Form input & pengaturan persen berfungsi |
| Integrasi chart library + rendering grafik | 2-3 jam | Grafik tampil dari hasil kalkulasi |
| Fitur filter saham fundamental (dataset statis) | 2 jam | List kandidat saham tampil |
| Polish UI + histori (jika waktu cukup) | 2 jam | Demo-ready |
| Buffer/testing/demo prep | 1-2 jam | — |

## 12. Risiko & Mitigasi

| Risiko | Mitigasi |
|---|---|
| Waktu hackathon terbatas, backend penuh tidak sempat | Fokuskan semua logic di client-side (Blazor WASM), backend opsional |
| Library chart Blazor kurang stabil/dokumentasi minim | Siapkan fallback render chart manual via SVG/Canvas interop |
| Kompleksitas filter saham fundamental melebar | Gunakan dataset statis kecil (10-20 saham) yang di-hardcode/JSON, bukan API live |
| Scope creep (fitur histori, tren, export PDF) | Tandai sebagai "Could Have"/"Won't Have", fokus ke Must Have dulu |

## 13. Pertanyaan Terbuka (Untuk Didiskusikan Tim)

1. Apakah dataset saham untuk filter fundamental disiapkan manual (JSON statis) atau ingin coba fetch dari sumber publik (risiko waktu)?
2. Apakah histori alokasi perlu persist lintas sesi browser (LocalStorage) atau cukup in-memory untuk demo?
3. Apakah perlu skenario "multi-gaji" (freelancer dengan beberapa sumber income) di scope hackathon, atau cukup single input?

---

*Dokumen ini adalah working draft — silakan revisi bagian mana pun sebelum development dimulai.*