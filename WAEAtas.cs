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

        private readonly ValueDataSeries _negSeries = new(Resources.Down)
        {
            Color = Colors.Red,
            VisualType = VisualMode.Histogram
        };

        private readonly ValueDataSeries _posSeries = new(Resources.Up)
        {
            Color = Colors.Green,
            VisualType = VisualMode.Histogram
        };

        public WaddahExplosion() : 
            base(true)
        {
            Panel = IndicatorDataProvider.NewPanel;
            DataSeries[0] = _posSeries;
            DataSeries.Add(_negSeries);
        }

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar < 5)
                return;

            var candle = GetCandle(bar);

            value = candle.Close;

            fastEma.Calculate(bar, value);
            var fast = ((ValueDataSeries)fastEma.DataSeries[0])[bar];
            var fastM = ((ValueDataSeries)fastEma.DataSeries[0])[bar - 1];

            slowEma.Calculate(bar, value);
            var slow = ((ValueDataSeries)slowEma.DataSeries[0])[bar];
            var slowM = ((ValueDataSeries)slowEma.DataSeries[0])[bar - 1];
            var slowN = ((ValueDataSeries)slowEma.DataSeries[0])[bar - 2];

            var t1 = ((fast - slow) - (fastM - slowM)) * Sensitivity;

            if (t1 > 0)
                _posSeries[bar] = t1;
            else
                _negSeries[bar] = t1 * -1;
        }

    }
}