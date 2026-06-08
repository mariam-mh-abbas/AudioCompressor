using System;
using System.Collections.Generic;

namespace AudioCompressor.Models
{
    /// <summary>
    /// Result of a compression operation, containing metrics tracked in real-time.
    /// </summary>
    public class CompressionResult
    {
        /// <summary>
        /// Compressed byte data
        /// </summary>
        public byte[] CompressedData { get; set; }

        /// <summary>
        /// Original file size in bytes
        /// </summary>
        public long OriginalSizeBytes { get; set; }

        /// <summary>
        /// Compressed data size in bytes
        /// </summary>
        public long CompressedSizeBytes { get; set; }

        /// <summary>
        /// Compression ratio = Original / Compressed
        /// </summary>
        public double CompressionRatio
        {
            get => CompressedSizeBytes > 0 ? (double)OriginalSizeBytes / CompressedSizeBytes : 0;
        }

        /// <summary>
        /// Space saving percentage = (1 - Compressed/Original) * 100
        /// </summary>
        public double SpaceSavingPercent
        {
            get => OriginalSizeBytes > 0 ? (1.0 - (double)CompressedSizeBytes / OriginalSizeBytes) * 100 : 0;
        }

        /// <summary>
        /// Time taken for compression
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Algorithm used
        /// </summary>
        public AlgorithmType Algorithm { get; set; }

        /// <summary>
        /// Settings used for compression
        /// </summary>
        public CompressionSettings Settings { get; set; }

        /// <summary>
        /// Real-time data points for compression ratio chart
        /// (progress_percent, ratio_at_that_point)
        /// </summary>
        public List<Tuple<int, double>> CompressionRatioHistory { get; set; } = new List<Tuple<int, double>>();

        /// <summary>
        /// Real-time data points for processing speed chart
        /// (progress_percent, samples_per_second)
        /// </summary>
        public List<Tuple<int, double>> ProcessingSpeedHistory { get; set; } = new List<Tuple<int, double>>();
    }
}
