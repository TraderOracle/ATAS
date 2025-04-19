
namespace ATAS.Indicators.Technical
{
    #region INCLUDES

    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using ATAS.Indicators;
    using ATAS.Indicators.Drawing;
    using OFT.Rendering.Context;
    using OFT.Rendering.Tools;
    using static ATAS.Indicators.Technical.SampleProperties;
    using Color = System.Drawing.Color;
    using MColors = System.Windows.Media.Colors;
    using MColor = System.Windows.Media.Color;
    using Pen = System.Drawing.Pen;

    #endregion

    [DisplayName("TO VolImb")]
    public class TOVolImb : Indicator
    {
        #region VARIABLES
        private const String sVersion = "1.0";

        private readonly PaintbarsDataSeries _paintBars = new("Paint bars");
        List<int> LineTouches = new List<int>();
        private int iFutureSound = 0;
        private String sWavDir = @"C:\Program Files (x86)\ATAS Platform\Sounds";
        private int _lastBar = -1;
        private bool _lastBarCounted;
        private int iOffset = 9;
        private int iFontSize = 10;
        private int iMinBars = 3;

        [Display(Name = "Font Size", GroupName = "General", Order = int.MaxValue)]
        [Range(1, 90)]
        public int TextFont { get => iFontSize; set { iFontSize = value; RecalculateValues(); } }

        [Display(Name = "Text Offset", GroupName = "General", Order = int.MaxValue)]
        [Range(0, 900)]
        public int Offset { get => iOffset; set { iOffset = value; RecalculateValues(); } }

        [Display(GroupName = "General", Name = "Minimum candle gap")]
        public int MinBars { get => iMinBars; set { iMinBars = value; RecalculateValues(); } }

        [Display(GroupName = "Alerts", Name = "Use Alert Sounds")]
        public bool UseAlerts { get; set; }
        [Display(GroupName = "Alerts", Name = "WAV Sound Directory")]
        public String WavDir { get => sWavDir; set { sWavDir = value; RecalculateValues(); } }

        private readonly ValueDataSeries _posSeries = new("Vol Imbalance Sell") { Color = MColors.White, VisualType = VisualMode.DownArrow, Width = 1 };
        private readonly ValueDataSeries _negSeries = new("Vol Imbalance Buy") { Color = MColors.White, VisualType = VisualMode.UpArrow, Width = 1 };

        public TOVolImb() :
            base(true)
        {
            DenyToChangePanel = true;
            SubscribeToDrawingEvents(DrawingLayouts.Historical);
            EnableCustomDrawing = true;

            DataSeries[0] = _posSeries;
            DataSeries.Add(_negSeries);
            DataSeries.Add(_paintBars);
        }

        private readonly RSI _rsi = new() { Period = 14 };
        private readonly BollingerBands _bb = new BollingerBands() { Period = 20, Shift = 0, Width = 2 };

        protected void DrawText(int bBar, String strX, Color cI, Color cB, bool bOverride = false, bool bSwap = false)
        {
            var candle = GetCandle(bBar);

            decimal _tick = ChartInfo.PriceChartContainer.Step;
            decimal loc = 0;

            if (candle.Close > candle.Open || bOverride)
                loc = candle.High + (_tick * iOffset);
            else
                loc = candle.Low - (_tick * iOffset);

            if (candle.Close > candle.Open && bSwap)
                loc = candle.Low - (_tick * (iOffset * 2));
            else if (candle.Close < candle.Open && bSwap)
                loc = candle.High + (_tick * iOffset);

            if (strX == "▼")
                loc = candle.High + (_tick * iOffset);
            if (strX == "▲")
                loc = candle.Low - (_tick * (iOffset * 2));

            AddText("Aver" + bBar, strX, true, bBar, loc, cI, cB, iFontSize, DrawingText.TextAlign.Center);
        }

        #endregion

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar == 0)
            {
                DataSeries.ForEach(x => x.Clear());
                HorizontalLinesTillTouch.Clear();
                _lastBarCounted = false;
                return;
            }
            if (bar < 6) return;

            #region CANDLE CALCULATIONS

            var pcandle = GetCandle(bar);
            var candle = GetCandle(bar - 1);
            var pbar = bar - 1;
            var ppbar = bar - 2;
            value = candle.Close;
            var chT = ChartInfo.ChartType;

