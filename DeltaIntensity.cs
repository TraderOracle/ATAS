namespace ATAS.Indicators.Technical
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Resources;
    using System.Runtime.Intrinsics.X86;
    using System.Windows.Media;
    using ATAS.Indicators;
    using ATAS.Indicators.Technical.Properties;
    using OFT.Attributes;
    using OFT.Rendering.Context;
    using OFT.Rendering.Settings;
    using OFT.Rendering.Tools;
     
    [DisplayName("Delta Intensity")]
    public class DeltaIntensity : Indicator
    {
        private IndicatorCandle prevCandle;
        private int iBigTrades = 25000;

        [Range(1, 100000)]
        public int Bigtrades_Threshold
        {
            get => iBigTrades;
            set
            {
                if (value < 1)
                    return;
                iBigTrades = value;
                RecalculateValues();
            }
        }

        private readonly ValueDataSeries _VolSecNeg = new("Volume/Second Negative")
        {
            Color = Colors.Red,
            VisualType = VisualMode.Histogram
        };

        private readonly ValueDataSeries _VolSecPos = new("Volume/Second Positive")
        {
            Color = Colors.LimeGreen,
            VisualType = VisualMode.Histogram
        };

        private readonly ValueDataSeries _negSeries = new("Negative Delta")
        {
            Color = Colors.DarkOrange,
            VisualType = VisualMode.Histogram
        };

        private readonly ValueDataSeries _posSeries = new("Positive Delta")
        {
            Color = Colors.DarkGreen,
            VisualType = VisualMode.Histogram
        };

        public DeltaIntensity() :
            base(true)
        {
            Panel = IndicatorDataProvider.NewPanel;
            DataSeries[0] = _posSeries;
            DataSeries.Add(_negSeries);
            DataSeries.Add(_VolSecPos);
            DataSeries.Add(_VolSecNeg);
        }

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar < 2)
                return;

            var candle = GetCandle(bar);

            prevCandle = candle;
            value = candle.Close;

            var candleSeconds = Convert.ToDecimal((candle.LastTime - candle.Time).TotalSeconds);
            if (candleSeconds is 0)
                candleSeconds = 1;
            var volPerSecond = candle.Volume / candleSeconds;

            var deltaPer = candle.Delta > 0 ? (candle.Delta / candle.MaxDelta) : (candle.Delta / candle.MinDelta);
            var deltaIntense = Math.Abs((candle.Delta * deltaPer) * volPerSecond);
            var deltaShaved = candle.Delta * deltaPer;

            if (deltaIntense > iBigTrades)
            {
                if (candle.Delta > 0)
                    _VolSecPos[bar] = deltaShaved; // volPerSecond * 1000;
                else
                    _VolSecNeg[bar] = deltaShaved * -1; // volPerSecond * 1000;
            }
            else
            {
                if (candle.Delta > 0)
                    _posSeries[bar] = deltaShaved;
                else
                    _negSeries[bar] = deltaShaved * -1;
            }

        }

    }
}