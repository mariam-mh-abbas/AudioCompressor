using AudioCompressor.Interfaces;
using AudioCompressor.Models;
using System;
using System.Threading;

namespace AudioCompressor.Algorithms
{
    /// <summary>
    /// Nonlinear Quantization using mu-law companding.
    /// Member 1's algorithm.
    ///
    /// Mu-law compression maps a linear sample value to a nonlinear scale
    /// using the formula: y = sign(x) * ln(1 + mu*|x|) / ln(1 + mu)
    /// This gives more resolution to quieter sounds and less to louder ones.
    /// </summary>
    public class NonlinearQuantization : IAudioCompressor
    {
        public string AlgorithmName => "Nonlinear Quantization (mu-law)";

        public double CompressionRatio { get; private set; }

        public byte[] Compress(short[] samples, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken)
        {
            int totalSamples = samples.Length;
            double mu = settings.Mu;
            int levels = settings.QuantizationLevels;
            int bitsPerSample = (int)Math.Log(levels, 2);

            // The compressed data stores the quantized mu-law indices
            // Each sample is compressed to 'bitsPerSample' bits
            // We pack bits into bytes

            // Calculate output size
            int totalBits = totalSamples * bitsPerSample;
            int outputSize = (totalBits + 7) / 8;

            // Header: 12 bytes (sample count + mu + bits per sample + levels)
            byte[] output = new byte[outputSize + 12];

            // Write header
            byte[] countBytes = BitConverter.GetBytes(totalSamples);
            byte[] muBytes = BitConverter.GetBytes(mu);
            byte[] bitsBytes = BitConverter.GetBytes((short)bitsPerSample);

            Array.Copy(countBytes, 0, output, 0, 4);
            Array.Copy(muBytes, 0, output, 4, 8);
            Array.Copy(bitsBytes, 0, output, 12, 2);

            double lnMuPlus1 = Math.Log(1 + mu);
            double maxSample = 32768.0; // max value for 16-bit audio

            long bitBuffer = 0;
            int bitsInBuffer = 0;
            int byteIndex = 14; // start after header

            for (int i = 0; i < totalSamples; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return null;

                // Normalize sample to [-1.0, 1.0]
                double normalized = samples[i] / maxSample;

                // Apply mu-law compression formula
                double compressed;
                if (normalized >= 0)
                    compressed = Math.Log(1 + mu * normalized) / lnMuPlus1;
                else
                    compressed = -Math.Log(1 + mu * (-normalized)) / lnMuPlus1;

                // Quantize to 'levels' levels
                // Map from [-1, 1] to [0, levels-1]
                int quantized = (int)Math.Round((compressed + 1.0) / 2.0 * (levels - 1));
                quantized = Math.Max(0, Math.Min(levels - 1, quantized));

                // Pack bits
                bitBuffer = (bitBuffer << bitsPerSample) | (uint)quantized;
                bitsInBuffer += bitsPerSample;

                while (bitsInBuffer >= 8)
                {
                    bitsInBuffer -= 8;
                    if (byteIndex < output.Length)
                    {
                        output[byteIndex++] = (byte)((bitBuffer >> bitsInBuffer) & 0xFF);
                    }
                }

                // Report progress every 1%
                if (i % Math.Max(1, totalSamples / 100) == 0)
                {
                    int percent = (int)((double)i / totalSamples * 100);
                    progress?.Report(percent);
                }
            }

            // Flush remaining bits
            if (bitsInBuffer > 0 && byteIndex < output.Length)
            {
                output[byteIndex] = (byte)((bitBuffer << (8 - bitsInBuffer)) & 0xFF);
            }

            CompressionRatio = (double)(totalSamples * 2) / output.Length;

            progress?.Report(100);
            return output;
        }

        public short[] Decompress(byte[] data, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken)
        {
            if (data == null || data.Length < 14)
                throw new ArgumentException("Invalid compressed data");

            // Read header
            int totalSamples = BitConverter.ToInt32(data, 0);
            double mu = BitConverter.ToDouble(data, 4);
            short bitsPerSample = BitConverter.ToInt16(data, 12);
            int levels = 1 << bitsPerSample;

            double lnMuPlus1 = Math.Log(1 + mu);
            double maxSample = 32768.0;

            short[] samples = new short[totalSamples];

            long bitBuffer = 0;
            int bitsInBuffer = 0;
            int byteIndex = 14;
            uint mask = (uint)((1 << bitsPerSample) - 1);

            for (int i = 0; i < totalSamples; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return null;

                // Ensure we have enough bits in the buffer
                while (bitsInBuffer < bitsPerSample && byteIndex < data.Length)
                {
                    bitBuffer = (bitBuffer << 8) | data[byteIndex++];
                    bitsInBuffer += 8;
                }

                // Extract quantized value
                bitsInBuffer -= bitsPerSample;
                int quantized = (int)((bitBuffer >> bitsInBuffer) & mask);

                // Map from [0, levels-1] to [-1, 1]
                double compressed = (double)quantized / (levels - 1) * 2.0 - 1.0;

                // Apply mu-law expansion (inverse)
                double expanded;
                if (compressed >= 0)
                    expanded = (Math.Pow(1 + mu, compressed) - 1) / mu;
                else
                    expanded = -(Math.Pow(1 + mu, -compressed) - 1) / mu;

                // Scale back to short range
                short sample = (short)Math.Round(expanded * maxSample);
                samples[i] = Math.Max((short)-32768, Math.Min((short)32767, sample));

                // Report progress
                if (i % Math.Max(1, totalSamples / 100) == 0)
                {
                    int percent = (int)((double)i / totalSamples * 100);
                    progress?.Report(percent);
                }
            }

            progress?.Report(100);
            return samples;
        }
    }
}
