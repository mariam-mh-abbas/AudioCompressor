using AudioCompressor.Interfaces;
using AudioCompressor.Models;
using System;
using System.Threading;

namespace AudioCompressor.Algorithms
{
    /// <summary>
    /// Delta Modulation.
    /// Member 4's algorithm.
    ///
    /// The simplest form of differential coding: each sample is compared
    /// to the predicted value. If the actual sample is greater, output 1;
    /// if less, output 0. The step size (delta) is fixed.
    /// Compression ratio is 16:1 for 16-bit audio (1 bit per sample).
    /// </summary>
    public class DeltaModulation : IAudioCompressor
    {
        public string AlgorithmName => "Delta Modulation";

        public double CompressionRatio { get; private set; }

        public byte[] Compress(short[] samples, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken)
        {
            int totalSamples = samples.Length;
            int delta = settings.DeltaStepSize;

            // Header: 8 bytes (sample count + delta)
            byte[] header = new byte[8];
            BitConverter.GetBytes(totalSamples).CopyTo(header, 0);
            BitConverter.GetBytes(delta).CopyTo(header, 4);

            // 1 bit per sample => totalSamples bits => (totalSamples+7)/8 bytes
            int dataBytes = (totalSamples + 7) / 8;
            byte[] output = new byte[header.Length + dataBytes];
            Array.Copy(header, output, header.Length);

            short predictor = 0; // start from 0
            long bitBuffer = 0;
            int bitsInBuffer = 0;
            int byteIndex = header.Length;

            for (int i = 0; i < totalSamples; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return null;

                // Compare actual sample with predicted value
                int bit;
                if (samples[i] > predictor)
                {
                    bit = 1;
                    predictor = (short)(predictor + delta);
                }
                else
                {
                    bit = 0;
                    predictor = (short)(predictor - delta);
                }

                // Clamp predictor
                predictor = Math.Max((short)-32768, Math.Min((short)32767, predictor));

                // Pack bit
                bitBuffer = (bitBuffer << 1) | (uint)bit;
                bitsInBuffer++;

                if (bitsInBuffer == 8)
                {
                    output[byteIndex++] = (byte)bitBuffer;
                    bitBuffer = 0;
                    bitsInBuffer = 0;
                }

                // Report progress
                if (i % Math.Max(1, totalSamples / 100) == 0)
                {
                    int percent = (int)((double)i / totalSamples * 100);
                    progress?.Report(percent);
                }
            }

            // Flush remaining bits
            if (bitsInBuffer > 0)
            {
                output[byteIndex] = (byte)(bitBuffer << (8 - bitsInBuffer));
            }

            CompressionRatio = (double)(totalSamples * 2) / output.Length;
            progress?.Report(100);
            return output;
        }

        public short[] Decompress(byte[] data, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken)
        {
            if (data == null || data.Length < 8)
                throw new ArgumentException("Invalid compressed data");

            // Read header
            int totalSamples = BitConverter.ToInt32(data, 0);
            int delta = BitConverter.ToInt32(data, 4);

            short[] samples = new short[totalSamples];
            short predictor = 0;
            int byteIndex = 8;

            for (int i = 0; i < totalSamples; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return null;

                // Extract bit
                int bitIndex = i % 8;
                if (bitIndex == 0 && i > 0)
                    byteIndex++;

                int bit = (data[byteIndex] >> (7 - bitIndex)) & 1;

                // Reconstruct
                if (bit == 1)
                    predictor = (short)(predictor + delta);
                else
                    predictor = (short)(predictor - delta);

                // Clamp
                predictor = Math.Max((short)-32768, Math.Min((short)32767, predictor));
                samples[i] = predictor;

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
