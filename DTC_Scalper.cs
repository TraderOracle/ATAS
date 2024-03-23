namespace ATAS.Indicators.Technical
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Media;
    using ATAS.Indicators;

    [DisplayName("DTC Scalper")]
    public class DTC_Scalper : Indicator
    {
        decimal upTrades;
        decimal dnTrades;

        private readonly ValueDataSeries _Vol = new("Basic Volume")
        { Color = System.Windows.Media.Color.FromArgb(120,200,0,50), VisualType = VisualMode.Histogram };

        private readonly ValueDataSeries _negSeries = new("Red Dot")
        { Color = Colors.Red, VisualType = VisualMode.Dots, Width = 4 };
        private readonly ValueDataSeries _posSeries = new("Green Dot")
        { Color = Colors.Lime, VisualType = VisualMode.Dots, Width = 4 };

        private readonly ValueDataSeries _negLine = new("Red Line")
        { Color = Colors.Red, VisualType = VisualMode.Line, Width = 1 };
        private readonly ValueDataSeries _posLine = new("Green Line")
        { Color = Colors.Lime, VisualType = VisualMode.Line, Width = 1 };

        protected override void OnNewTrade(MarketDataArg arg)
        {
            int bar = CurrentBar-1;
            var cnd = GetCandle(bar);

            upTrades = cnd.Volume * (cnd.Close - cnd.Low) / (cnd.High - cnd.Low);
            dnTrades = cnd.Volume * (cnd.High - cnd.Close) / (cnd.High - cnd.Low);

            _posSeries[bar] = upTrades;
            _negSeries[bar] = dnTrades;
            _posLine[bar] = upTrades;
            _negLine[bar] = dnTrades;
            //_Vol[bar] = upTrades + dnTrades;

            var greenPen = new System.Drawing.Pen(new SolidBrush(System.Drawing.Color.Lime)) { Width = 1 };
            var redPen = new System.Drawing.Pen(new SolidBrush(System.Drawing.Color.Red)) { Width = 1 };
            //TrendLines.Add(new TrendLine(bar - 1, _posSeries[bar - 1], bar, _posSeries[bar], greenPen));
            //TrendLines.Add(new TrendLine(bar - 1, _negSeries[bar - 1], bar, _negSeries[bar], redPen));
        }

        public DTC_Scalper() :
            base(true)
        {
            EnableCustomDrawing = true;
            DenyToChangePanel = true;
            SubscribeToDrawingEvents(DrawingLayouts.Historical);

            Panel = IndicatorDataProvider.NewPanel;
            DataSeries[0] = _posSeries;
            DataSeries.Add(_negSeries);
            DataSeries.Add(_negLine);
            DataSeries.Add(_posLine);
            //DataSeries.Add(_Vol);
        }

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar > CurrentBar - 1)
                return;

            var cnd = GetCandle(bar);

            upTrades = cnd.Volume * (cnd.Close - cnd.Low) / (cnd.High - cnd.Low);
            dnTrades = cnd.Volume * (cnd.High - cnd.Close) / (cnd.High - cnd.Low);

            _posSeries[bar] = upTrades;
            _negSeries[bar] = dnTrades;
            _posLine[bar] = upTrades;
            _negLine[bar] = dnTrades;
        }

    }
}