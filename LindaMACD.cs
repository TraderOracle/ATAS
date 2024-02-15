namespace ATAS.Indicators.Technical
{
    using System.ComponentModel;
    using System.Windows.Media;
    using ATAS.Indicators.Drawing;

    [DisplayName("Linda MACD")]
    public class LindaMACD : Indicator
    {
        private readonly EMA _short = new() { Period = 3 };
        private readonly EMA _long = new() { Period = 10 };
        private readonly EMA _signal = new() { Period = 16 };

        private readonly ValueDataSeries _neg = new("Down Bar")
        {
            Color = Colors.Red,
            VisualType = VisualMode.Histogram
        };

        private readonly ValueDataSeries _pos = new("Up Bar")
        {
            Color = Colors.Green,
            VisualType = VisualMode.Histogram
        };

        public LindaMACD()
        {
            Panel = IndicatorDataProvider.NewPanel;

            DataSeries[0] = _pos;
            DataSeries.Add(_neg);
        }
        protected override void OnCalculate(int bar, decimal value)
        {
            var macd = _short.Calculate(bar, value) - _long.Calculate(bar, value);
            var signal = _signal.Calculate(bar, macd);
            var bars = macd - signal;

            if (bars >= 0)
                _pos[bar] = bars;
            else
                _neg[bar] = bars * -1;
        }

    }
}