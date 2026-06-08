using AudioCompressor.Models;
using System;
using System.Threading;

namespace AudioCompressor.Interfaces
{
    /// <summary>
    /// Unified interface that every compression algorithm must implement.
    /// Each team member implements this for their algorithm.
    /// </summary>
    public interface IAudioCompressor
    {
        /// <summary>
        /// Name of the compression algorithm
        /// </summary>
        string AlgorithmName { get; }

        /// <summary>
        /// Compress audio samples and return compressed byte data.
        /// Reports progress via the IProgress interface.
        /// Supports cancellation via CancellationToken.
        /// </summary>
        byte[] Compress(short[] samples, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken);

        /// <summary>
        /// Decompress byte data back to audio samples.
        /// Reports progress via the IProgress interface.
        /// Supports cancellation via CancellationToken.
        /// </summary>
        short[] Decompress(byte[] data, CompressionSettings settings,
            IProgress<int> progress, CancellationToken cancellationToken);

        /// <summary>
        /// The compression ratio achieved after the last Compress() call.
        /// Ratio = original size / compressed size
        /// </summary>
        double CompressionRatio { get; }
    }
}
