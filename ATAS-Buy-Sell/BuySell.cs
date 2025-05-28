
namespace ATAS.Indicators.Technical
{
    #region INCLUDES

    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using System.Net; 
    using ATAS.Indicators;
    using ATAS.Indicators.Drawing;
    using OFT.Attributes.Editors;
    using Newtonsoft.Json.Linq;
    using OFT.Rendering.Context;
    using OFT.Rendering.Tools;
    using static ATAS.Indicators.Technical.SampleProperties;

    using Color = System.Drawing.Color; 
    using MColor = System.Windows.Media.Color;
    using MColors = System.Windows.Media.Colors; 
    using Pen = System.Drawing.Pen;
    using String = System.String;
    using System.Globalization;
    using OFT.Rendering.Settings;
    using System.Text;
    using System.Linq;
    using System.Windows.Media;

    #endregion

    [DisplayName("TraderOracle Buy/Sell")]
    public class BuySell : Indicator
    {
        private const String sVersion = "5.5";

        #region PRIVATE FIELDS

        private PenSettings defibPen = new PenSettings
        {
            Color = DefaultColors.Red.Convert(),
            Width = 1,
            LineDashStyle = LineDashStyle.Dot
        };

        private RenderStringFormat _format = new()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        private List<string> lsH = new List<string>();
        private List<string> lsM = new List<string>();
        private bool bBigArrowUp = false;
        private readonly PaintbarsDataSeries _paintBars = new("Paint bars");

        private int iFutureSound = 0;
        private int iTouched = 0;
        private int _highBar;
        private int _lowBar;
        private decimal _highest = 0;
        private decimal _lowest = 0;
        private int _highBarL;
        private int _lowBarL;
        private decimal _highestL = 0;
        private decimal _lowestL = 0;
        private int _lastBar = -1;
        private bool _lastBarCounted;
        private Color lastColor = Color.White;
        private bool bShowUp = true;
        private bool bShowDown = true;

        // Default TRUE
        private bool bShowTramp = true;          
        private bool bUseFisher = true;          
        private bool bUseWaddah = true;
        private bool bUsePSAR = true;
        private bool bShowRegularBuySell = true;

        private bool bKAMAWick = true;
        private bool bUseSqueeze = false;
        private bool bUseMACD = true;
        private bool bUseHMA = true;

        private bool bShowSqueeze = false;
        private bool bShowRevPattern = false;
        private bool bAdvanced = false;
        private bool bShowLines = false;

        private int iOffset = 9;
        private int iFontSize = 10;
        private int iNewsFont = 10;
        private int iWaddaSensitivity = 150;
        private int iMACDSensitivity = 70;
        private int CandleColoring = 0;

        #endregion

        #region SETTINGS

        [Display(GroupName = "Main", Name = "Show buy/sell dots")]
        public bool ShowRegularBuySell { get => bShowRegularBuySell; set { bShowRegularBuySell = value; RecalculateValues(); } }
        
        // =============================================================
        // =======================    ALERTS    ========================
        // =============================================================

        [Display(GroupName = "Alerts", Name = "Use Alert Sounds")]
        public bool UseAlerts { get; set; }

        // ======================================================================
        // =======================    COLORED CANDLES    ========================
        // ======================================================================

        [Display(GroupName = "Colored Candles", Name = "Show Reversal Patterns")]
        public bool ShowRevPattern { get => bShowRevPattern; set { bShowRevPattern = value; RecalculateValues(); } }
        [Display(GroupName = "Colored Candles", Name = "Show Advanced Ideas")]
        public bool ShowBrooks { get => bAdvanced; set { bAdvanced = value; RecalculateValues(); } }
        [Display(GroupName = "Colored Candles", Name = "Waddah Sensitivity")]
        [Range(0, 9000)]
        public int WaddaSensitivity { get => iWaddaSensitivity; set { if (value < 0) return; iWaddaSensitivity = value; RecalculateValues(); } }
        [Display(GroupName = "Colored Candles", Name = "MACD Sensitivity")]
        [Range(0, 9000)]
        public int MACDSensitivity { get => iMACDSensitivity; set { if (value < 0) return; iMACDSensitivity = value; RecalculateValues(); } }

        // =============================================================
        // =======================    EXTRAS    ========================
        // =============================================================

