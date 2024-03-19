namespace ATAS.Indicators.Technical
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using System.Resources;
    using System.Runtime.Intrinsics.X86;
    using System.Windows.Media;
    using ATAS.Indicators;
    using ATAS.Indicators.Drawing;
    using ATAS.Indicators.Technical.Properties;
    using OFT.Attributes;
    using OFT.Rendering.Context;
    using OFT.Rendering.Settings;
    using OFT.Rendering.Tools;

    [DisplayName("DTC Scalper")]
    public class DTC_Scalper : Indicator
    {
        decimal upTrades;
        decimal dnTrades;
        decimal daTrades;

        private readonly ValueDataSeries _Vol = new("Basic Volume")
        { Color = System.Windows.Media.Color.FromArgb(120,200,0,50), VisualType = VisualMode.Histogram };

        private readonly ValueDataSeries _negSeries = new("Negative")
        { Color = Colors.Red, VisualType = VisualMode.Dots, Width = 4 };

        private readonly ValueDataSeries _posSeries = new("Positive")
        { Color = Colors.Lime, VisualType = VisualMode.Dots, Width = 4 };

        protected override void OnRender(RenderContext context, DrawingLayouts layout)
        {
            if (ChartInfo is null || InstrumentInfo is null)
                return;

            int bar = CurrentBar-1;
            var x1 = ChartInfo.GetXByBar(bar, false);
            var x2 = ChartInfo.GetXByBar(bar-1, false);
            var y1 = ChartInfo.GetYByPrice(_posSeries[bar], false);
            var y2 = ChartInfo.GetYByPrice(_posSeries[bar-1], false);

            context.DrawLine(new RenderPen(System.Drawing.Color.White), x1, y1, x2, y2);
        }

        protected override void OnNewTrade(MarketDataArg arg)
        {
            int bar = CurrentBar-1;
            var cnd = GetCandle(bar);

            upTrades = cnd.Volume * (cnd.Close - cnd.Low) / (cnd.High - cnd.Low);
            dnTrades = cnd.Volume * (cnd.High - cnd.Close) / (cnd.High - cnd.Low);

            _posSeries[bar] = upTrades;
            _negSeries[bar] = dnTrades;
            //_Vol[bar] = upTrades + dnTrades;

            var greenPen = new System.Drawing.Pen(new SolidBrush(System.Drawing.Color.Lime)) { Width = 1 };
            var redPen = new System.Drawing.Pen(new SolidBrush(System.Drawing.Color.Red)) { Width = 1 };
            //TrendLines.Add(new TrendLine(bar - 1, _posSeries[bar - 1], bar, _posSeries[bar], greenPen));
            //TrendLines.Add(new TrendLine(bar - 1, _negSeries[bar - 1], bar, _negSeries[bar], redPen));
        }

        public DTC_Scalper() :
            base(true)
        {
            Panel = IndicatorDataProvider.NewPanel;
            DataSeries[0] = _posSeries;
            DataSeries.Add(_negSeries);
            DataSeries.Add(_Vol);
        }

        protected override void OnCalculate(int bar, decimal value)
        {

        }

    }
}