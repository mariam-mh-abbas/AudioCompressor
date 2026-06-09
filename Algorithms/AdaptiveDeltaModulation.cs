using AudioCompressor.Interfaces;
using AudioCompressor.Models;
using System;
using System.Threading;

namespace AudioCompressor.Algorithms
{
    
    public class AdaptiveDeltaModulation : IAudioCompressor
    {
        public string AlgorithmName => "Adaptive Delta Modulation";

        public double CompressionRatio { get; private set; }

        
        private const double Multiplier = 1.5;
        
        private const double Divisor = 1.5;

        public byte[] Compress(short[] samples, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken)
        {
            int totalSamples = samples.Length;
            int channels = settings.Channels;
            int samplesPerChannel = totalSamples / channels;
            int minStep = settings.MinStepSize;
            int maxStep = settings.MaxStepSize;
            double startStep = settings.DeltaStepSize;

            // Step 1: Separate channels 
            short[][] channelSamples = new short[channels][];
            for (int ch = 0; ch < channels; ch++)
            {
                channelSamples[ch] = new short[samplesPerChannel];
                for (int i = 0; i < samplesPerChannel; i++)
                {
                    int interleavedIndex = i * channels + ch;
                    channelSamples[ch][i] = samples[interleavedIndex];
                }
            }

            // Step 2: Compress each channel independently
            byte[][] compressedChannels = new byte[channels][];
            for (int ch = 0; ch < channels; ch++)
            {
                compressedChannels[ch] = CompressChannel(
                    channelSamples[ch], minStep, maxStep, startStep,
                    progress, cancellationToken, ch, channels);

                if (compressedChannels[ch] == null)
                    return null;
            }

            
            byte[] header = new byte[28];
            BitConverter.GetBytes(samplesPerChannel).CopyTo(header, 0);
            BitConverter.GetBytes(channels).CopyTo(header, 4);
            BitConverter.GetBytes(minStep).CopyTo(header, 8);
            BitConverter.GetBytes(maxStep).CopyTo(header, 12);
            BitConverter.GetBytes(startStep).CopyTo(header, 16);

            
            int dataLength = 4 * channels;
            for (int ch = 0; ch < channels; ch++)
                dataLength += compressedChannels[ch].Length;

            byte[] output = new byte[header.Length + dataLength];
            Array.Copy(header, output, header.Length);

           
            int offset = header.Length;
            for (int ch = 0; ch < channels; ch++)
            {
                BitConverter.GetBytes(compressedChannels[ch].Length).CopyTo(output, offset);
                offset += 4;
                Array.Copy(compressedChannels[ch], 0, output, offset, compressedChannels[ch].Length);
                offset += compressedChannels[ch].Length;
            }

            CompressionRatio = (double)(totalSamples * 2) / output.Length;
            progress?.Report(100);
            return output;
        }

        private byte[] CompressChannel(short[] samples, int minStep, int maxStep,
            double startStep, IProgress<int> progress, CancellationToken ct,
            int channelIndex, int totalChannels)
        {
            int totalSamples = samples.Length;
            
            int dataBytes = (totalSamples + 7) / 8;
            byte[] output = new byte[dataBytes];

            double currentStep = startStep;
            int predictor = 0;
            int previousBit = -1;
            long bitBuffer = 0;
            int bitsInBuffer = 0;
            int byteIndex = 0;

            for (int i = 0; i < totalSamples; i++)
            {
                if (ct.IsCancellationRequested)
                    return null;


                int bit;
                if (samples[i] > predictor)
                {
                    bit = 1;
                    predictor = Math.Max(-32768, Math.Min(32767, predictor + (int)currentStep));
                }
                else
                {
                    bit = 0;
                    predictor = Math.Max(-32768, Math.Min(32767, predictor - (int)currentStep));
                }


                
                if (previousBit != -1)
                {
                    bool sameDirection = (bit == previousBit);

                    if (sameDirection)
                    {
                        
                        currentStep = currentStep * Multiplier;
                        if (currentStep > maxStep)
                            currentStep = maxStep;
                    }
                    else
                    {
                        
                        currentStep = currentStep / Divisor;
                        if (currentStep < minStep)
                            currentStep = minStep;
                    }
                }
                previousBit = bit;

                
                bitBuffer = (bitBuffer << 1) | bit;
                bitsInBuffer++;

                
                if (bitsInBuffer == 8)
                {
                    output[byteIndex] = (byte)bitBuffer;
                    byteIndex++;
                    bitBuffer = 0;
                    bitsInBuffer = 0;
                }

                
                if (channelIndex == 0 && i % Math.Max(1, totalSamples / 100) == 0)
                {
                    int percent = (int)((double)i / totalSamples * 100);
                    progress?.Report(percent);
                }
            }

            
            if (bitsInBuffer > 0)
            {
                output[byteIndex] = (byte)(bitBuffer << (8 - bitsInBuffer));
            }

            return output;
        }

