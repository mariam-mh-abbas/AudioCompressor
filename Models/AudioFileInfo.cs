using System;

namespace AudioCompressor.Models
{
    /// <summary>
    /// Contains all metadata about an audio file.
    /// Built by Member 2 — first priority since everyone needs it.
    /// </summary>
    public class AudioFileInfo
    {
        /// <summary>
        /// Full file path
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// File name only
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        public long FileSizeBytes { get; set; }

        /// <summary>
        /// File size formatted as a human-readable string (KB, MB)
        /// </summary>
        public string FileSizeFormatted
        {
            get
            {
                if (FileSizeBytes < 1024)
                    return $"{FileSizeBytes} B";
                else if (FileSizeBytes < 1024 * 1024)
                    return $"{FileSizeBytes / 1024.0:F2} KB";
                else
                    return $"{FileSizeBytes / (1024.0 * 1024.0):F2} MB";
            }
        }

        /// <summary>
        /// Duration of the audio file
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Duration formatted as mm:ss
        /// </summary>
        public string DurationFormatted
        {
            get => $"{Duration.Minutes:D2}:{Duration.Seconds:D2}";
        }

        /// <summary>
        /// Sample rate in Hz (e.g. 44100)
        /// </summary>
        public int SampleRate { get; set; }

        /// <summary>
        /// Number of audio channels (1 = mono, 2 = stereo)
        /// </summary>
        public int Channels { get; set; }

        /// <summary>
        /// Bits per sample (e.g. 16)
        /// </summary>
        public int BitsPerSample { get; set; }

        /// <summary>
        /// Bit rate in kbps
        /// </summary>
        public int BitRate { get; set; }

        /// <summary>
        /// Encoding format (e.g. PCM, IEEE Float)
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// Total number of samples in the audio
        /// </summary>
        public long TotalSamples { get; set; }
    }
}