        [Display(GroupName = "Extras", Name = "Show kama wicks")]
        public bool KAMAWick { get => bKAMAWick; set { bKAMAWick = value; RecalculateValues(); } }

        [Display(GroupName = "Extras", Name = "Show Kama/EMA 200/VWAP lines")]
        public bool ShowLines { get => bShowLines; set { bShowLines = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "Show Squeeze Relaxer")]
        public bool Show_Squeeze_Relax { get => bShowSqueeze; set { bShowSqueeze = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "Show Trampoline", Description = "Trampoline is the ultimate reversal indicator")]
        public bool Use_Tramp { get => bShowTramp; set { bShowTramp = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "News font")]
        [Range(1, 900)]
        public int NewsFont
        { get => iNewsFont; set { iNewsFont = value; RecalculateValues(); } }

        private decimal VolSec(IndicatorCandle c) { return c.Volume / Convert.ToDecimal((c.LastTime - c.Time).TotalSeconds); }

        #endregion

        #region CONSTRUCTOR

        public BuySell() :
            base(true)
        {
            DenyToChangePanel = true;

            DataSeries[0] = _posSeries;
            DataSeries.Add(_negSeries);
            DataSeries.Add(_negWhite);
            DataSeries.Add(_posWhite);
            DataSeries.Add(_squeezie);
            DataSeries.Add(_paintBars);

            Add(_ft);
            Add(_sq);
            Add(_psar);
            Add(_atr);
            Add(_hma);
        }

        #endregion

        #region INDICATORS

        private readonly SMA _Sshort = new SMA() { Period = 3 };
        private readonly SMA _Slong = new SMA() { Period = 10 };
        private readonly SMA _Ssignal = new SMA() { Period = 16 };
        private readonly RSI _rsi = new() { Period = 14 };
        private readonly ATR _atr = new() { Period = 14 };
        private readonly ParabolicSAR _psar = new ParabolicSAR();
        private readonly EMA _myEMA = new EMA() { Period = 21 };
        private readonly EMA _9 = new EMA() { Period = 9 };
        private readonly EMA _21 = new EMA() { Period = 21 };
        private readonly HMA _hma = new HMA() { };
        private readonly EMA fastEma = new EMA() { Period = 20 };
        private readonly EMA slowEma = new EMA() { Period = 40 };
        private readonly FisherTransform _ft = new FisherTransform() { Period = 10 };
        private readonly BollingerBands _bb = new BollingerBands() { Period = 20, Shift = 0, Width = 2 };
        private readonly SqueezeMomentum _sq = new SqueezeMomentum() { BBPeriod = 20, BBMultFactor = 2, KCPeriod = 20, KCMultFactor = 1.5m, UseTrueRange = false };

        #endregion

        #region RENDER CONTEXT

        private void DrawString(RenderContext context, string renderText, int yPrice, Color color)
        {
            var textSize = context.MeasureString(renderText, new RenderFont("Arial", 9));
            context.DrawString(renderText, new RenderFont("Arial", 9), color, 
                Container.Region.Right - textSize.Width - 5, yPrice - textSize.Height);
        }

        protected override void OnRender(RenderContext context, DrawingLayouts layout)
        {
        }

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

        #region DATA SERIES

        [Display(Name = "Font Size", GroupName = "Drawing", Order = int.MaxValue)]
        [Range(1, 90)]
        public int TextFont { get => iFontSize; set { iFontSize = value; RecalculateValues(); } }

        [Display(Name = "Text Offset", GroupName = "Drawing", Order = int.MaxValue)]
        [Range(0, 900)]
        public int Offset { get => iOffset; set { iOffset = value; RecalculateValues(); } }

        private readonly ValueDataSeries _squeezie = new("Squeeze Relaxer") { Color = MColors.Yellow, VisualType = VisualMode.Dots, Width = 3 };
        private readonly ValueDataSeries _posWhite = new("Vol Imbalance Sell") { Color = MColors.White, VisualType = VisualMode.DownArrow, Width = 1 };
        private readonly ValueDataSeries _negWhite = new("Vol Imbalance Buy") { Color = MColors.White, VisualType = VisualMode.UpArrow, Width = 1 };
        private readonly ValueDataSeries _posSeries = new("Regular Buy Signal") { Color = MColor.FromArgb(255, 0, 255, 0), VisualType = VisualMode.Dots, Width = 2 };
        private readonly ValueDataSeries _negSeries = new("Regular Sell Signal") { Color = MColor.FromArgb(255, 255, 104, 48), VisualType = VisualMode.Dots, Width = 2 };

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
            if (bar < 6)
                return;

