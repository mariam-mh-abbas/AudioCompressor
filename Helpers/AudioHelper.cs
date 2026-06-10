using AudioCompressor.Models;
using NAudio.Wave;
using System;
using System.IO;

namespace AudioCompressor.Helpers
{
    /// <summary>
    /// Helper class for reading WAV audio files, extracting properties,
    /// and writing WAV files. Uses NAudio library.
    /// </summary>
    public static class AudioHelper
    {
        /// <summary>
        /// Read audio file info using NAudio
        /// </summary>
        public static AudioFileInfo GetAudioFileInfo(string filePath)
        {
            var info = new AudioFileInfo
            {
                FilePath = filePath,
                FileName = Path.GetFileName(filePath),
                FileSizeBytes = new FileInfo(filePath).Length
            };

            using (var reader = new AudioFileReader(filePath))
            {
                info.Duration = reader.TotalTime;
                info.SampleRate = reader.WaveFormat.SampleRate;
                info.Channels = reader.WaveFormat.Channels;
                info.BitsPerSample = reader.WaveFormat.BitsPerSample;
                info.BitRate = reader.WaveFormat.AverageBytesPerSecond * 8 / 1000;
                info.Encoding = reader.WaveFormat.Encoding.ToString();
                info.TotalSamples = (long)(reader.TotalTime.TotalSeconds * reader.WaveFormat.SampleRate * reader.WaveFormat.Channels);
            }

            return info;
        }

        public static short[] ReadAudioSamples(string filePath)
        {
            using (var reader = new AudioFileReader(filePath))
            {
                var waveFormat = new WaveFormat(reader.WaveFormat.SampleRate, 16, reader.WaveFormat.Channels);

                using (var resampler = new MediaFoundationResampler(reader, waveFormat))
                {
                    // نقرأ البيانات بطريقة ديناميكية عشان نتجنب مشكلة حجم المصفوفة
                    var ms = new MemoryStream();
                    byte[] tempBuffer = new byte[4096];
                    int read;
                    while ((read = resampler.Read(tempBuffer, 0, tempBuffer.Length)) > 0)
                    {
                        ms.Write(tempBuffer, 0, read);
                    }

                    byte[] buffer = ms.ToArray();
                    int totalSamples = buffer.Length / 2;
                    short[] allSamples = new short[totalSamples];
                    Buffer.BlockCopy(buffer, 0, allSamples, 0, buffer.Length);

                    return allSamples;
                }
            }
        }

        /// <summary>
        /// Write short samples to a WAV file
        /// </summary>
        public static void WriteWavFile(string filePath, short[] samples, int sampleRate, int channels = 1)
        {
            var waveFormat = new WaveFormat(sampleRate, 16, channels);
            using (var writer = new WaveFileWriter(filePath, waveFormat))
            {
                byte[] buffer = new byte[samples.Length * 2];
                Buffer.BlockCopy(samples, 0, buffer, 0, buffer.Length);
                writer.Write(buffer, 0, buffer.Length);
            }
        }

        /// <summary>
        /// Write compressed byte data to a file with a custom header
        /// </summary>
        public static void WriteCompressedFile(string filePath, byte[] data, AlgorithmType algorithm, CompressionSettings settings)
        {
            // Custom file format: .afc (Audio File Compressed)
            // Header: "AFC" + version + algorithm + settings + data
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (var bw = new BinaryWriter(fs))
            {
                // Magic number
                bw.Write(new char[] { 'A', 'F', 'C' });

                // Version
                bw.Write((byte)1);

                // Algorithm
                bw.Write((int)algorithm);

                // Settings
                bw.Write(settings.SampleRate);
                bw.Write(settings.QuantizationLevels);
                bw.Write(settings.Mu);
                bw.Write(settings.DeltaStepSize);
                bw.Write(settings.MinStepSize);
                bw.Write(settings.MaxStepSize);
                bw.Write(settings.PredictionOrder);
                bw.Write(settings.DpcmBits);
                bw.Write(settings.Channels);
                bw.Write(settings.BitsPerSample);

                // Data length
                bw.Write(data.Length);

                // Data
                bw.Write(data);
            }
        }

        /// <summary>
        /// Read compressed file and return the data and settings
        /// </summary>
        public static (byte[] data, AlgorithmType algorithm, CompressionSettings settings) ReadCompressedFile(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                // Magic number
                char[] magic = br.ReadChars(3);
                if (magic[0] != 'A' || magic[1] != 'F' || magic[2] != 'C')
                    throw new InvalidDataException("Not a valid compressed audio file");

                // Version
                byte version = br.ReadByte();

                // Algorithm
                AlgorithmType algorithm = (AlgorithmType)br.ReadInt32();

                // Settings
                var settings = new CompressionSettings
                {
                    Algorithm = algorithm,
                    SampleRate = br.ReadInt32(),
                    QuantizationLevels = br.ReadInt32(),
                    Mu = br.ReadDouble(),
                    DeltaStepSize = br.ReadInt32(),
                    MinStepSize = br.ReadInt32(),
                    MaxStepSize = br.ReadInt32(),
                    PredictionOrder = br.ReadInt32(),
                    DpcmBits = br.ReadInt32(),
                    Channels = br.ReadInt32(),
                    BitsPerSample = br.ReadInt32()
                };

                // Data
                int dataLength = br.ReadInt32();
                byte[] data = br.ReadBytes(dataLength);

                return (data, algorithm, settings);
            }
        }
    }
}
