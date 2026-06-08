namespace AudioCompressor.Models
{
    /// <summary>
    /// Settings that control how compression is performed.
    /// Built by Member 3's settings panel, consumed by all algorithms.
    /// </summary>
    public class CompressionSettings
    {
        /// <summary>
        /// Selected compression algorithm type
        /// </summary>
        public AlgorithmType Algorithm { get; set; } = AlgorithmType.NonlinearQuantization;

        /// <summary>
        /// Sample rate for the audio (e.g. 44100, 22050, 11025)
        /// </summary>
        public int SampleRate { get; set; } = 44100;

        /// <summary>
        /// Number of quantization levels (e.g. 8, 16, 32, 64, 128, 256)
        /// Used by Nonlinear Quantization
        /// </summary>
        public int QuantizationLevels { get; set; } = 256;

        /// <summary>
        /// Mu parameter for mu-law compression (typical: 255)
        /// Used by Nonlinear Quantization (mu-law)
        /// </summary>
        public double Mu { get; set; } = 255.0;

        /// <summary>
        /// Step size for Delta Modulation and Adaptive Delta Modulation
        /// </summary>
        public int DeltaStepSize { get; set; } = 500;

        /// <summary>
        /// Minimum step size for Adaptive Delta Modulation
        /// </summary>
        public int MinStepSize { get; set; } = 50;

        /// <summary>
        /// Maximum step size for Adaptive Delta Modulation
        /// </summary>
        public int MaxStepSize { get; set; } = 5000;

        /// <summary>
        /// Prediction order for Predictive Differential Coding (1 = simple, 2+ = higher order)
        /// </summary>
        public int PredictionOrder { get; set; } = 1;

        /// <summary>
        /// Number of bits for DPCM quantization of differences
        /// </summary>
        public int DpcmBits { get; set; } = 4;

        /// <summary>
        /// Number of audio channels (1 = mono, 2 = stereo)
        /// </summary>
        public int Channels { get; set; } = 1;

        /// <summary>
        /// Bits per sample of the original audio (typically 16)
        /// </summary>
        public int BitsPerSample { get; set; } = 16;
    }

    /// <summary>
    /// Enum of all supported compression algorithms
    /// </summary>
    public enum AlgorithmType
    {
        NonlinearQuantization,
        DPCM,
        PredictiveDifferentialCoding,
        DeltaModulation,
        AdaptiveDeltaModulation
    }
}
