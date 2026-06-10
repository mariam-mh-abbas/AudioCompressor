using AudioCompressor.Algorithms;
using AudioCompressor.Helpers;
using AudioCompressor.Interfaces;
using AudioCompressor.Models;
using NAudio.Wave;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioCompressor
{
    public partial class MainForm : Form
    {
        // Audio state
        private AudioFileInfo _currentFileInfo;
        private short[] _originalSamples;
        private short[] _currentSamples;
        private byte[] _compressedData;
        private bool _isCompressed = false;

        // Player state
        private WaveOutEvent _waveOut;
        private WaveFileReader _waveReader;
        private bool _isPlaying = false;

        // Compression state
        private CancellationTokenSource _cts;
        private bool _isCompressing = false;
        private Stopwatch _compressionStopwatch;

        // Algorithms dictionary
        private Dictionary<AlgorithmType, IAudioCompressor> _algorithms;

        // Chart data tracking
        private List<Tuple<int, double>> _compressionRatioHistory = new List<Tuple<int, double>>();
        private List<Tuple<int, double>> _processingSpeedHistory = new List<Tuple<int, double>>();
        private int _lastProgressPercent = 0;

        public MainForm()
        {
            InitializeComponent();
            InitializeAlgorithms();
            SetupCharts();
            SetupDragDrop();

            // Set default settings
            cboAlgorithm.SelectedIndex = 0;
            cboQuantLevels.SelectedIndex = 3; // 256
            cboDpcmBits.SelectedIndex = 2;   // 4 bits
            cboPredictionOrder.SelectedIndex = 0; // order 1

            // Initial UI state
            UpdateUIState(false);

            //TestPDC(); // ← أضيفي هاد السطر هون
        }

        private void InitializeAlgorithms()
        {
            _algorithms = new Dictionary<AlgorithmType, IAudioCompressor>
            {
                { AlgorithmType.NonlinearQuantization, new NonlinearQuantization() },
                { AlgorithmType.DPCM, new DPCM() },
                { AlgorithmType.PredictiveDifferentialCoding, new PredictiveDifferentialCoding() },
                { AlgorithmType.DeltaModulation, new DeltaModulation() },
                { AlgorithmType.AdaptiveDeltaModulation, new AdaptiveDeltaModulation() }
            };
        }

        private void SetupCharts()
        {
            // Compression Ratio Chart
            var ratioModel = new PlotModel { Title = "Compression Ratio Over Time" };
            ratioModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Progress (%)",
                Minimum = 0,
                Maximum = 100
            });
            ratioModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Compression Ratio",
                Minimum = 0,
                Maximum = 20
            });
            var ratioSeries = new LineSeries
            {
                Title = "Ratio",
                Color = OxyColors.DodgerBlue,
                MarkerType = MarkerType.None
            };
            ratioModel.Series.Add(ratioSeries);
            plotRatio.Model = ratioModel;
            plotRatio.Model.InvalidatePlot(true);

            // Processing Speed Chart
            var speedModel = new PlotModel { Title = "Processing Speed" };
            speedModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Progress (%)",
                Minimum = 0,
                Maximum = 100
            });
            speedModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Samples/sec (thousands)",
                Minimum = 0
            });
            var speedSeries = new LineSeries
            {
                Title = "Speed",
                Color = OxyColors.OrangeRed,
                MarkerType = MarkerType.None
            };
            speedModel.Series.Add(speedSeries);
            plotSpeed.Model = speedModel;
            plotSpeed.Model.InvalidatePlot(true);
        }

        private void SetupDragDrop()
        {
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(MainForm_DragEnter);
            this.DragDrop += new DragEventHandler(MainForm_Drop);

            // Also allow drop on the main panel
            panelMain.AllowDrop = true;
            panelMain.DragEnter += new DragEventHandler(MainForm_DragEnter);
            panelMain.DragDrop += new DragEventHandler(MainForm_Drop);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string ext = Path.GetExtension(files[0]).ToLower();
                    if (ext == ".wav" || ext == ".mp3" || ext == ".afc")
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void MainForm_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    LoadFile(files[0]);
                }
            }
        }

        // ==================== FILE OPERATIONS ====================

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Open Audio File";
                ofd.Filter = "Audio Files|*.wav;*.mp3|WAV Files|*.wav|MP3 Files|*.mp3|Compressed Files|*.afc|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadFile(ofd.FileName);
                }
            }
        }

        private void LoadFile(string filePath)
        {
            try
            {
                string ext = Path.GetExtension(filePath).ToLower();

                if (ext == ".afc")
                {
                    LoadCompressedFile(filePath);
                    return;
                }

                // Reset previous state
                StopPlayback();
                _isCompressed = false;
                _compressedData = null;

                // Load file info
                _currentFileInfo = AudioHelper.GetAudioFileInfo(filePath);
                DisplayFileInfo(_currentFileInfo);

                // Read samples
                _originalSamples = AudioHelper.ReadAudioSamples(filePath);
                _currentSamples = (short[])_originalSamples.Clone();

                // Update settings based on file
                txtSampleRate.Text = _currentFileInfo.SampleRate.ToString();
                txtChannels.Text = _currentFileInfo.Channels.ToString();
                txtBitsPerSample.Text = _currentFileInfo.BitsPerSample.ToString();

                // Enable UI
                UpdateUIState(true);
                lblStatus.Text = $"Loaded: {_currentFileInfo.FileName}";

                // Draw waveform
                DrawWaveform(_currentSamples);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCompressedFile(string filePath)
        {
            try
            {
                var (data, algorithm, settings) = AudioHelper.ReadCompressedFile(filePath);

                _compressedData = data;
                _isCompressed = true;

                // Update settings
                cboAlgorithm.SelectedIndex = (int)algorithm;
                txtSampleRate.Text = settings.SampleRate.ToString();
                txtMu.Text = settings.Mu.ToString();
                txtDeltaStep.Text = settings.DeltaStepSize.ToString();
                txtMinStep.Text = settings.MinStepSize.ToString();
                txtMaxStep.Text = settings.MaxStepSize.ToString();
                cboQuantLevels.SelectedIndex = GetQuantLevelIndex(settings.QuantizationLevels);
                cboDpcmBits.SelectedIndex = settings.DpcmBits - 1;
                cboPredictionOrder.SelectedIndex = settings.PredictionOrder - 1;

                UpdateUIState(true);
                lblStatus.Text = $"Loaded compressed file: {Path.GetFileName(filePath)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading compressed file:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFileInfo(AudioFileInfo info)
        {
            lblFileName.Text = info.FileName;
            lblFileSize.Text = info.FileSizeFormatted;
            lblDuration.Text = info.DurationFormatted;
            lblSampleRateInfo.Text = $"{info.SampleRate} Hz";
            lblChannelsInfo.Text = info.Channels == 1 ? "Mono" : $"Stereo ({info.Channels})";
            lblBitRateInfo.Text = $"{info.BitRate} kbps";
            lblBitsPerSampleInfo.Text = $"{info.BitsPerSample} bit";
            lblEncodingInfo.Text = info.Encoding;
        }

        // ==================== AUDIO PLAYBACK ====================

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (_currentSamples == null) return;
            PlaySamples(_currentSamples);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_waveOut != null && _isPlaying)
            {
                _waveOut.Pause();
                _isPlaying = false;
                btnPause.Text = "Resume";
            }
            else if (_waveOut != null)
            {
                _waveOut.Play();
                _isPlaying = true;
                btnPause.Text = "Pause";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        private void PlaySamples(short[] samples)
        {
            StopPlayback();

            try
            {
                int sampleRate = int.TryParse(txtSampleRate.Text, out int sr) ? sr : 44100;

                // Write samples to a temporary WAV file for playback
                string tempFile = Path.Combine(Path.GetTempPath(), "audio_compressor_preview.wav");
                //AudioHelper.WriteWavFile(tempFile, samples, sampleRate, 1);
                int ch = _currentFileInfo?.Channels ?? 1;
                AudioHelper.WriteWavFile(tempFile, samples, sampleRate, ch);

                _waveReader = new WaveFileReader(tempFile);
                _waveOut = new WaveOutEvent();
                _waveOut.Init(_waveReader);
                _waveOut.PlaybackStopped += (s, args) =>
                {
                    _isPlaying = false;
                    btnPause.Text = "Pause";
                };
                _waveOut.Play();
                _isPlaying = true;
                btnPause.Text = "Pause";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing audio:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopPlayback()
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
                _waveOut = null;
            }
            if (_waveReader != null)
            {
                _waveReader.Dispose();
                _waveReader = null;
            }
            _isPlaying = false;
            if (btnPause != null)
                btnPause.Text = "Pause";
        }

        // ==================== COMPRESSION ====================

        private async void btnCompress_Click(object sender, EventArgs e)
        {
            if (_originalSamples == null)
            {
                MessageBox.Show("Please load an audio file first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isCompressing)
                return;

            var settings = GetSettingsFromUI();
            if (settings == null) return;

            if (!ValidateSettings(settings))
                return;

            _isCompressing = true;
            _cts = new CancellationTokenSource();
            _compressionStopwatch = Stopwatch.StartNew();

            // Clear chart data
            _compressionRatioHistory.Clear();
            _processingSpeedHistory.Clear();
            _lastProgressPercent = 0;
            ClearCharts();

            // UI updates
            btnCompress.Enabled = false;
            btnDecompress.Enabled = false;
            btnCancel.Enabled = true;
            progressBar1.Value = 0;

            var algorithm = _algorithms[settings.Algorithm];

            try
            {
                var progressHandler = new Progress<int>(percent =>
                {
                    if (this.IsDisposed) return;
                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Value = Math.Min(100, percent);
                        lblProgressPercent.Text = $"{percent}%";

                        // Track metrics
                        long elapsedMs = _compressionStopwatch.ElapsedMilliseconds;
                        if (elapsedMs > 0 && percent > _lastProgressPercent)
                        {
                            double samplesSoFar = (double)_originalSamples.Length * percent / 100.0;
                            double speed = samplesSoFar / (elapsedMs / 1000.0) / 1000.0; // thousands/sec

                            // Estimate current compression ratio
                            //long originalBytes = _originalSamples.Length * 2;
                            //long estimatedCompressedBytes = (long)(originalBytes * (1.0 - percent / 200.0));
                            //if (estimatedCompressedBytes > 0)
                            //{
                            //    double estimatedRatio = (double)originalBytes / estimatedCompressedBytes;
                            //    _compressionRatioHistory.Add(Tuple.Create(percent, estimatedRatio));
                            //}

                            // بعد التعديل — تقدير ذكي حسب الخوارزمية
                            long originalBytes = _originalSamples.Length * 2;

                            double estimatedRatio;
                            AlgorithmType algo = settings.Algorithm;

                            if (algo == AlgorithmType.DeltaModulation ||
                                algo == AlgorithmType.AdaptiveDeltaModulation)
                            {
                                // Delta Modulation: 1 bit/عينة = نسبة 16:1 ثابتة
                                estimatedRatio = 16.0;
                            }
                            else if (algo == AlgorithmType.DPCM ||
                                     algo == AlgorithmType.PredictiveDifferentialCoding)
                            {
                                // DPCM/PDC: حسب DpcmBits
                                estimatedRatio = 16.0 / settings.DpcmBits;
                            }
                            else if (algo == AlgorithmType.NonlinearQuantization)
                            {
                                // Nonlinear: حسب QuantizationLevels
                                int bits = (int)Math.Log(settings.QuantizationLevels, 2);
                                estimatedRatio = 16.0 / bits;
                            }
                            else
                            {
                                estimatedRatio = 2.0;
                            }

                            _compressionRatioHistory.Add(Tuple.Create(percent, estimatedRatio));
                            _processingSpeedHistory.Add(Tuple.Create(percent, speed));

                            UpdateCharts();
                            _lastProgressPercent = percent;
                        }
                    }));
                });

                byte[] result = await Task.Run(() =>
                    algorithm.Compress(_originalSamples, settings, progressHandler, _cts.Token));

                _compressionStopwatch.Stop();

                if (result != null)
                {
                    _compressedData = result;
                    _isCompressed = true;

                    // Final compression ratio
                    double ratio = (double)(_originalSamples.Length * 2) / result.Length;
                    double saving = (1.0 - (double)result.Length / (_originalSamples.Length * 2)) * 100;

                    lblCompressionRatio.Text = $"{ratio:F2} : 1";
                    lblSpaceSaving.Text = $"{saving:F1}%";
                    lblOriginalSize.Text = AudioHelper_GetFormattedSize(_originalSamples.Length * 2);
                    lblCompressedSize.Text = AudioHelper_GetFormattedSize(result.Length);
                    lblTimeElapsed.Text = $"{_compressionStopwatch.Elapsed.TotalSeconds:F3} sec";

                    // Update final chart point
                    _compressionRatioHistory.Add(Tuple.Create(100, ratio));
                    _processingSpeedHistory.Add(Tuple.Create(100,
                        _originalSamples.Length / _compressionStopwatch.Elapsed.TotalSeconds / 1000.0));
                    UpdateCharts();

                    lblStatus.Text = $"Compression complete! Algorithm: {algorithm.AlgorithmName}";

                    // Show report
                    // Decompress to calculate SNR
                    short[] decompressed = null;
                    try
                    {
                        decompressed = algorithm.Decompress(result, settings, null, CancellationToken.None);
                    }
                    catch { }

                    // Show report with SNR
                    ShowReport(settings, result.Length, decompressed);
                }
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Compression cancelled.";
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Compression error:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Compression failed.";
            }
            finally
            {
                _isCompressing = false;
                btnCompress.Enabled = true;
                btnDecompress.Enabled = true;
                btnCancel.Enabled = false;
                _cts?.Dispose();
                _cts = null;
            }
        }

        private async void btnDecompress_Click(object sender, EventArgs e)
        {
            if (_compressedData == null)
            {
                MessageBox.Show("No compressed data available. Compress a file first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isCompressing) return;

            var settings = GetSettingsFromUI();
            if (settings == null) return;

            _isCompressing = true;
            _cts = new CancellationTokenSource();
            _compressionStopwatch = Stopwatch.StartNew();

            progressBar1.Value = 0;
            btnCompress.Enabled = false;
            btnDecompress.Enabled = false;

            var algorithm = _algorithms[settings.Algorithm];

            try
            {
                var progressHandler = new Progress<int>(percent =>
                {
                    if (this.IsDisposed) return;
                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Value = Math.Min(100, percent);
                        lblProgressPercent.Text = $"{percent}%";
                    }));
                });

                short[] result = await Task.Run(() =>
                    algorithm.Decompress(_compressedData, settings, progressHandler, _cts.Token));

                _compressionStopwatch.Stop();

                if (result != null)
                {
                    _currentSamples = result;
                    _isCompressed = false;

                    DrawWaveform(result);
                    lblStatus.Text = "Decompression complete! You can play the decompressed audio.";

                    // Auto-play
                    PlaySamples(result);
                }
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Decompression cancelled.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Decompression error:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isCompressing = false;
                btnCompress.Enabled = true;
                btnDecompress.Enabled = true;
                _cts?.Dispose();
                _cts = null;
            }
        }

        // ==================== CANCEL & RESET ====================

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    if (_cts != null && _isCompressing)
        //    {
        //        _cts.Cancel();
        //        lblStatus.Text = "Cancelling...";
        //    }
        //}

            private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_cts != null && _isCompressing)
            {
                _cts.Cancel();
                lblStatus.Text = "Compression cancelled.";
                progressBar1.Value = 0;
                lblProgressPercent.Text = "0%";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (_originalSamples == null) return;

            StopPlayback();
            _currentSamples = (short[])_originalSamples.Clone();
            _compressedData = null;
            _isCompressed = false;

            DrawWaveform(_currentSamples);

            // Reset labels
            lblCompressionRatio.Text = "--";
            lblSpaceSaving.Text = "--";
            lblOriginalSize.Text = "--";
            lblCompressedSize.Text = "--";
            lblTimeElapsed.Text = "--";
            progressBar1.Value = 0;
            lblProgressPercent.Text = "0%";

            ClearCharts();

            lblStatus.Text = "Reset to original audio.";
        }

        // ==================== SAVE ====================

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_compressedData == null && _currentSamples == null)
            {
                MessageBox.Show("No data to save.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                if (_isCompressed && _compressedData != null)
                {
                    sfd.Title = "Save Compressed Audio";
                    sfd.Filter = "Compressed Audio|*.afc";
                    sfd.FileName = Path.GetFileNameWithoutExtension(_currentFileInfo?.FileName ?? "audio") + "_compressed.afc";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var settings = GetSettingsFromUI();
                        AudioHelper.WriteCompressedFile(sfd.FileName, _compressedData, settings.Algorithm, settings);
                        lblStatus.Text = $"Saved compressed file: {sfd.FileName}";
                    }
                }
                else if (_currentSamples != null)
                {
                    sfd.Title = "Save Audio File";
                    sfd.Filter = "WAV Files|*.wav";
                    sfd.FileName = Path.GetFileNameWithoutExtension(_currentFileInfo?.FileName ?? "audio") + "_decompressed.wav";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        int sampleRate = int.TryParse(txtSampleRate.Text, out int sr) ? sr : 44100;
                        int channels = _currentFileInfo?.Channels ?? 1;
                        AudioHelper.WriteWavFile(sfd.FileName, _currentSamples, sampleRate, channels);
                        lblStatus.Text = $"Saved WAV file: {sfd.FileName}";
                    }
                }
            }
        }

        // ==================== SETTINGS ====================

        private void cboAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show/hide relevant settings based on algorithm
            AlgorithmType algo = (AlgorithmType)cboAlgorithm.SelectedIndex;

            panelQuantSettings.Visible = (algo == AlgorithmType.NonlinearQuantization);
            panelDpcmSettings.Visible = (algo == AlgorithmType.DPCM || algo == AlgorithmType.PredictiveDifferentialCoding);
            panelPredictiveSettings.Visible = (algo == AlgorithmType.PredictiveDifferentialCoding);
            panelDeltaSettings.Visible = (algo == AlgorithmType.DeltaModulation || algo == AlgorithmType.AdaptiveDeltaModulation);
            panelAdaptiveDeltaSettings.Visible = (algo == AlgorithmType.AdaptiveDeltaModulation);
        }

        private CompressionSettings GetSettingsFromUI()
        {
            var settings = new CompressionSettings();

            try
            {
                settings.Algorithm = (AlgorithmType)cboAlgorithm.SelectedIndex;
                settings.SampleRate = int.Parse(txtSampleRate.Text);
                settings.QuantizationLevels = int.Parse(cboQuantLevels.SelectedItem?.ToString() ?? "256");
                settings.Mu = double.Parse(txtMu.Text);
                settings.DeltaStepSize = int.Parse(txtDeltaStep.Text);
                settings.MinStepSize = int.Parse(txtMinStep.Text);
                settings.MaxStepSize = int.Parse(txtMaxStep.Text);
                settings.PredictionOrder = int.Parse(cboPredictionOrder.SelectedItem?.ToString() ?? "1");
                settings.DpcmBits = int.Parse(cboDpcmBits.SelectedItem?.ToString() ?? "4");
                settings.Channels = int.Parse(txtChannels.Text);
                settings.BitsPerSample = int.Parse(txtBitsPerSample.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Invalid settings:\n{ex.Message}", "Settings Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return settings;
        }

        private bool ValidateSettings(CompressionSettings settings)
        {
            if (settings.SampleRate <= 0)
            {
                MessageBox.Show("Sample rate must be positive.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (settings.QuantizationLevels < 2 || settings.QuantizationLevels > 65536)
            {
                MessageBox.Show("Quantization levels must be between 2 and 65536.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (settings.Mu <= 0)
            {
                MessageBox.Show("Mu parameter must be positive.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (settings.DeltaStepSize <= 0)
            {
                MessageBox.Show("Delta step size must be positive.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (settings.Algorithm == AlgorithmType.AdaptiveDeltaModulation)
            {
                if (settings.MinStepSize >= settings.MaxStepSize)
                {
                    MessageBox.Show("Min step size must be less than max step size.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (settings.Algorithm == AlgorithmType.PredictiveDifferentialCoding)
            {
                if (settings.PredictionOrder < 1 || settings.PredictionOrder > 4)
                {
                    MessageBox.Show("Prediction Order must be between 1 and 4.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (settings.DpcmBits < 1 || settings.DpcmBits > 16)
                {
                    MessageBox.Show("DPCM Bits must be between 1 and 16.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        // ==================== CHARTS ====================

        private void UpdateCharts()
        {
            try
            {
                // Update compression ratio chart
                var ratioModel = plotRatio.Model;
                if (ratioModel != null && ratioModel.Series.Count > 0)
                {
                    var series = (LineSeries)ratioModel.Series[0];
                    series.Points.Clear();
                    foreach (var point in _compressionRatioHistory)
                    {
                        series.Points.Add(new DataPoint(point.Item1, point.Item2));
                    }
                    ratioModel.InvalidatePlot(true);
                }

                // Update processing speed chart
                var speedModel = plotSpeed.Model;
                if (speedModel != null && speedModel.Series.Count > 0)
                {
                    var series = (LineSeries)speedModel.Series[0];
                    series.Points.Clear();
                    foreach (var point in _processingSpeedHistory)
                    {
                        series.Points.Add(new DataPoint(point.Item1, point.Item2));
                    }
                    speedModel.InvalidatePlot(true);
                }
            }
            catch { }
        }

        private void ClearCharts()
        {
            try
            {
                var ratioModel = plotRatio.Model;
                if (ratioModel != null && ratioModel.Series.Count > 0)
                {
                    ((LineSeries)ratioModel.Series[0]).Points.Clear();
                    ratioModel.InvalidatePlot(true);
                }

                var speedModel = plotSpeed.Model;
                if (speedModel != null && speedModel.Series.Count > 0)
                {
                    ((LineSeries)speedModel.Series[0]).Points.Clear();
                    speedModel.InvalidatePlot(true);
                }
            }
            catch { }
        }

        // ==================== WAVEFORM DRAWING ====================

        private void DrawWaveform(short[] samples)
        {
            if (picWaveform.Width <= 0 || picWaveform.Height <= 0) return;

            int width = picWaveform.Width;
            int height = picWaveform.Height;
            var bmp = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(30, 30, 45));

                // Draw center line
                using (var pen = new Pen(Color.FromArgb(60, 60, 80)))
                {
                    g.DrawLine(pen, 0, height / 2, width, height / 2);
                }

                // Draw waveform
                if (samples != null && samples.Length > 0)
                {
                    int samplesPerPixel = Math.Max(1, samples.Length / width);

                    using (var pen = new Pen(Color.DodgerBlue, 1))
                    {
                        for (int x = 0; x < width; x++)
                        {
                            int startSample = x * samplesPerPixel;
                            int endSample = Math.Min(startSample + samplesPerPixel, samples.Length);

                            short minVal = short.MaxValue;
                            short maxVal = short.MinValue;

                            for (int i = startSample; i < endSample; i++)
                            {
                                if (samples[i] < minVal) minVal = samples[i];
                                if (samples[i] > maxVal) maxVal = samples[i];
                            }

                            int yMin = (int)((1.0 - (double)maxVal / 32768.0) * height / 2);
                            int yMax = (int)((1.0 - (double)minVal / 32768.0) * height / 2);

                            g.DrawLine(pen, x, yMin, x, yMax);
                        }
                    }
                }
            }

            picWaveform.Image = bmp;
        }

        // ==================== REPORT ====================

        private void ShowReport(CompressionSettings settings, int compressedSize, short[] decompressedSamples = null)
        {
            // Calculate SNR
            double snr = 0;
            if (decompressedSamples != null && decompressedSamples.Length == _originalSamples.Length)
            {
                double signalPower = 0;
                double noisePower = 0;
                for (int i = 0; i < _originalSamples.Length; i++)
                {
                    double signal = (double)_originalSamples[i];
                    double noise = (double)_originalSamples[i] - (double)decompressedSamples[i];
                    signalPower += signal * signal;
                    noisePower += noise * noise;
                }
                if (noisePower > 0 && signalPower > 0)
                    snr = 10.0 * Math.Log10(signalPower / noisePower);
                else
                    snr = 99.99;
            }

            var report = new CompressionReport
            {
                OriginalFileInfo = _currentFileInfo,
                OriginalSizeFormatted = AudioHelper_GetFormattedSize(_originalSamples.Length * 2),
                CompressedSizeFormatted = AudioHelper_GetFormattedSize(compressedSize),
                SpaceSavingPercent = (1.0 - (double)compressedSize / (_originalSamples.Length * 2)) * 100,
                CompressionRatio = (double)(_originalSamples.Length * 2) / compressedSize,
                ElapsedTime = _compressionStopwatch.Elapsed,
                AlgorithmName = _algorithms[settings.Algorithm].AlgorithmName,
                Settings = settings,
                SNR = snr
            };

            var reportForm = new ReportForm(report);
            reportForm.ShowDialog(this);
        }

        // ==================== HELPERS ====================

        private string AudioHelper_GetFormattedSize(long bytes)
        {
            if (bytes < 1024)
                return $"{bytes} B";
            else if (bytes < 1024 * 1024)
                return $"{bytes / 1024.0:F2} KB";
            else
                return $"{bytes / (1024.0 * 1024.0):F2} MB";
        }

        private int GetQuantLevelIndex(int levels)
        {
            int[] values = { 8, 16, 32, 64, 128, 256 };
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == levels) return i;
            }
            return 3; // default to 256
        }

        private void UpdateUIState(bool fileLoaded)
        {
            btnPlay.Enabled = fileLoaded;
            btnPause.Enabled = fileLoaded;
            btnStop.Enabled = fileLoaded;
            btnCompress.Enabled = fileLoaded;
            btnDecompress.Enabled = fileLoaded;
            btnReset.Enabled = fileLoaded;
            btnSave.Enabled = fileLoaded;
            grpSettings.Enabled = fileLoaded;
        }

        // Form resize - redraw waveform
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_currentSamples != null && picWaveform.Width > 0)
            {
                DrawWaveform(_currentSamples);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopPlayback();
            _cts?.Cancel();
            base.OnFormClosing(e);
        }

        private void plotSpeed_Click(object sender, EventArgs e)
        {

        }

        // ==================== TEST ====================
        //private void TestPDC()
        //{
        //    var coder = new PredictiveDifferentialCoding();
        //    var settings = new CompressionSettings
        //    {
        //        DpcmBits = 8,
        //        PredictionOrder = 1,
        //        Channels = 1
        //    };

        //    short[] original = { 5000, 8000, 12000, 9000, 15000,
        //             20000, 18000, 10000 };

        //    var cts = new CancellationTokenSource();
        //    byte[] compressed = coder.Compress(original, settings, null, cts.Token);
        //    short[] restored = coder.Decompress(compressed, settings, null, cts.Token);

        //    string msg = $"Compressed size: {compressed.Length} bytes\n\n";
        //    bool ok = true;
        //    for (int i = 0; i < original.Length; i++)
        //    {
        //        int diff = Math.Abs(original[i] - restored[i]);
        //        if (diff > 50) ok = false;
        //        msg += $"orig={original[i]}, rest={restored[i]}, diff={diff}\n";
        //    }
        //    msg += ok ? "\n✓ شغالة" : "\n✗ في مشكلة";
        //    MessageBox.Show(msg, "PDC Test");
        //}
    }


}
