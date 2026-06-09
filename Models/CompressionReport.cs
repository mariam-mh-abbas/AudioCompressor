
using System;

namespace AudioCompressor.Models
{
    
    public class CompressionReport
    {
        public AudioFileInfo OriginalFileInfo { get; set; }
        public string OriginalSizeFormatted { get; set; }
        public string CompressedSizeFormatted { get; set; }
        public double SpaceSavingPercent { get; set; }
        public double CompressionRatio { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public string AlgorithmName { get; set; }
        public CompressionSettings Settings { get; set; }

       
        public double SNR { get; set; }

        public override string ToString()
        {
            string nl = Environment.NewLine;
            string separator = new string('=', 50);

            
            string channelDisplay;
            int ch = OriginalFileInfo?.Channels ?? 0;
            if (ch == 1)
                channelDisplay = "Mono (1)";
            else if (ch == 2)
                channelDisplay = "Stereo (2)";
            else
                channelDisplay = $"{ch} channels";

            
            string qualityLabel;
            if (SNR >= 30)
                qualityLabel = "Good";
            else if (SNR >= 20)
                qualityLabel = "Acceptable";
            else if (SNR >= 10)
                qualityLabel = "Poor";
            else
                qualityLabel = "Very Poor";

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
                $"  Channels:           {channelDisplay}" + nl +
                $"  Bits Per Sample:    {OriginalFileInfo?.BitsPerSample ?? 0}" + nl +
                $"  Sample Bit Rate:           {OriginalFileInfo?.BitRate ?? 0} kbps" + nl +
                $"  Encoding:           {OriginalFileInfo?.Encoding ?? "N/A"}" + nl + nl +

                "COMPRESSION RESULTS:" + nl +
                $"  Algorithm:          {AlgorithmName}" + nl +
                $"  Compression Ratio:  {CompressionRatio:F2} : 1" + nl +
                $"  Space Saving:       {SpaceSavingPercent:F2}%" + nl +
                $"  SNR:                {SNR:F2} dB ({qualityLabel})" + nl +
                $"  Time Elapsed:       {ElapsedTime.TotalSeconds:F3} seconds" + nl + nl +

                "COMPRESSION SETTINGS:" + nl +
                $"  Sample Rate:        {Settings?.SampleRate ?? 0} Hz" + nl +
                GetAlgorithmSpecificSettings(Settings) + nl +
                separator;
        }

        
        private string GetAlgorithmSpecificSettings(CompressionSettings s)
        {
            if (s == null) return "";
            string nl = Environment.NewLine;

            
            int sampleBitRate = s.SampleRate * s.Channels * s.BitsPerSample;
            string sampleBitRateStr = sampleBitRate >= 1000
                ? $"{(double)sampleBitRate / 1000:F1} kbps ({sampleBitRate} bps)"
                : $"{sampleBitRate} bps";

            string header = $"  Sample Bit Rate:    {sampleBitRateStr}" + nl;

            switch (s.Algorithm)
            {
                case AlgorithmType.NonlinearQuantization:
                    return header +
                        $"  Quantization Levels:{s.QuantizationLevels}" + nl +
                        $"  Mu (mu-law):        {s.Mu}";

                case AlgorithmType.DPCM:
                    return header +
                        $"  DPCM Bits:          {s.DpcmBits}";

                case AlgorithmType.PredictiveDifferentialCoding:
                    return header +
                        $"  DPCM Bits:          {s.DpcmBits}" + nl +
                        $"  Prediction Order:   {s.PredictionOrder}";

                case AlgorithmType.DeltaModulation:
                    return header +
                        $"  Delta Step Size:    {s.DeltaStepSize}";

                case AlgorithmType.AdaptiveDeltaModulation:
                    return header +
                        $"  Initial Step Size:  {s.DeltaStepSize}" + nl +
                        $"  Min Step Size:      {s.MinStepSize}" + nl +
                        $"  Max Step Size:      {s.MaxStepSize}";

                default:
                    return header;
            }
        }
    }
}

