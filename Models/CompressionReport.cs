using System;

namespace AudioCompressor.Models
{
    /// <summary>
    /// Final report after compression is complete (Requirement 10).
    /// Built by Member 5.
    /// </summary>
    public class CompressionReport
    {
        /// <summary>
        /// Original file info
        /// </summary>
        public AudioFileInfo OriginalFileInfo { get; set; }

        /// <summary>
        /// Original file size formatted
        /// </summary>
        public string OriginalSizeFormatted { get; set; }

        /// <summary>
        /// Compressed file size formatted
        /// </summary>
        public string CompressedSizeFormatted { get; set; }

        /// <summary>
        /// Space saving percentage
        /// </summary>
        public double SpaceSavingPercent { get; set; }

        /// <summary>
        /// Compression ratio
        /// </summary>
        public double CompressionRatio { get; set; }

        /// <summary>
        /// Time elapsed during compression
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Algorithm used
        /// </summary>
        public string AlgorithmName { get; set; }

        /// <summary>
        /// Settings used for compression
        /// </summary>
        public CompressionSettings Settings { get; set; }

        /// <summary>
        /// Convert report to a text format for export
        /// </summary>
        public override string ToString()
        {
            string nl = Environment.NewLine;
            string separator = new string('=', 50);
            return
                separator + nl +
                "        AUDIO COMPRESSION REPORT" + nl +
                separator + nl + nl +
                "FILE INFORMATION:" + nl +
                $"  Original File:      {OriginalFileInfo?.FileName ?? "N/A"}" + nl +
                $"  Original Size:      {OriginalSizeFormatted}" + nl +
                $"  Compressed Size:    {CompressedSizeFormatted}" + nl +
                $"  Duration:           {OriginalFileInfo?.DurationFormatted ?? "N/A"}" + nl +
                $"  Sample Rate:        {OriginalFileInfo?.SampleRate ?? 0} Hz" + nl +
                $"  Channels:           {OriginalFileInfo?.Channels ?? 0}" + nl +
                $"  Bits Per Sample:    {OriginalFileInfo?.BitsPerSample ?? 0}" + nl +
                $"  Encoding:           {OriginalFileInfo?.Encoding ?? "N/A"}" + nl + nl +
                "COMPRESSION RESULTS:" + nl +
                $"  Algorithm:          {AlgorithmName}" + nl +
                $"  Compression Ratio:  {CompressionRatio:F2} : 1" + nl +
                $"  Space Saving:       {SpaceSavingPercent:F2}%" + nl +
                $"  Time Elapsed:       {ElapsedTime.TotalSeconds:F3} seconds" + nl + nl +
                "COMPRESSION SETTINGS:" + nl +
                $"  Sample Rate:        {Settings?.SampleRate ?? 0} Hz" + nl +
                $"  Quantization Levels:{Settings?.QuantizationLevels ?? 0}" + nl +
                $"  Mu (mu-law):        {Settings?.Mu ?? 0}" + nl +
                $"  Delta Step Size:    {Settings?.DeltaStepSize ?? 0}" + nl +
                $"  Prediction Order:   {Settings?.PredictionOrder ?? 0}" + nl +
                $"  DPCM Bits:          {Settings?.DpcmBits ?? 0}" + nl + nl +
                separator;
        }
    }
}