        public short[] Decompress(byte[] data, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken)
        {
            if (data == null || data.Length < 28)
                throw new ArgumentException("Invalid compressed data");

            
            int samplesPerChannel = BitConverter.ToInt32(data, 0);
            int channels = BitConverter.ToInt32(data, 4);
            int minStep = BitConverter.ToInt32(data, 8);
            int maxStep = BitConverter.ToInt32(data, 12);
            double startStep = BitConverter.ToDouble(data, 16);

            // Step 1: Decompress each channel independently
            short[][] channelSamples = new short[channels][];
            int offset = 28;

            for (int ch = 0; ch < channels; ch++)
            {
                int channelDataLength = BitConverter.ToInt32(data, offset);
                offset += 4;

                byte[] channelData = new byte[channelDataLength];
                Array.Copy(data, offset, channelData, 0, channelDataLength);
                offset += channelDataLength;

                channelSamples[ch] = DecompressChannel(
                    channelData, samplesPerChannel, minStep, maxStep, startStep,
                    progress, cancellationToken, ch, channels);

                if (channelSamples[ch] == null)
                    return null;
            }

            // Step 2: Re-interleave channels 
            short[] result = new short[samplesPerChannel * channels];
            for (int i = 0; i < samplesPerChannel; i++)
            {
                for (int ch = 0; ch < channels; ch++)
                {
                    int interleavedIndex = i * channels + ch;
                    result[interleavedIndex] = channelSamples[ch][i];
                }
            }

            progress?.Report(100);
            return result;
        }

        private short[] DecompressChannel(byte[] data, int totalSamples, int minStep,
            int maxStep, double startStep, IProgress<int> progress,
            CancellationToken ct, int channelIndex, int totalChannels)
        {
            short[] samples = new short[totalSamples];
            int predictor = 0;
            int previousBit = -1;
            double currentStep = startStep;

            for (int i = 0; i < totalSamples; i++)
            {
                if (ct.IsCancellationRequested)
                    return null;

                
                int byteIndex = i / 8;
                int bitPosition = 7 - (i % 8);
                int bit = (data[byteIndex] >> bitPosition) & 1;

                
                if (bit == 1)
                    predictor = predictor + (int)currentStep;
                else
                    predictor = predictor - (int)currentStep;

               
                if (predictor > 32767) predictor = 32767;
                if (predictor < -32768) predictor = -32768;

                samples[i] = (short)predictor;

                
                if (previousBit != -1)
                {
                    bool sameDirection = (bit == previousBit);

                    if (sameDirection)
                    {
                        currentStep = currentStep * Multiplier;
                        if (currentStep > maxStep)
                            currentStep = maxStep;
                    }
                    else
                    {
                        currentStep = currentStep / Divisor;
                        if (currentStep < minStep)
                            currentStep = minStep;
                    }
                }
                previousBit = bit;

                
                if (channelIndex == 0 && i % Math.Max(1, totalSamples / 100) == 0)
                {
                    int percent = (int)((double)i / totalSamples * 100);
                    progress?.Report(percent);
                }
            }

            return samples;
        }
    }
}