            #region CANDLE CALCULATIONS

            var pcandle = GetCandle(bar);
            var candle = GetCandle(bar - 1);
            var pbar = bar - 1;
            var ppbar = bar - 2;
            value = candle.Close;
            var chT = ChartInfo.ChartType;

            if (IsNewSession(bar))
            {
                _highest = candle.High;
                _lowest = candle.Low;
                _highestL = candle.High;
                _lowestL = candle.Low;
            }

            bShowDown = true;
            bShowUp = true;

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
            var c3G = p3C.Open < p3C.Close;
            var c3R = p3C.Open > p3C.Close;
            var c4G = p4C.Open < p4C.Close;
            var c4R = p4C.Open > p4C.Close;

            var c00Body = Math.Abs(pcandle.Close - pcandle.Open);
            var c0Body = Math.Abs(candle.Close - candle.Open);
            var c1Body = Math.Abs(p1C.Close - p1C.Open);
            var c2Body = Math.Abs(p2C.Close - p2C.Open);
            var c3Body = Math.Abs(p3C.Close - p3C.Open);
            var c4Body = Math.Abs(p4C.Close - p4C.Open);

            var upWickLarger = c0R && Math.Abs(candle.High - candle.Open) > Math.Abs(candle.Low - candle.Close);
            var downWickLarger = c0G && Math.Abs(candle.Low - candle.Open) > Math.Abs(candle.Close - candle.High);

            var ThreeOutUp = c2R && c1G && c0G && p1C.Open < p2C.Close && p2C.Open < p1C.Close && Math.Abs(p1C.Open - p1C.Close) > Math.Abs(p2C.Open - p2C.Close) && candle.Close > p1C.Low;

            var ThreeOutDown = c2G && c1R && c0R && p1C.Open > p2C.Close && p2C.Open > p1C.Close && Math.Abs(p1C.Open - p1C.Close) > Math.Abs(p2C.Open - p2C.Close) && candle.Close < p1C.Low;

            #endregion

            #region INDICATORS CALCULATE

            _myEMA.Calculate(pbar, value);
            fastEma.Calculate(pbar, value);
            slowEma.Calculate(pbar, value);
            _9.Calculate(pbar, value);
            _21.Calculate(pbar, value);

            _bb.Calculate(pbar, value);
            _rsi.Calculate(pbar, value);

