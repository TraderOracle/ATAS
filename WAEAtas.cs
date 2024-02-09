namespace ATAS.Indicators.Technical
{
    using System.ComponentModel;
    using System.Windows.Media;
    using ATAS.Indicators;
    using ATAS.Indicators.Technical.Properties;
    using OFT.Attributes;
    using OFT.Rendering.Context;
    using OFT.Rendering.Settings;
    using OFT.Rendering.Tools;

    [DisplayName("WaddahExplosion")]
    public class WaddahExplosion : Indicator
    {
        private readonly int Sensitivity = 150;
        private readonly EMA fastEma = new EMA() { Period = 20 };
        private readonly EMA slowEma = new EMA() { Period = 40 };
        private readonly StdDev _dev = new StdDev() { Period = 20 };
        private readonly BollingerBands _bb = new BollingerBands() 
        {
            Period = 20,
            Shift = 0,
            Width = 2
        };

        private readonly ValueDataSeries _negSeries = new("Regular Up Bar")
        {
            Color = Colors.Red,
            VisualType = VisualMode.Histogram
        };
        private readonly ValueDataSeries _negLess = new("Less Up Bar")
        {
            Color = Colors.Orange,
            VisualType = VisualMode.Histogram
        };

        private readonly ValueDataSeries _posSeries = new("Regular Down Bar")
        {
            Color = Colors.Lime,
            VisualType = VisualMode.Histogram
        };
        private readonly ValueDataSeries _posLess = new("Less Down Bar")
        {
            Color = Colors.Green,
            VisualType = VisualMode.Histogram
        };

        private readonly ValueDataSeries _s1 = new("S1 EMA")
        {
            Color = Colors.White,
            VisualType = VisualMode.Histogram
        };

        public WaddahExplosion() : 
            base(true)
        {
            Panel = IndicatorDataProvider.NewPanel;
            DataSeries[0] = _posSeries;
            DataSeries.Add(_negSeries);
            DataSeries.Add(_negLess);
            DataSeries.Add(_posLess);
            DataSeries.Add(_s1);
        }

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar < 5)
                return;

            var candle = GetCandle(bar);
            value = candle.Close;

            var bb_mid = ((ValueDataSeries)_bb.DataSeries[0])[bar]; // mid
            var bb_top = ((ValueDataSeries)_bb.DataSeries[1])[bar]; // top
            var bb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[bar]; // bottom

            var dev = 2 * _dev.Calculate(bar, value);
            var e1 = bb_top - bb_bottom;

            fastEma.Calculate(bar, value);
            var fast = ((ValueDataSeries)fastEma.DataSeries[0])[bar];
            var fastM = ((ValueDataSeries)fastEma.DataSeries[0])[bar - 1];
            var fastN = ((ValueDataSeries)fastEma.DataSeries[0])[bar - 2];

            slowEma.Calculate(bar, value);
            var slow = ((ValueDataSeries)slowEma.DataSeries[0])[bar];
            var slowM = ((ValueDataSeries)slowEma.DataSeries[0])[bar - 1];
            var slowN = ((ValueDataSeries)slowEma.DataSeries[0])[bar - 2];

            var t1 = ((fast - slow) - (fastM - slowM)) * Sensitivity;
            var t1Prev = ((fastM - slowM) - (fastN - slowN)) * Sensitivity;

            _s1[bar] = e1;

            if (t1 > 0)
            {
                if (t1 < t1Prev)
                    _posSeries[bar] = t1;
                else
                    _posLess[bar] = t1;
            }
            else
            {
                if (t1 < t1Prev)
                    _negSeries[bar] = t1 * -1;
                else
                    _negLess[bar] = t1 * -1;
            }
        }

    }
}