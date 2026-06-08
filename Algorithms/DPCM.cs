using AudioCompressor.Interfaces;
using AudioCompressor.Models;
using System;
using System.Threading;

namespace AudioCompressor.Algorithms
{
    /// <summary>
    /// Differential Pulse Code Modulation (DPCM).
    /// Member 2's algorithm.
    ///
    /// DPCM encodes the difference between consecutive samples instead of
    /// the absolute values. Since differences are typically smaller than
    /// the original values, they can be represented with fewer bits.
    /// Quantization of differences reduces the number of bits needed.
    /// </summary>
    public class DPCM : IAudioCompressor
    {
        public string AlgorithmName => "DPCM (Differential Pulse Code Modulation)";

        public double CompressionRatio { get; private set; }

        public byte[] Compress(short[] samples, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken)
        {
            int totalSamples = samples.Length;
            int bits = settings.DpcmBits; // bits per difference (e.g. 4)
            int levels = 1 << bits;       // quantization levels
            int maxDiff = levels / 2 - 1; // maximum positive difference index

            // The max representable difference in original scale
            // For 16-bit audio, max possible diff = 65535
            // We map this to 'levels' quantization steps
            double stepSize = 65536.0 / levels;

            // Header: 10 bytes (sample count + first sample + bits per diff)
            byte[] header = new byte[10];
            BitConverter.GetBytes(totalSamples).CopyTo(header, 0);
            BitConverter.GetBytes(samples[0]).CopyTo(header, 4);
            BitConverter.GetBytes((short)bits).CopyTo(header, 8);

            // Calculate output size for packed bits
            int totalBits = totalSamples * bits; // first sample is in header
            int dataBytes = (totalBits + 7) / 8;

            byte[] output = new byte[header.Length + dataBytes];
            Array.Copy(header, output, header.Length);

            long bitBuffer = 0;
            int bitsInBuffer = 0;
            int byteIndex = header.Length;
            short previous = samples[0];

            for (int i = 1; i < totalSamples; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return null;

                // Calculate difference
                int diff = samples[i] - previous;

                // Quantize the difference
                int quantizedDiff = (int)Math.Round(diff / stepSize);
                quantizedDiff = Math.Max(-maxDiff - 1, Math.Min(maxDiff, quantizedDiff));

                // Map to unsigned index [0, levels-1]
                int index = quantizedDiff + maxDiff + 1;

                // Pack into bit stream
                bitBuffer = (bitBuffer << bits) | (uint)index;
                bitsInBuffer += bits;

                while (bitsInBuffer >= 8)
                {
                    bitsInBuffer -= 8;
                    if (byteIndex < output.Length)
                    {
                        output[byteIndex++] = (byte)((bitBuffer >> bitsInBuffer) & 0xFF);
                    }
                }

                // Reconstruct to update predictor
                int reconstructedDiff = (int)((index - maxDiff - 1) * stepSize);
                previous = (short)(previous + reconstructedDiff);

                // Report progress
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
            if (data == null || data.Length < 10)
                throw new ArgumentException("Invalid compressed data");

            // Read header
            int totalSamples = BitConverter.ToInt32(data, 0);
            short firstSample = BitConverter.ToInt16(data, 4);
            short bits = BitConverter.ToInt16(data, 8);

            int levels = 1 << bits;
            int maxDiff = levels / 2 - 1;
            double stepSize = 65536.0 / levels;

            short[] samples = new short[totalSamples];
            samples[0] = firstSample;

            long bitBuffer = 0;
            int bitsInBuffer = 0;
            int byteIndex = 10;
            uint mask = (uint)((1 << bits) - 1);
            short previous = firstSample;

            for (int i = 1; i < totalSamples; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return null;

                // Ensure enough bits
                while (bitsInBuffer < bits && byteIndex < data.Length)
                {
                    bitBuffer = (bitBuffer << 8) | data[byteIndex++];
                    bitsInBuffer += 8;
                }

                // Extract index
                bitsInBuffer -= bits;
                int index = (int)((bitBuffer >> bitsInBuffer) & mask);

                // Reconstruct difference
                int quantizedDiff = index - maxDiff - 1;
                int diff = (int)(quantizedDiff * stepSize);

                // Reconstruct sample
                previous = (short)(previous + diff);
                previous = Math.Max((short)-32768, Math.Min((short)32767, previous));
                samples[i] = previous;

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