            var fast = ((ValueDataSeries)fastEma.DataSeries[0])[pbar];
            var fastM = ((ValueDataSeries)fastEma.DataSeries[0])[pbar - 1];
            var slow = ((ValueDataSeries)slowEma.DataSeries[0])[pbar];
            var slowM = ((ValueDataSeries)slowEma.DataSeries[0])[pbar - 1];
            var sq1 = ((ValueDataSeries)_sq.DataSeries[0])[pbar];
            var psq1 = ((ValueDataSeries)_sq.DataSeries[0])[pbar - 1];
            var ppsq1 = ((ValueDataSeries)_sq.DataSeries[0])[pbar - 2];
            var f1 = ((ValueDataSeries)_ft.DataSeries[0])[pbar];
            var f2 = ((ValueDataSeries)_ft.DataSeries[1])[pbar];
            var psar = ((ValueDataSeries)_psar.DataSeries[0])[pbar];
            var bb_top = ((ValueDataSeries)_bb.DataSeries[1])[pbar]; // top
            var bb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[pbar]; // bottom
            var rsi = ((ValueDataSeries)_rsi.DataSeries[0])[pbar];
            var rsi1 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 1];
            var rsi2 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 2];
            var hma = ((ValueDataSeries)_hma.DataSeries[0])[pbar];
            var phma = ((ValueDataSeries)_hma.DataSeries[0])[pbar - 1];

            // Linda MACD
            var macd = _Sshort.Calculate(pbar, value) - _Slong.Calculate(pbar, value);
            var signal = _Ssignal.Calculate(pbar, macd);

            var hullUp = hma > phma;
            var hullDown = hma < phma;
            var fisherUp = (f1 < f2);
            var fisherDown = (f2 < f1);
            var macdUp = (macd > signal);
            var macdDown = (macd < signal);

            var psarBuy = (psar < candle.Close);
            var psarSell = (psar > candle.Close);

            var t1 = ((fast - slow) - (fastM - slowM)) * iWaddaSensitivity;

            #endregion

            #region BUY / SELL
            
            if (!macdUp || psarSell || !fisherUp || t1 < 0 || sq1 < 0 || hullDown)
                bShowUp = false;

            if (bShowRegularBuySell)
            {
                iFutureSound = 10;
                _posSeries[pbar] = candle.Low - (_tick * iOffset);
            }

            if (psarBuy || !macdDown || !fisherDown || t1 >= 0 || hullUp)
                bShowDown = false;

            if (bShowRegularBuySell)
            {
                _negSeries[pbar] = candle.High + _tick * iOffset;
                iFutureSound = 11;
            }

            #endregion

            #region ADVANCED LOGIC

            // Squeeze momentum relaxer show
            if (sq1 > 0 && sq1 < psq1 && psq1 > ppsq1 && bShowSqueeze)
            {
                DrawText(pbar, "▼", Color.Yellow, Color.Transparent, false, true); // "▲" "▼"
                iFutureSound = 5;
            }
                
            if (sq1 < 0 && sq1 > psq1 && psq1 < ppsq1 && bShowSqueeze)
            {
                DrawText(pbar, "▲", Color.Yellow, Color.Transparent, false, true);
                iFutureSound = 5;
            }

            if (bAdvanced)
            {
                if (c4Body > c3Body && c3Body > c2Body && c2Body > c1Body && c1Body > c0Body)
                    if ((candle.Close > p1C.Close && p1C.Close > p2C.Close && p2C.Close > p3C.Close) ||
                    (candle.Close < p1C.Close && p1C.Close < p2C.Close && p2C.Close < p3C.Close))
                {
                        DrawText(pbar, "Stairs", Color.Yellow, Color.Transparent);
                        iFutureSound = 4; 
                }
            }

            if (bShowRevPattern)
            {
                if (c0G && c1R && c2R && VolSec(p1C) > VolSec(p2C) && VolSec(p2C) > VolSec(p3C) && candle.Delta < 0)
                    DrawText(pbar, "Vol\nRev", Color.Yellow, Color.Transparent, false, true);
                if (c0R && c1G && c2G && VolSec(p1C) > VolSec(p2C) && VolSec(p2C) > VolSec(p3C) && candle.Delta > 0)
                    DrawText(pbar, "Vol\nRev", Color.Lime, Color.Transparent, false, true);
                if (ThreeOutUp && bShowRevPattern)
                    DrawText(pbar, "3oU", Color.Yellow, Color.Transparent);
                if (ThreeOutDown && bShowRevPattern)
                    DrawText(pbar, "3oD", Color.Yellow, Color.Transparent);
            }

            // Trampoline
            if (bShowTramp)
            {
                if (c0R && c1R && candle.Close < p1C.Close && (rsi >= 70 || rsi1 >= 70 || rsi2 >= 70) &&
                    c2G && p2C.High >= (bb_top - (_tick * 30)))
                {
                    DrawText(pbar, "TR", Color.Yellow, Color.BlueViolet, false, true);
                    iFutureSound = 8;
                }
                   
                if (c0G && c1G && candle.Close > p1C.Close && (rsi < 25 || rsi1 < 25 || rsi2 < 25) &&
                    c2R && p2C.Low <= (bb_bottom + (_tick * 30)))
                {
                    DrawText(pbar, "TR", Color.Yellow, Color.BlueViolet, false, true);
                    iFutureSound = 8;
                }
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
                        case 1:
                            play("majorline");
                            break;
                        case 2:
                            play("VolRev");
                            break;
                        case 3:
                            play("intensity");
                            break;
                        case 4:
                            play("stairs");
                            break;
                        case 5:
                            //play("squeezie");
                            break;
                        case 6:
                            play("equal high");
                            break;
                        case 7:
                            play("equal low");
                            break;
                        case 8:
                            play("trampoline");
                            break;
                        case 9:
                            play("kama");
                            break;
                        case 10:
                            play("buy");
                            break;
                        case 11:
                            play("sell");
                            break;
                        case 12:
                            play("volimb");
                            break;
                        case 13:
                            play("dojicity");
                            break;
                        case 17:
                            play("engulf");
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

        #region MISC FUNCTIONS

        private void play(String s)
        {
        }

        #endregion

    }
}