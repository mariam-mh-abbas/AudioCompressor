# Audio Compressor - Multimedia Systems Project

A desktop application for audio file compression built with C# WinForms (.NET Framework 4.7.2).

## Features

1. **Load audio files** via GUI file dialog or drag-and-drop (WAV, MP3 supported)
2. **Audio playback** with Play/Pause/Stop controls
3. **Display audio properties** (sample rate, channels, bits/sample, duration, bit rate, encoding, file size)
4. **5 Compression Algorithms:**
   - Nonlinear Quantization (mu-law companding)
   - DPCM (Differential Pulse Code Modulation)
   - Predictive Differential Coding (with Levinson-Durbin predictor)
   - Delta Modulation
   - Adaptive Delta Modulation
5. **Adjustable compression settings** per algorithm (quantization levels, mu parameter, delta step, prediction order, DPCM bits)
6. **Real-time progress monitoring** with progress bar and percentage
7. **Real-time charts** showing compression ratio and processing speed over time (OxyPlot)
8. **Cancellation support** - cancel compression/decompression mid-process
9. **Reset to original** - restore original audio after compression
10. **Compression report** with full details (algorithm, ratio, space saving, time elapsed, settings)
11. **Save compressed files** (.afc custom format) and decompressed WAV files

## Prerequisites

- Visual Studio 2019 (Enterprise/Professional/Community)
- .NET Framework 4.7.2 (included in VS 2019)

## Setup Instructions

1. Open `AudioCompressor.sln` in Visual Studio 2019
2. Restore NuGet packages:
   - Right-click the solution in Solution Explorer → "Restore NuGet Packages"
   - Or build the project (VS will prompt to restore missing packages)
3. Build and run the project (F5)

## NuGet Packages

The project uses the following NuGet packages (auto-restored on build):

| Package | Version | Purpose |
|---------|---------|---------|
| NAudio | 2.2.1 | Audio file reading, playback, format conversion |
| NAudio.Core | 2.2.1 | Core NAudio library |
| OxyPlot.Core | 2.1.2 | Chart rendering engine |
| OxyPlot.WindowsForms | 2.1.2 | WinForms chart controls |
| MathNet.Numerics | 5.0.0 | Numerical computations (optional) |

## Project Structure

```
AudioCompressor/
├── Algorithms/
│   ├── NonlinearQuantization.cs      # mu-law companding compression
│   ├── DPCM.cs                       # Differential Pulse Code Modulation
│   ├── PredictiveDifferentialCoding.cs # Linear predictor + differential coding
│   ├── DeltaModulation.cs            # 1-bit delta modulation
│   └── AdaptiveDeltaModulation.cs    # Adaptive step-size delta modulation
├── Forms/
│   ├── MainForm.cs                   # Main application form (logic)
│   ├── MainForm.Designer.cs          # Main form UI layout
│   ├── ReportForm.cs                 # Compression report form
│   └── ReportForm.Designer.cs        # Report form UI layout
├── Helpers/
│   └── AudioHelper.cs                # Audio file I/O, WAV reading/writing
├── Interfaces/
│   └── IAudioCompressor.cs           # Compression algorithm interface
├── Models/
│   ├── AudioFileInfo.cs              # Audio file metadata model
│   ├── CompressionSettings.cs        # Compression settings + AlgorithmType enum
│   ├── CompressionResult.cs          # Compression result with metrics
│   └── CompressionReport.cs          # Report model for export
├── Properties/
│   └── AssemblyInfo.cs               # Assembly metadata
├── App.config                        # Application configuration
├── packages.config                   # NuGet package references
├── AudioCompressor.csproj            # Project file
├── AudioCompressor.sln               # Solution file
└── Program.cs                        # Entry point
```

## Algorithm Details

### 1. Nonlinear Quantization (mu-law)
Applies mu-law companding: `y = sign(x) * ln(1 + mu*|x|) / ln(1 + mu)`. This gives more resolution to quiet sounds and less to loud sounds. Parameters: mu (default 255), quantization levels (8-256).

### 2. DPCM
Encodes differences between consecutive samples instead of absolute values. Since differences are typically smaller, fewer bits are needed. Parameters: DPCM bits (2-8 bits per difference).

### 3. Predictive Differential Coding
Uses a linear predictor (computed via Levinson-Durbin recursion) to estimate the next sample. The residual (actual - predicted) is quantized and stored. Parameters: prediction order (1-4), DPCM bits.

### 4. Delta Modulation
Simplest differential coding: each sample is compared to the predicted value, outputting 1 bit per sample. Compression ratio is 16:1 for 16-bit audio. Parameter: delta step size.

### 5. Adaptive Delta Modulation
Like Delta Modulation but with adaptive step size. When consecutive bits are the same (slope overload), the step increases. When bits alternate (granular noise), the step decreases. Parameters: initial delta step, min step, max step.

## Custom File Format (.afc)

Compressed files use a custom format with the `.afc` extension:
- Header: "AFC" magic number + version byte + algorithm type + compression settings
- Body: Algorithm-specific compressed byte data
