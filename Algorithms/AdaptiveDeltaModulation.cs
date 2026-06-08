using AudioCompressor.Interfaces;
using AudioCompressor.Models;
using System;
using System.Threading;

namespace AudioCompressor.Algorithms
{
    /// <summary>
    /// Adaptive Delta Modulation (ADM).
    /// Member 5's algorithm.
    ///
    /// Like Delta Modulation, but the step size adapts based on the
    /// signal characteristics. When consecutive bits are the same
    /// (indicating slope overload), the step size increases.
    /// When bits alternate (indicating granular noise), the step decreases.
    /// This provides better quality than fixed Delta Modulation.
    /// </summary>
    public class AdaptiveDeltaModulation : IAudioCompressor
    {
        public string AlgorithmName => "Adaptive Delta Modulation";

        public double CompressionRatio { get; private set; }

        // Adaptation factor - multiplies step on consecutive same bits
        private const double Multiplier = 1.5;
        // Adaptation factor - divides step on alternating bits
        private const double Divisor = 1.2;

        public byte[] Compress(short[] samples, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken)
        {
            int totalSamples = samples.Length;
            int minStep = settings.MinStepSize;
            int maxStep = settings.MaxStepSize;
            double currentStep = settings.DeltaStepSize;

            // Header: 12 bytes (sample count + minStep + maxStep + initialStep)
            byte[] header = new byte[16];
            BitConverter.GetBytes(totalSamples).CopyTo(header, 0);
            BitConverter.GetBytes(minStep).CopyTo(header, 4);
            BitConverter.GetBytes(maxStep).CopyTo(header, 8);
            BitConverter.GetBytes(currentStep).CopyTo(header, 12);

            // 1 bit per sample + 2 bytes per sample for step tracking
            // Actually we store 1 bit per sample + header contains step info
            // For ADM we pack 1 bit per sample but also store step adaptation info in header
            int dataBytes = (totalSamples + 7) / 8;
            byte[] output = new byte[header.Length + dataBytes];
            Array.Copy(header, output, header.Length);

            short predictor = 0;
            int previousBit = -1;
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
                    predictor = (short)(predictor + (int)currentStep);
                }
                else
                {
                    bit = 0;
                    predictor = (short)(predictor - (int)currentStep);
                }

                // Clamp predictor
                predictor = Math.Max((short)-32768, Math.Min((short)32767, predictor));

                // Adapt step size
                if (previousBit != -1)
                {
                    if (bit == previousBit)
                    {
                        // Consecutive same bits: slope overload, increase step
                        currentStep = Math.Min(maxStep, currentStep * Multiplier);
                    }
                    else
                    {
                        // Alternating bits: granular noise, decrease step
                        currentStep = Math.Max(minStep, currentStep / Divisor);
                    }
                }
                previousBit = bit;

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
            if (data == null || data.Length < 16)
                throw new ArgumentException("Invalid compressed data");

            // Read header
            int totalSamples = BitConverter.ToInt32(data, 0);
            int minStep = BitConverter.ToInt32(data, 4);
            int maxStep = BitConverter.ToInt32(data, 8);
            double currentStep = BitConverter.ToDouble(data, 12);

            short[] samples = new short[totalSamples];
            short predictor = 0;
            int previousBit = -1;
            int byteIndex = 16;

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
                    predictor = (short)(predictor + (int)currentStep);
                else
                    predictor = (short)(predictor - (int)currentStep);

                // Clamp
                predictor = Math.Max((short)-32768, Math.Min((short)32767, predictor));
                samples[i] = predictor;

                // Adapt step size (same logic as compression)
                if (previousBit != -1)
                {
                    if (bit == previousBit)
                    {
                        currentStep = Math.Min(maxStep, currentStep * Multiplier);
                    }
                    else
                    {
                        currentStep = Math.Max(minStep, currentStep / Divisor);
                    }
                }
                previousBit = bit;

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