            decimal _tick = ChartInfo.PriceChartContainer.Step;
            var p1C = GetCandle(pbar - 1);
            var p2C = GetCandle(pbar - 2);
            var p3C = GetCandle(pbar - 3);
            var p4C = GetCandle(pbar - 4);

            var red = candle.Close < candle.Open;
            var green = candle.Close > candle.Open;
            var c00G = pcandle.Open < pcandle.Close;
            var c00R = pcandle.Open > pcandle.Close;
            var c0G = candle.Open < candle.Close;
            var c0R = candle.Open > candle.Close;
            var c1G = p1C.Open < p1C.Close;
            var c1R = p1C.Open > p1C.Close;
            var c2G = p2C.Open < p2C.Close;
            var c2R = p2C.Open > p2C.Close;

            _bb.Calculate(pbar, value);
            _rsi.Calculate(pbar, value);

            var bb_mid = ((ValueDataSeries)_bb.DataSeries[0])[pbar]; // mid
            var bb_top = ((ValueDataSeries)_bb.DataSeries[1])[pbar]; // top
            var bb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[pbar]; // bottom
            var rsi = ((ValueDataSeries)_rsi.DataSeries[0])[pbar];
            var rsi1 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 1];
            var rsi2 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 2];

            #endregion

            #region VOLUME IMBALANCES

            var highPen = new Pen(new SolidBrush(Color.FromArgb(255, 235, 243, 255)))
            { Width = 3, DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };
            var invisPen = new Pen(new SolidBrush(Color.FromArgb(0, 0, 0, 0)))
            { Width = 0, DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };

            if (green && c1G && candle.Open > p1C.Close)
            {
                if (LineTouches.IndexOf(bar) == -1)
                {
                    LineTouches.Add(bar);
                    HorizontalLinesTillTouch.Add(new LineTillTouch(pbar, candle.Open, highPen));
                    iFutureSound = 12;
                }
            }

            if (red && c1R && candle.Open < p1C.Close)
            {
                if (LineTouches.IndexOf(bar) == -1)
                {
                    LineTouches.Add(bar);
                    HorizontalLinesTillTouch.Add(new LineTillTouch(pbar, candle.Open, highPen));
                    iFutureSound = 12;
                }
            }

            foreach (LineTillTouch ltt in HorizontalLinesTillTouch)
            {
                int iDistance = ltt.SecondBar - ltt.FirstBar;
                if (ltt.Finished && ltt.SecondBar == bar && iDistance > iMinBars)
                {
                    _paintBars[bar] = MColor.FromRgb(255, 255, 255);
                    break;
                }
                else if (ltt.Finished && iDistance < iMinBars)
                {
                    HorizontalLinesTillTouch.Remove(ltt);
                    ltt.Pen = invisPen;
                    break;
                }
            }

            #endregion

            #region TRAMPOLINE

            if (c0R && c1R && candle.Close < p1C.Close && (rsi >= 70 || rsi1 >= 70 || rsi2 >= 70) &&
                c2G && p2C.High >= (bb_top - (_tick * 30)))
            {
                DrawText(pbar, "TR", Color.Black, Color.Lime, false, true);
                iFutureSound = 8;
            }

            if (c0G && c1G && candle.Close > p1C.Close && (rsi < 25 || rsi1 < 25 || rsi2 < 25) &&
                c2R && p2C.Low <= (bb_bottom + (_tick * 30)))
            {
                DrawText(pbar, "TR", Color.Black, Color.Lime, false, true);
                iFutureSound = 8;
            }

            #endregion

            #region ALERTS LOGIC

            if (_lastBar != bar)
            {
                if (_lastBarCounted && UseAlerts)
                {
                    var priceString = candle.Close.ToString();

                    switch (iFutureSound)
                    {
                        case 8:
                            play("trampoline");
                            break;
                        case 12:
                            play("volimb");
                            break;
                        default: break;
                    }
                    iFutureSound = 0;
                }
                _lastBar = bar;
            }
            else
            {
                if (!_lastBarCounted)
                    _lastBarCounted = true;
            }

            #endregion

        }

        private void play(String s)
        {
            try
            {
                System.Diagnostics.Process.Start("cmd.exe", "/c " + sWavDir + "\\" + s + ".wav");
            }
            catch { }
        }

    }
}