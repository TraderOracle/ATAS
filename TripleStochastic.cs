namespace ATAS.Indicators.Technical
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using ATAS.Indicators.Drawing;
    using OFT.Rendering.Context;
    using OFT.Rendering.Settings;
    using OFT.Rendering.Tools;
    using Color = System.Drawing.Color;
    using Pen = System.Drawing.Pen;

    [DisplayName("TripleStochastic")]
    public class TripleStochastic : Indicator
    {
        #region Fields

        private struct days
        {
            public int bar;
            public string label;
            public decimal price1;
            public decimal price2;
            public Color c;
        }
        private List<days> lsDays = new List<days>();
        private int iBarHigh = 90;
        private int iBarWidth = 13;

        private readonly Highest _highest9 = new() { Period = 9 };
        private readonly Lowest _lowest9 = new() { Period = 9 };
        private readonly Highest _highest14 = new() { Period = 14 };
        private readonly Lowest _lowest14 = new() { Period = 14 };
        private readonly Highest _highest40 = new() { Period = 30 };
        private readonly Lowest _lowest40 = new() { Period = 30 };
        private readonly Highest _highest60 = new() { Period = 60 };
        private readonly Lowest _lowest60 = new() { Period = 60 };

        private readonly SMA _ksma9 = new() { Period = 1 };
        private readonly SMA _sma9 = new() { Period = 3 };
        private readonly SMA _ksma14 = new() { Period = 1 };
        private readonly SMA _sma14 = new() { Period = 3 };
        private readonly SMA _ksma40 = new() { Period = 1 };
        private readonly SMA _sma40 = new() { Period = 4 };
        private readonly SMA _ksma60 = new() { Period = 1 };
        private readonly SMA _sma60 = new() { Period = 10 };

        private readonly ValueDataSeries _k1 = new("DId1", "%D14");
        private readonly ValueDataSeries _k2 = new("DId2", "%D40");
        private readonly ValueDataSeries _k3 = new("DId3", "%D60");
        private readonly ValueDataSeries _k4 = new("DId4", "%D60");

        #endregion

        #region Properties

        [Display(GroupName = "Settings", Name = "Highlight Height")]
        public int BarHigh { get => iBarHigh; set { iBarHigh = value; RecalculateValues(); } }

        [Display(GroupName = "Settings", Name = "Highlight Width")]
        public int BarWidth { get => iBarWidth; set { iBarWidth = value; RecalculateValues(); } }

        public LineSeries UpLine { get; set; } = new("UpLine", "Up")
        { Color = Color.White.Convert(), LineDashStyle = LineDashStyle.Solid, Value = 89, Width = 1 };

        public LineSeries DownLine { get; set; } = new("DownLine", "Down")
        { Color = Color.White.Convert(), LineDashStyle = LineDashStyle.Solid, Value = 11, Width = 1 };


        #endregion

        #region constructor 

        public TripleStochastic()
            : base(true)
        {
            EnableCustomDrawing = true;
            DenyToChangePanel = true;
            SubscribeToDrawingEvents(DrawingLayouts.Historical);

            Panel = IndicatorDataProvider.NewPanel;

            ((ValueDataSeries)DataSeries[0]).Color = DefaultColors.Lime.Convert();
            ((ValueDataSeries)DataSeries[0]).Width = 3;

            DataSeries.Add(new ValueDataSeries("DId1", "%D14")
            { VisualType = VisualMode.Line, LineDashStyle = LineDashStyle.Dot, Color = DefaultColors.Red.Convert(), Width = 1 });
            DataSeries.Add(new ValueDataSeries("DId2", "%D40")
            { VisualType = VisualMode.Line, LineDashStyle = LineDashStyle.Dot, Color = DefaultColors.Red.Convert(), Width = 1 });
            DataSeries.Add(new ValueDataSeries("DId3", "%D60")
            { VisualType = VisualMode.Line, LineDashStyle = LineDashStyle.Solid, Color = DefaultColors.Orange.Convert(), Width = 2 });

            LineSeries.Add(UpLine);
            LineSeries.Add(DownLine);
        }

        #endregion

        #region Protected methods

        protected override void OnRender(RenderContext context, DrawingLayouts layout)
        {
            if (ChartInfo is null || InstrumentInfo is null)
                return;

            foreach (var l in lsDays)
            {
                RenderPen highPen = new RenderPen(l.c);
                var xH = ChartInfo.PriceChartContainer.GetXByBar(l.bar, false);
                var yWidth = ChartInfo.ChartContainer.Region.Width;
                var xHigh = ChartInfo.ChartContainer.Region.Height;
                context.DrawFillRectangle(highPen, l.c, new Rectangle(xH, xHigh - iBarHigh, iBarWidth, iBarHigh));
            }
        }

        protected override void OnCalculate(int bar, decimal value)
        {
            var candle = GetCandle(bar);

            var highest9 = _highest9.Calculate(bar, candle.High);
            var lowest9 = _lowest9.Calculate(bar, candle.Low);
            var highest14 = _highest14.Calculate(bar, candle.High);
            var lowest14 = _lowest14.Calculate(bar, candle.Low);
            var highest40 = _highest40.Calculate(bar, candle.High);
            var lowest40 = _lowest40.Calculate(bar, candle.Low);
            var highest60 = _highest60.Calculate(bar, candle.High);
            var lowest60 = _lowest60.Calculate(bar, candle.Low);

            decimal k9 = 50;
            decimal k14 = 50;
            decimal k40 = 50;
            decimal k60 = 50;

            // NINE
            if (highest9 - lowest9 == 0 && bar > 0)
                k9 = _k1[bar - 1];
            else
                k9 = (candle.Close - lowest9) / (highest9 - lowest9) * 100;
            var ksma9 = _ksma9.Calculate(bar, k9);
            var d9 = _sma9.Calculate(bar, ksma9);
            this[bar] = d9;

            // FOURTEEN
            if (highest14 - lowest14 == 0 && bar > 0)
                k14 = _k2[bar - 1];
            else
                k14 = (candle.Close - lowest14) / (highest14 - lowest14) * 100;
            var ksma14 = _ksma14.Calculate(bar, k14);
            var d14 = _sma14.Calculate(bar, ksma14);
            DataSeries[1][bar] = d14;

            // FORTY
            if (highest40 - lowest40 == 0 && bar > 0)
                k40 = _k3[bar - 1];
            else
                k40 = (candle.Close - lowest40) / (highest40 - lowest40) * 100;
            var ksma40 = _ksma40.Calculate(bar, k40);
            var d40 = _sma40.Calculate(bar, ksma40);
            DataSeries[2][bar] = d40;

            // SIXTY
            if (highest60 - lowest60 == 0 && bar > 0)
                k60 = _k4[bar - 1];
            else
                k60 = (candle.Close - lowest60) / (highest60 - lowest60) * 100;
            var ksma60 = _ksma60.Calculate(bar, k60);
            var d60 = _sma60.Calculate(bar, ksma60);
            DataSeries[3][bar] = d60;

            var gPen = new Pen(new SolidBrush(Color.Transparent)) { Width = 3 };
            var rPen = new Pen(new SolidBrush(Color.Transparent)) { Width = 3 };

            if (d9 >= UpLine.Value && d14 >= UpLine.Value && d40 >= UpLine.Value && d60 >= UpLine.Value)
            {
                days a = new days();
                a.bar = bar;
                a.c = Color.Lime;
                lsDays.Add(a);
            }
            else if (d9 <= DownLine.Value && d14 <= DownLine.Value && d40 <= DownLine.Value && d60 <= DownLine.Value)
            {
                days a = new days();
                a.bar = bar;
                a.c = Color.Red;
                lsDays.Add(a);
            }
            else if (d9 >= UpLine.Value)
            {
                days a = new days();
                a.bar = bar;
                a.c = Color.DarkGreen;
                lsDays.Add(a);
            }
            else if (d9 <= DownLine.Value)
            {
                days a = new days();
                a.bar = bar;
                a.c = Color.DarkRed;
                lsDays.Add(a);
            }
        }

        #endregion
    }
}