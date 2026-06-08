using AudioCompressor.Interfaces;
using AudioCompressor.Models;
using System;
using System.Threading;

namespace AudioCompressor.Algorithms
{
    public class PredictiveDifferentialCoding : IAudioCompressor
    {
        public string AlgorithmName => "Predictive Differential Coding";

        public double CompressionRatio { get; private set; }

        public byte[] Compress(short[] samples, CompressionSettings settings,
    IProgress<int> progress, CancellationToken cancellationToken)
        {
            int order = settings.PredictionOrder; // بدل GetExtra
            int bitsPerSample = settings.DpcmBits; // 4 bits افتراضي، المستخدم يغيره من DPCM Bits
            int levels = 1 << bitsPerSample;
            double scale = 32768.0 / (levels / 2);

            int totalBits = samples.Length * bitsPerSample;
            int totalBytes = (totalBits + 7) / 8;
            byte[] packed = new byte[totalBytes];
            int bitPos = 0;

            short predL = 0, predL2 = 0;
            short predR = 0, predR2 = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                // إلغاء لو طلب المستخدم
                if (cancellationToken.IsCancellationRequested) return null;

                bool isLeft = (i % 2 == 0);

                short predicted;
                if (order >= 2)
                    predicted = isLeft
                        ? (short)(2 * predL - predL2)
                        : (short)(2 * predR - predR2);
                else
                    predicted = isLeft ? predL : predR;

                int diff = samples[i] - predicted;
                int clipped = (int)Math.Round(diff / scale);
                clipped = Math.Max(-(levels / 2), Math.Min(levels / 2 - 1, clipped));
                int q = clipped + (levels / 2);
                q = Math.Max(0, Math.Min(levels - 1, q));

                for (int b = 0; b < bitsPerSample; b++)
                {
                    if (((q >> b) & 1) == 1)
                        packed[bitPos / 8] |= (byte)(1 << (bitPos % 8));
                    bitPos++;
                }

                int diffRestored = (int)Math.Round((q - (levels / 2)) * scale);
                short reconstructed = (short)Math.Max(short.MinValue,
                    Math.Min(short.MaxValue, predicted + diffRestored));
                if (isLeft) { predL2 = predL; predL = reconstructed; }
                else { predR2 = predR; predR = reconstructed; }

                // Progress كل 1%
                if (i % Math.Max(1, samples.Length / 100) == 0)
                    progress?.Report((int)((double)i / samples.Length * 100));
            }

            byte[] result = new byte[8 + packed.Length];
            BitConverter.GetBytes(samples.Length).CopyTo(result, 0);
            BitConverter.GetBytes(bitsPerSample).CopyTo(result, 4);
            packed.CopyTo(result, 8);

            CompressionRatio = (double)(samples.Length * 2) / result.Length;
            progress?.Report(100);
            return result;
        }

        public short[] Decompress(byte[] data, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken)
        {
            int sampleCount = BitConverter.ToInt32(data, 0);
            int bitsPerSample = BitConverter.ToInt32(data, 4);
            int order = settings.PredictionOrder;
            int levels = 1 << bitsPerSample;
            double scale = 32768.0 / (levels / 2);

            short[] samples = new short[sampleCount];
            int bitPos = 0;

            short predL = 0, predL2 = 0;
            short predR = 0, predR2 = 0;

            for (int i = 0; i < sampleCount; i++)
            {
                if (cancellationToken.IsCancellationRequested) return null;

                int q = 0;
                for (int b = 0; b < bitsPerSample; b++)
                {
                    int byteIdx = 8 + (bitPos / 8);
                    int bitIdx = bitPos % 8;
                    if (((data[byteIdx] >> bitIdx) & 1) == 1)
                        q |= (1 << b);
                    bitPos++;
                }

                bool isLeft = (i % 2 == 0);
                short predicted;
                if (order >= 2)
                    predicted = isLeft
                        ? (short)(2 * predL - predL2)
                        : (short)(2 * predR - predR2);
                else
                    predicted = isLeft ? predL : predR;

                int diff = (int)Math.Round((q - (levels / 2)) * scale);
                int restored = predicted + diff;
                samples[i] = (short)Math.Max(short.MinValue,
                               Math.Min(short.MaxValue, restored));

                if (isLeft) { predL2 = predL; predL = samples[i]; }
                else { predR2 = predR; predR = samples[i]; }

                if (i % Math.Max(1, sampleCount / 100) == 0)
                    progress?.Report((int)((double)i / sampleCount * 100));
            }

            progress?.Report(100);
            return samples;
        }

        /// <summary>
        /// Compute simple linear predictor coefficients using autocorrelation method.
        /// </summary>
        private double[] ComputePredictorCoefficients(short[] samples, int order)
        {
            double[] coeffs = new double[order];

            // Simple approach: use fixed coefficients for order 1
            if (order == 1)
            {
                coeffs[0] = 0.95; // strong correlation between consecutive samples
                return coeffs;
            }

            // For higher orders, compute using autocorrelation
            int N = Math.Min(samples.Length, 10000); // use first 10k samples for estimation
            double[] x = new double[N];
            for (int i = 0; i < N; i++)
                x[i] = samples[i];

            // Compute autocorrelation
            double[] R = new double[order + 1];
            for (int k = 0; k <= order; k++)
            {
                double sum = 0;
                for (int n = k; n < N; n++)
                    sum += x[n] * x[n - k];
                R[k] = sum;
            }

            // Levinson-Durbin recursion
            double[] a = new double[order + 1];
            double[] aPrev = new double[order + 1];
            a[0] = 1.0;

            double err = R[0];

            for (int m = 1; m <= order; m++)
            {
                double lambda = 0;
                for (int k = 0; k < m; k++)
                    lambda += a[k] * R[m - k];
                lambda = -lambda / err;

                for (int k = 0; k <= m; k++)
                    aPrev[k] = a[k];

                for (int k = 1; k <= m; k++)
                    a[k] = aPrev[k] + lambda * aPrev[m - k];

                err = err * (1 - lambda * lambda);
            }

            for (int k = 0; k < order; k++)
                coeffs[k] = -a[k + 1];

            return coeffs;
        }
    }
}
