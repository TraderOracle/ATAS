
namespace ATAS.Indicators.Technical
{
    #region INCLUDES

    using System;
    using System.Media;
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
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Drawing.Drawing2D;

    #endregion

    [DisplayName("TraderOracle Buy/Sell")]
    public class BuySell : Indicator
    {
        private const String sVersion = "4.1";
        private int iTouched = 0;
        private bool bVolImbFinished = false;
        private bool bRefreshLines = false;

        #region PRIVATE FIELDS

        private struct bars
        {
            public String s;
            public int bar;
            public bool top;
        }
        private struct days
        {
            public int idx;
            public string label;
            public decimal price1;
            public decimal price2;
            public Color c;
        }

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

        private List<days> lsDays = new List<days>();
        private List<bars> lsBar = new List<bars>();
        private List<string> lsH = new List<string>();
        private List<string> lsM = new List<string>();
        private bool bBigArrowUp = false;
        private static readonly HttpClient client = new HttpClient();
        private readonly PaintbarsDataSeries _paintBars = new("Paint bars");

        private String _highS = "1st Hour High";
        private String _lowS = "1st Hour Low";
        private String _highL = "London High";
        private String _lowL = "London Low";
        private String sWavDir = @"C:\Program Files (x86)\ATAS Platform\Sounds";
        private String tsFile = @"c:\temp\TraderSmarts.txt";
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
        private Color colorEngulfg = Color.FromArgb(255, 0, 61, 29);
        private Color colorEngulfr = Color.FromArgb(255, 87, 3, 3);
        private bool bShowUp = true;
        private bool bShowDown = true;
        private bool bShowLondon = false;

        // Default TRUE
        private bool bShowEngBB = true;
        private bool bShowTramp = true;          // SHOW
        private bool bShowNews = true;
        private bool bUseFisher = true;          // USE
        private bool bUseWaddah = true;
        private bool bUseT3 = false;
        private bool bUsePSAR = true;
        private bool bShowRegularBuySell = true;
        private bool bVolumeImbalances = true;

        private bool bKAMAWick = true;
        private bool bNewsProcessed = false;     // USE
        private bool bUseSuperTrend = false;
        private bool bUseSqueeze = false;
        private bool bUseMACD = true;
        private bool bUseKAMA = false;
        private bool bUseAO = false;
        private bool bUseHMA = true;

        private bool bShowSqueeze = false;
        private bool bShowRevPattern = false;
        private bool bAdvanced = false;
        private bool bShowLines = false;

        private int iKAMAPeriod = 9;
        private int iOffset = 9;
        private int iFontSize = 10;
        private int iNewsFont = 10;
        private int iWaddaSensitivity = 150;
        private int iMACDSensitivity = 70;
        private int CandleColoring = 0;

        #endregion

        #region SETTINGS

        [Display(GroupName = "Buy/Sell Indicators", Name = "Show buy/sell dots")]
        public bool ShowRegularBuySell { get => bShowRegularBuySell; set { bShowRegularBuySell = value; RecalculateValues(); } }
        [Display(GroupName = "Buy/Sell Indicators", Name = "Use Alert Sounds")]
        public bool UseAlerts { get; set; }

        // ========================================================================
        // =======================    FILTER INDICATORS    ========================
        // ========================================================================

        [Display(GroupName = "Buy/Sell Filters", Name = "Waddah Explosion", Description = "The Waddah Explosion must be the correct color, and have a value")]
        public bool Use_Waddah_Explosion { get => bUseWaddah; set { bUseWaddah = value; RecalculateValues(); } }
        [Display(GroupName = "Buy/Sell Filters", Name = "Awesome Oscillator", Description = "AO is positive or negative")]
        public bool Use_Awesome { get => bUseAO; set { bUseAO = value; RecalculateValues(); } }
        [Display(GroupName = "Buy/Sell Filters", Name = "Parabolic SAR", Description = "The PSAR must be signaling a buy/sell signal same as the arrow")]
        public bool Use_PSAR { get => bUsePSAR; set { bUsePSAR = value; RecalculateValues(); } }
        [Display(GroupName = "Buy/Sell Filters", Name = "Squeeze Momentum", Description = "The squeeze must be the correct color")]
        public bool Use_Squeeze_Momentum { get => bUseSqueeze; set { bUseSqueeze = value; RecalculateValues(); } }
        [Display(GroupName = "Buy/Sell Filters", Name = "MACD", Description = "Standard 12/26/9 MACD crossing in the correct direction")]
        public bool Use_MACD { get => bUseMACD; set { bUseMACD = value; RecalculateValues(); } }
        [Display(GroupName = "Buy/Sell Filters", Name = "Hull Moving Avg", Description = "Price must align to the HMA trend")]
        public bool Use_HMA { get => bUseHMA; set { bUseHMA = value; RecalculateValues(); } }
        [Display(GroupName = "Buy/Sell Filters", Name = "SuperTrend", Description = "Price must align to the current SuperTrend trend")]
        public bool Use_SuperTrend { get => bUseSuperTrend; set { bUseSuperTrend = value; RecalculateValues(); } }
        [Display(GroupName = "Buy/Sell Filters", Name = "T3", Description = "Price must cross the T3")]
        public bool Use_T3 { get => bUseT3; set { bUseT3 = value; RecalculateValues(); } }
        [Display(GroupName = "Buy/Sell Filters", Name = "Fisher Transform", Description = "Fisher Transform must cross to the correct direction")]
        public bool Use_Fisher_Transform { get => bUseFisher; set { bUseFisher = value; RecalculateValues(); } }

        [Display(GroupName = "Custom MA Filter", Name = "Use KAMA", Description = "Price crosses KAMA")]
        public bool Use_KAMA { get => bUseKAMA; set { bUseKAMA = value; RecalculateValues(); } }

        private class candleColor : Collection<Entity>
        {
            public candleColor()
                : base(new[]
                {
                    new Entity { Value = 1, Name = "None" },
                    new Entity { Value = 2, Name = "Waddah Explosion" },
                    new Entity { Value = 3, Name = "Squeeze" },
                    new Entity { Value = 4, Name = "Delta" },
                    new Entity { Value = 5, Name = "MACD" }
                })
            { }
        }
        [Display(Name = "Candle Color", GroupName = "Colored Candles")]
        [ComboBoxEditor(typeof(candleColor), DisplayMember = nameof(Entity.Name), ValueMember = nameof(Entity.Value))]
        public int canColor { get => CandleColoring; set { if (value < 0) return; CandleColoring = value; RecalculateValues(); } }
        [Display(GroupName = "Colored Candles", Name = "Color BB engulfing candles")]
        public bool ShowEngBB { get => bShowEngBB; set { bShowEngBB = value; RecalculateValues(); } }
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
        [Display(GroupName = "Colored Candles", Name = "Engulfing GREEN Candle Color")]
        public Color colEngulf { get => colorEngulfg; set { colorEngulfg = value; RecalculateValues(); } }
        [Display(GroupName = "Colored Candles", Name = "Engulfing RED Candle Color")]
        public Color colEngulfr { get => colorEngulfr; set { colorEngulfr = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "Show kama wicks")]
        public bool KAMAWick { get => bKAMAWick; set { bKAMAWick = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "WAV Sound Directory")]
        public String WavDir { get => sWavDir; set { sWavDir = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "TraderSmarts File")]
        public String stsFile { get => tsFile; set { tsFile = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "Show Kama/EMA 200/VWAP lines")]
        public bool ShowLines { get => bShowLines; set { bShowLines = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "Show Squeeze Relaxer")]
        public bool Show_Squeeze_Relax { get => bShowSqueeze; set { bShowSqueeze = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "Show Volume Imbalances", Description = "Show gaps between two candles, indicating market strength")]
        public bool Use_VolumeImbalances { get => bVolumeImbalances; set { bVolumeImbalances = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "Show Trampoline", Description = "Trampoline is the ultimate reversal indicator")]
        public bool Use_Tramp { get => bShowTramp; set { bShowTramp = value; RecalculateValues(); } }
        [Display(GroupName = "Extras", Name = "Show London Session Lines", Description = "Show lines from London session")]
        public bool ShowLondon { get => bShowLondon; set { bShowLondon = value; RecalculateValues(); } }
        [Display(GroupName = "High Impact News", Name = "Show today's news")]
        public bool Show_News { get => bShowNews; set { bShowNews = value; RecalculateValues(); } }
        [Display(GroupName = "High Impact News", Name = "News font")]
        [Range(1, 900)]
        public int NewsFont
        { get => iNewsFont; set { iNewsFont = value; RecalculateValues(); } }

        private decimal VolSec(IndicatorCandle c) { return c.Volume / Convert.ToDecimal((c.LastTime - c.Time).TotalSeconds); }

        #endregion

        #region CONSTRUCTOR

        public BuySell() :
            base(true)
        {
            EnableCustomDrawing = true;
            DenyToChangePanel = true;
            SubscribeToDrawingEvents(DrawingLayouts.Historical);

            DataSeries[0] = _posSeries;
            DataSeries.Add(_negSeries);
            DataSeries.Add(_negWhite);
            DataSeries.Add(_posWhite);
            DataSeries.Add(_negBBounce);
            DataSeries.Add(_posBBounce);
            DataSeries.Add(_squeezie);
            DataSeries.Add(_paintBars);

            DataSeries.Add(_lineVWAP);
            DataSeries.Add(_lineEMA200);
            DataSeries.Add(_lineKAMA);

            Add(_ao);
            Add(_ft);
            Add(_sq);
            Add(_psar);
            Add(_kama9);
            Add(_VWAP);
            Add(_kama21);
            Add(_atr);
            Add(_hma);
            Add(SI);
        }

        #endregion

        #region INDICATORS

        private readonly VWAP _VWAP = new VWAP() { VWAPOnly = true, Type = VWAP.VWAPPeriodType.Daily, TWAPMode = VWAP.VWAPMode.VWAP, VolumeMode = VWAP.VolumeType.Total, Period = 300 };
        private readonly EMA Ema200 = new EMA() { Period = 200 };

        private readonly SMA _Sshort = new SMA() { Period = 3 };
        private readonly SMA _Slong = new SMA() { Period = 10 };
        private readonly SMA _Ssignal = new SMA() { Period = 16 };

        private readonly StackedImbalance SI = new StackedImbalance();
        private readonly RSI _rsi = new() { Period = 14 };
        private readonly ATR _atr = new() { Period = 14 };
        private readonly AwesomeOscillator _ao = new AwesomeOscillator();
        private readonly ParabolicSAR _psar = new ParabolicSAR();
        private readonly ADX _adx = new ADX() { Period = 10 };
        private readonly EMA _myEMA = new EMA() { Period = 21 };
        private readonly EMA _9 = new EMA() { Period = 9 };
        private readonly EMA _21 = new EMA() { Period = 21 };
        private readonly HMA _hma = new HMA() { };
        private readonly EMA fastEma = new EMA() { Period = 20 };
        private readonly EMA slowEma = new EMA() { Period = 40 };
        private readonly FisherTransform _ft = new FisherTransform() { Period = 10 };
        private readonly SuperTrend _st1 = new SuperTrend() { Period = 10, Multiplier = 1m };
        private readonly SuperTrend _st2 = new SuperTrend() { Period = 11, Multiplier = 2m };
        private readonly SuperTrend _st3 = new SuperTrend() { Period = 12, Multiplier = 3m };
        private readonly BollingerBands _bb = new BollingerBands() { Period = 20, Shift = 0, Width = 2 };
        private readonly KAMA _kama9 = new KAMA() { ShortPeriod = 2, LongPeriod = 109, EfficiencyRatioPeriod = 9 };
        private readonly KAMA _kama21 = new KAMA() { ShortPeriod = 2, LongPeriod = 109, EfficiencyRatioPeriod = 21 };
        private readonly T3 _t3 = new T3() { Period = 10, Multiplier = 1 };
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
            if (ChartInfo is null || InstrumentInfo is null)
                return;

            if (bRefreshLines)
            foreach (var l in lsDays)
            {
                var xH = ChartInfo.PriceChartContainer.GetXByBar(CurrentBar, false);
                var yH = ChartInfo.PriceChartContainer.GetYByPrice(l.price1, false);
                var yH2 = ChartInfo.PriceChartContainer.GetYByPrice(l.price2, false);
                var yWidth = ChartInfo.ChartContainer.Region.Width;
                RenderPen highPen = new RenderPen(l.c, 1, DashStyle.Dash);
                var rectPen = new Pen(new SolidBrush(l.c)) { Width = 1 };

                if (l.price2 > 0)
                {
                    DrawingRectangle dr = new DrawingRectangle(1, l.price1, CurrentBar, l.price2, rectPen, new SolidBrush(l.c));
                    if (!Rectangles.Contains(dr))
                        Rectangles.Add(dr);
                }
                else
                    context.DrawLine(highPen, 0, yH, xH, yH);

                context.DrawString(l.label, new RenderFont("Arial", iFontSize), l.c, xH, yH);
                bRefreshLines = false;
            }

            if (bShowLondon)
            {
                defibPen.Color = DefaultColors.Lime.Convert();
                var xH = ChartInfo.PriceChartContainer.GetXByBar(_highBarL, false);
                var yH = ChartInfo.PriceChartContainer.GetYByPrice(_highestL, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, _highL, yH, defibPen.RenderObject.Color);

                var xL = ChartInfo.PriceChartContainer.GetXByBar(_lowBarL, false);
                var yL = ChartInfo.PriceChartContainer.GetYByPrice(_lowestL, false);
                context.DrawLine(defibPen.RenderObject, xL, yL, Container.Region.Right, yL);
                DrawString(context, _lowL, yL, defibPen.RenderObject.Color);
            }

            if (!bShowNews && !bAdvanced && !bShowRevPattern)
                return;

            FontSetting Font = new("Arial", iFontSize);
            var renderString = "Howdy";
            var stringSize = context.MeasureString(renderString, Font.RenderObject);
            int x4 = 0;
            int y4 = 0;

            for (var bar = FirstVisibleBarNumber; bar <= LastVisibleBarNumber; bar++)
            {
                renderString = bar.ToString(CultureInfo.InvariantCulture);
                stringSize = context.MeasureString(renderString, Font.RenderObject);

                var font2 = new RenderFont("Arial", iNewsFont);
                var fontB = new RenderFont("Arial", iNewsFont, FontStyle.Bold);
                int upY = 50;
                int upX = ChartArea.Width - 250;
                int iTrades = 0;

                if (bShowNews)
                {
                    RenderFont font;
                    Size textSize;
                    int currY = 40;

                    font = new RenderFont("Arial", iNewsFont + 2);
                    textSize = context.MeasureString("Today's News:", font);
                    context.DrawString("Today's News:", font, Color.YellowGreen, 50, currY);
                    currY += textSize.Height + 10;
                    font = new RenderFont("Arial", iNewsFont);

                    foreach (string s in lsH)
                    {
                        textSize = context.MeasureString(s, font);
                        context.DrawString("High - " + s, font, Color.DarkOrange, 50, currY);
                        currY += textSize.Height;
                    }
                    currY += 9;
                    foreach (string s in lsM)
                    {
                        textSize = context.MeasureString(s, font);
                        context.DrawString("Med  - " + s, font, Color.Gray, 50, currY);
                        currY += textSize.Height;
                    }
                }

            }
        }

        protected void DrawText(int bBar, String strX, Color cI, Color cB, bool bOverride = false, bool bSwap = false)
        {
            var candle = GetCandle(bBar);
            bars ty;

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

        private readonly ValueDataSeries _posBBounce = new("Bollinger Bounce Up") { Color = MColors.LightGreen, VisualType = VisualMode.Block, Width = 9 };
        private readonly ValueDataSeries _negBBounce = new("Bollinger Bounce Down") { Color = MColors.LightPink, VisualType = VisualMode.Block, Width = 9 };

        private readonly ValueDataSeries _posSeries = new("Regular Buy Signal") { Color = MColor.FromArgb(255, 0, 255, 0), VisualType = VisualMode.Dots, Width = 2 };
        private readonly ValueDataSeries _negSeries = new("Regular Sell Signal") { Color = MColor.FromArgb(255, 255, 104, 48), VisualType = VisualMode.Dots, Width = 2 };

        private readonly ValueDataSeries _lineVWAP = new("VWAP") { Color = MColor.FromArgb(180, 30, 114, 250), VisualType = VisualMode.Line, Width = 4 };
        private readonly ValueDataSeries _lineEMA200 = new("EMA 200") { Color = MColor.FromArgb(255, 165, 166, 164), VisualType = VisualMode.Line, Width = 4 };
        private readonly ValueDataSeries _lineKAMA = new("KAMA 9") { Color = MColor.FromArgb(180, 252, 186, 3), VisualType = VisualMode.Line, Width = 3 };

        #endregion

        #region Stock HTTP Fetch

        private void ParseStockEvents(String result, int bar)
        {
            int iJSONStart = 0;
            int iJSONEnd = -1;
            String sFinalText = String.Empty; String sNews = String.Empty; String name = String.Empty; String impact = String.Empty; String time = String.Empty; String actual = String.Empty; String previous = String.Empty; String forecast = String.Empty;

            try
            {
                iJSONStart = result.IndexOf("window.calendarComponentStates[1] = ");
                iJSONEnd = result.IndexOf("\"}]}],", iJSONStart);
                sFinalText = result.Substring(iJSONStart, iJSONEnd - iJSONStart);
                sFinalText = sFinalText.Replace("window.calendarComponentStates[1] = ", "");
                sFinalText += "\"}]}]}";

                var jsFile = JObject.Parse(sFinalText);
                foreach (JToken j3 in (JArray)jsFile["days"])
                {
                    JToken j2 = j3.SelectToken("events");
                    foreach (JToken j in j2)
                    {
                        name = j["name"].ToString();
                        impact = j["impactTitle"].ToString();
                        time = j["timeLabel"].ToString();
                        actual = j["actual"].ToString();
                        previous = j["previous"].ToString();
                        forecast = j["forecast"].ToString();
                        sNews = time + "     " + name;
                        if (previous.ToString().Trim().Length > 0)
                            sNews += " (Prev: " + previous + ", Forecast: " + forecast + ")";
                        if (impact.Contains("High"))
                            lsH.Add(sNews);
                        if (impact.Contains("Medium"))
                            lsM.Add(sNews);
                    }
                }
            }
            catch { }
        }

        private void LoadStock(int bar)
        {
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://www.forexfactory.com/calendar?day=today");
                myRequest.Method = "GET";
                myRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                ParseStockEvents(result, bar);
                bNewsProcessed = true;
            }
            catch { }
        }

        private void play(String s)
        {
            try
            {
                SoundPlayer my_wave_file = new SoundPlayer(sWavDir + @"\" + s + ".wav");
                my_wave_file.PlaySync();
            }
            catch (Exception)            {            }
        }

        #endregion

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar == 0)
            {
                DataSeries.ForEach(x => x.Clear());
                HorizontalLinesTillTouch.Clear();
                Rectangles.Clear();
                _lastBarCounted = false;
                return;
            }
            if (bar < 6)
                return;

            MarkOpenSession(bar);

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

            var c0Body = Math.Abs(candle.Close - candle.Open);
            var c1Body = Math.Abs(p1C.Close - p1C.Open);
            var c2Body = Math.Abs(p2C.Close - p2C.Open);
            var c3Body = Math.Abs(p3C.Close - p3C.Open);
            var c4Body = Math.Abs(p4C.Close - p4C.Open);

            var bShaved = red && candle.Close == candle.Low ? true : green && candle.Close == candle.High ? true : false;

            var upWickLarger = c0R && Math.Abs(candle.High - candle.Open) > Math.Abs(candle.Low - candle.Close);
            var downWickLarger = c0G && Math.Abs(candle.Low - candle.Open) > Math.Abs(candle.Close - candle.High);

            var ThreeOutUp = c2R && c1G && c0G && p1C.Open < p2C.Close && p2C.Open < p1C.Close && Math.Abs(p1C.Open - p1C.Close) > Math.Abs(p2C.Open - p2C.Close) && candle.Close > p1C.Low;

            var ThreeOutDown = c2G && c1R && c0R && p1C.Open > p2C.Close && p2C.Open > p1C.Close && Math.Abs(p1C.Open - p1C.Close) > Math.Abs(p2C.Open - p2C.Close) && candle.Close < p1C.Low;

            decimal deltaIntense = 0;
            if (!candle.MaxDelta.Equals(null) && !candle.MinDelta.Equals(null) && !candle.Delta.Equals(null))
            {
                var candleSeconds = Convert.ToDecimal((candle.LastTime - candle.Time).TotalSeconds);
                if (candleSeconds is 0)
                    candleSeconds = 1;
                var volPerSecond = candle.Volume / candleSeconds;
                var deltaPer = candle.Delta > 0 ? (candle.Delta / candle.MaxDelta) : (candle.Delta / candle.MinDelta);
                deltaIntense = Math.Abs((candle.Delta * deltaPer) * volPerSecond);
                var deltaShaved = candle.Delta * deltaPer;
            }

            #endregion

            #region INDICATORS CALCULATE

            _myEMA.Calculate(pbar, value);
            _t3.Calculate(pbar, value);
            fastEma.Calculate(pbar, value);
            slowEma.Calculate(pbar, value);
            _9.Calculate(pbar, value);
            _21.Calculate(pbar, value);

            _bb.Calculate(pbar, value);
            _rsi.Calculate(pbar, value);
            Ema200.Calculate(pbar, value);

            var e200 = ((ValueDataSeries)Ema200.DataSeries[0])[pbar];
            var vwap = ((ValueDataSeries)_VWAP.DataSeries[0])[pbar];
            var kama9 = ((ValueDataSeries)_kama9.DataSeries[0])[pbar];

            var ao = ((ValueDataSeries)_ao.DataSeries[0])[pbar];
            var t3 = ((ValueDataSeries)_t3.DataSeries[0])[pbar];
            var fast = ((ValueDataSeries)fastEma.DataSeries[0])[pbar];
            var fastM = ((ValueDataSeries)fastEma.DataSeries[0])[pbar - 1];
            var fastN = ((ValueDataSeries)fastEma.DataSeries[0])[pbar - 2];
            var slow = ((ValueDataSeries)slowEma.DataSeries[0])[pbar];
            var slowM = ((ValueDataSeries)slowEma.DataSeries[0])[pbar - 1];
            var slowN = ((ValueDataSeries)slowEma.DataSeries[0])[pbar - 2];
            var sq1 = ((ValueDataSeries)_sq.DataSeries[0])[pbar];
            var sq2 = ((ValueDataSeries)_sq.DataSeries[1])[pbar];
            var psq1 = ((ValueDataSeries)_sq.DataSeries[0])[pbar - 1];
            var psq2 = ((ValueDataSeries)_sq.DataSeries[1])[pbar - 1];
            var ppsq1 = ((ValueDataSeries)_sq.DataSeries[0])[pbar - 2];
            var ppsq2 = ((ValueDataSeries)_sq.DataSeries[1])[pbar - 2];
            var f1 = ((ValueDataSeries)_ft.DataSeries[0])[pbar];
            var f2 = ((ValueDataSeries)_ft.DataSeries[1])[pbar];
            var stu1 = ((ValueDataSeries)_st1.DataSeries[0])[pbar];
            var stu2 = ((ValueDataSeries)_st2.DataSeries[0])[pbar];
            var stu3 = ((ValueDataSeries)_st3.DataSeries[0])[pbar];
            var std1 = ((ValueDataSeries)_st1.DataSeries[1])[pbar];
            var std2 = ((ValueDataSeries)_st2.DataSeries[1])[pbar];
            var std3 = ((ValueDataSeries)_st3.DataSeries[1])[pbar];
            var x = ((ValueDataSeries)_adx.DataSeries[0])[pbar];
            var nn = ((ValueDataSeries)_9.DataSeries[0])[pbar];
            var prev_nn = ((ValueDataSeries)_9.DataSeries[0])[pbar - 1];
            var twone = ((ValueDataSeries)_21.DataSeries[0])[pbar];
            var prev_twone = ((ValueDataSeries)_21.DataSeries[0])[pbar - 1];
            var myema = ((ValueDataSeries)_myEMA.DataSeries[0])[pbar];
            var psar = ((ValueDataSeries)_psar.DataSeries[0])[pbar];
            var ppsar = ((ValueDataSeries)_psar.DataSeries[0])[bar];
            var bb_mid = ((ValueDataSeries)_bb.DataSeries[0])[pbar]; // mid
            var bb_top = ((ValueDataSeries)_bb.DataSeries[1])[pbar]; // top
            var bb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[pbar]; // bottom
            var rsi = ((ValueDataSeries)_rsi.DataSeries[0])[pbar];
            var rsi1 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 1];
            var rsi2 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 2];
            var hma = ((ValueDataSeries)_hma.DataSeries[0])[pbar];
            var phma = ((ValueDataSeries)_hma.DataSeries[0])[pbar - 1];
            var stack = ((ValueDataSeries)SI.DataSeries[0])[pbar];

            // Linda MACD
            var macd = _Sshort.Calculate(pbar, value) - _Slong.Calculate(pbar, value);
            var signal = _Ssignal.Calculate(pbar, macd);
            var m3 = macd - signal;

            var hullUp = hma > phma;
            var hullDown = hma < phma;
            var fisherUp = (f1 < f2);
            var fisherDown = (f2 < f1);
            var macdUp = (macd > signal);
            var macdDown = (macd < signal);

            var psarBuy = (psar < candle.Close);
            var ppsarBuy = (ppsar < pcandle.Close);
            var psarSell = (psar > candle.Close);
            var ppsarSell = (ppsar > pcandle.Close);

            var eqHigh = c0R && c1R && c2G && c3G && (p1C.High > bb_top || p2C.High > bb_top) &&
                candle.Close < p1C.Close &&
                (p1C.Open == p2C.Close || p1C.Open == p2C.Close + _tick || p1C.Open + _tick == p2C.Close);

            var eqLow = c0G && c1G && c2R && c3R && (p1C.Low < bb_bottom || p2C.Low < bb_bottom) &&
                candle.Close > p1C.Close &&
                (p1C.Open == p2C.Close || p1C.Open == p2C.Close + _tick || p1C.Open + _tick == p2C.Close);

            var t1 = ((fast - slow) - (fastM - slowM)) * iWaddaSensitivity;
            var prevT1 = ((fastM - slowM) - (fastN - slowN)) * iWaddaSensitivity;
            var s1 = bb_top - bb_bottom;

            #endregion

            var bEMA200Bounce = false;
            var bVWAPBounce = false;
            var bKAMABounce = false;
            decimal c0UpWick = 0;
            decimal c0DownWick = 0;

            if (c0G)
            {
                bEMA200Bounce = (candle.Low < e200 && candle.Open > e200); // (candle.High > e200 && value < e200) ||
                bVWAPBounce = (candle.Low < vwap && candle.Open > vwap); // (candle.High > vwap && value < vwap) || 
                bKAMABounce = (candle.Low < kama9 && candle.Open > kama9);
                c0UpWick = Math.Abs(candle.High - candle.Close);
                c0DownWick = Math.Abs(candle.Low - candle.Open);
            }
            else if (c0R)
            {
                bEMA200Bounce = (candle.High > e200 && candle.Open < e200); // || (candle.Low < e200 && value > e200);
                bVWAPBounce = (candle.High > vwap && candle.Open < vwap); //  || (candle.Low < vwap && value > vwap);
                bKAMABounce = (candle.High > kama9 && candle.Open < kama9);
                c0UpWick = Math.Abs(candle.High - candle.Open);
                c0DownWick = Math.Abs(candle.Low - candle.Close);
            }

            if (bEMA200Bounce || bVWAPBounce)
                _paintBars[pbar] = MColor.FromRgb(255, 255, 255);

            if (bKAMABounce && bKAMAWick)
                _paintBars[pbar] = MColor.FromRgb(255, 255, 255);

            #region BUY / SELL

            if (bVolumeImbalances)
            {
                var highPen = new Pen(new SolidBrush(Color.FromArgb(255, 169, 97, 250))) { Width = 3, DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                if (green && c1G && candle.Open > p1C.Close)
                {
                    HorizontalLinesTillTouch.Add(new LineTillTouch(pbar, candle.Open, highPen));
                    _negWhite[pbar] = candle.Low - (_tick * 2);
                }
                if (red && c1R && candle.Open < p1C.Close)
                {
                    HorizontalLinesTillTouch.Add(new LineTillTouch(pbar, candle.Open, highPen));
                    _posWhite[pbar] = candle.High + (_tick * 2);
                }
            }

            if ((!macdUp && bUseMACD) || (psarSell && bUsePSAR) || (!fisherUp && bUseFisher) || (value < t3 && bUseT3) || (value < kama9 && bUseKAMA) || (t1 < 0 && bUseWaddah) || (ao < 0 && bUseAO) || (stu2 == 0 && bUseSuperTrend) || (sq1 < 0 && bUseSqueeze) || (bUseHMA && hullDown))
                bShowUp = false;

            if (bShowUp && bShowRegularBuySell)
                _posSeries[pbar] = candle.Low - (_tick * iOffset);

            if ((psarBuy && bUsePSAR) || (!macdDown && bUseMACD) || (!fisherDown && bUseFisher) || (value > kama9 && bUseKAMA) || (value > t3 && bUseT3) || (t1 >= 0 && bUseWaddah) || (ao > 0 && bUseAO) || (std2 == 0 && bUseSuperTrend) || (sq1 > 0 && bUseSqueeze) || (bUseHMA && hullUp))
                bShowDown = false;

            if (bShowDown && bShowRegularBuySell)
                _negSeries[pbar] = candle.High + _tick * iOffset;

            if (canColor > 1)
            {
                var waddah = Math.Min(Math.Abs(t1) + 70, 255);
                if (canColor == 2)
                    _paintBars[pbar] = t1 > 0 ? MColor.FromArgb(255, 0, (byte)waddah, 0) : MColor.FromArgb(255, (byte)waddah, 0, 0);

                var filteredSQ = Math.Min(Math.Abs(sq1 * 25), 255);
                if (canColor == 3)
                    _paintBars[pbar] = sq1 > 0 ? MColor.FromArgb(255, 0, (byte)filteredSQ, 0) : MColor.FromArgb(255, (byte)filteredSQ, 0, 0);

                var filteredDelta = Math.Min(Math.Abs(candle.Delta), 255);
                if (canColor == 4)
                    _paintBars[pbar] = candle.Delta > 0 ? MColor.FromArgb(255, 0, (byte)filteredDelta, 0) : MColor.FromArgb(255, (byte)filteredDelta, 0, 0);

                var filteredLinda = Math.Min(Math.Abs(m3 * iMACDSensitivity), 255);
                if (canColor == 5)
                    _paintBars[pbar] = m3 > 0 ? MColor.FromArgb(255, 0, (byte)filteredLinda, 0) : MColor.FromArgb(255, (byte)filteredLinda, 0, 0);
            }

            #endregion

            #region ADVANCED LOGIC

            int iLocalTouch = 0;
            foreach(LineTillTouch ltt in HorizontalLinesTillTouch)
                if (ltt.Finished)
                    iLocalTouch++;

            if (iLocalTouch > iTouched)
            {
                iTouched = iLocalTouch;
                // _paintBars[bar] = MColor.FromRgb(255, 255, 255);
                bVolImbFinished = true;
            }

            if (bShowEngBB)
            {
                var gPen = new Pen(new SolidBrush(Color.Transparent)) { Width = 3 };
                var rPen = new Pen(new SolidBrush(Color.Transparent)) { Width = 3 };

                if ((candle.Low < bb_bottom || p1C.Low < bb_bottom || p2C.Low < bb_bottom) && c0Body > c1Body && c0G && c1R && candle.Close > p1C.Open)
                {
                    Rectangles.Add(new DrawingRectangle(pbar, p1C.Low - 499, pbar, p1C.High + 499, gPen, new SolidBrush(colorEngulfg)));
                }
                else if ((candle.High > bb_top || p1C.High > bb_top || p2C.High > bb_top) && c0Body > c1Body && c0R && c1G && candle.Open < p1C.Close)
                {
                    Rectangles.Add(new DrawingRectangle(pbar, p1C.Low - 499, pbar, p1C.High + 499, rPen, new SolidBrush(colorEngulfr)));
                }
            }

            if (bShowLines)
            {
                _lineEMA200[pbar] = e200;
                _lineKAMA[pbar] = kama9;
                _lineVWAP[pbar] = vwap;
            }

            // Squeeze momentum relaxer show
            if (sq1 > 0 && sq1 < psq1 && psq1 > ppsq1 && bShowSqueeze)
            {
                DrawText(pbar, "▼", Color.Yellow, Color.Transparent, false, true); // "▲" "▼"
            }
                
            if (sq1 < 0 && sq1 > psq1 && psq1 < ppsq1 && bShowSqueeze)
            {
                DrawText(pbar, "▲", Color.Yellow, Color.Transparent, false, true);
            }

            if (bAdvanced)
            {
                if (c4Body > c3Body && c3Body > c2Body && c2Body > c1Body && c1Body > c0Body)
                    if ((candle.Close > p1C.Close && p1C.Close > p2C.Close && p2C.Close > p3C.Close) ||
                    (candle.Close < p1C.Close && p1C.Close < p2C.Close && p2C.Close < p3C.Close))
                {
                        DrawText(pbar, "Stairs", Color.Yellow, Color.Transparent);
                }

                if (eqHigh)
                {
                    DrawText(pbar - 1, "Eq Hi", Color.Lime, Color.Transparent, false, true);
                }

                if (eqLow)
                {
                    DrawText(pbar - 1, "Eq Low", Color.Yellow, Color.Transparent, false, true);
                }
            }

            if (bShowRevPattern)
            {
                if (c0G && c1R && c2R && VolSec(p1C) > VolSec(p2C) && VolSec(p2C) > VolSec(p3C) && candle.Delta < 0)
                {
                    DrawText(pbar, "Vol\nRev", Color.Yellow, Color.Transparent, false, true);
                }

                if (c0R && c1G && c2G && VolSec(p1C) > VolSec(p2C) && VolSec(p2C) > VolSec(p3C) && candle.Delta > 0)
                {
                    DrawText(pbar, "Vol\nRev", Color.Lime, Color.Transparent, false, true);
                }

                if (ThreeOutUp)
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
                }
                   
                if (c0G && c1G && candle.Close > p1C.Close && (rsi < 25 || rsi1 < 25 || rsi2 < 25) &&
                    c2R && p2C.Low <= (bb_bottom + (_tick * 30)))
                {
                    DrawText(pbar, "TR", Color.Yellow, Color.BlueViolet, false, true);
                }
            }

            #endregion

            #region ALERTS LOGIC

            DrawTraderSmarts();

            if (_lastBar != bar)
            {
                if (_lastBarCounted && UseAlerts)
                {
                    DrawTraderSmarts();
                    var priceString = candle.Close.ToString();
                }
                _lastBar = bar;
            }
            else
            {
                if (!_lastBarCounted)
                    _lastBarCounted = true;
            }

            #endregion

            if (!bNewsProcessed && bShowNews)
                LoadStock(pbar);
        }

        #region MISC FUNCTIONS

        private void AddRecord(string price, string price2, string s)
        {
            try
            {
                days a = new days();
                a.c = s.Contains("Long") ? Color.Green : s.Contains("Short") ? 
                    Color.Red : s.Contains("Sand") ? Color.FromArgb(255, 169, 97, 250) : Color.Yellow;
                a.label = s;
                a.price1 = Convert.ToDecimal(price);
                a.price2 = Convert.ToDecimal(price2);
                lsDays.Add(a);
            }
            catch { }
        }

        private void DrawTraderSmarts()
        {
            lsDays.Clear();

            try
            {
                StreamReader sr = new StreamReader(tsFile);
                while (sr.Peek() >= 0)
                {
                    string s1, s2;
                    string s = sr.ReadLine();
                    if (s.Contains(" - "))
                    {
                        // 4610.75 - 4608.75 Range Short
                        s = s.Replace(" - ", "-");
                        int i = s.IndexOf(' ');
                        if (i >= 0)
                        {
                            s1 = s.Substring(0, i).Trim();
                            s2 = s.Substring(i, s.Length - i).Trim();
                            string[] price = s1.Split('-');
                            AddRecord(s1.Trim(), s1.Trim(), s2);
                        }
                    }
                    else if (s.Contains(".") && !s.Contains("Numbers"))
                    {
                        // 4597.75 Line in the Sand
                        int i = s.IndexOf(' ');
                        if (i >= 0)
                        {
                            s1 = s.Substring(0, i).Trim();
                            s2 = s.Substring(i, s.Length - i).Trim();
                            AddRecord(s1.Trim(), s1.Trim(), s2);
                        }
                    }
                    else if (s.Contains("Numbers"))
                    {
                        // NQ MTS Numbers: 20390.25, 19501, 19234.75, 18912, 18517, 18420
                        s = s.Replace("NQ MTS Numbers: ", "").Replace(" ", "");
                        string[] prices = s.Split(',');
                        foreach (string ac in prices)
                            AddRecord(ac.Trim(), ac.Trim(), "MTS");
                    }
                }
            }
            catch { }
            bRefreshLines = true;
        }

            private void MarkOpenSession(int bar)
        {
            var candle = GetCandle(bar);
            var diff = InstrumentInfo.TimeZone;
            var time = candle.Time.AddHours(diff);
            var today = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();

            if (time > DateTime.Parse(today + " 08:20AM") && time < DateTime.Parse(today + " 08:29AM"))
            {
                _highest = candle.High;
                _highBar = bar;
                _lowest = candle.Low;
                _lowBar = bar;
            }

            if (time > DateTime.Parse(today + " 08:30AM") && time < DateTime.Parse(today + " 09:30AM"))
            {
                if (candle.High > _highest)
                {
                    _highest = candle.High;
                    _highBar = bar;
                }
                if (candle.Low < _lowest)
                {
                    _lowest = candle.Low;
                    _lowBar = bar;
                }
            }

            // LONDON SESSION TIMES
            if (time > DateTime.Parse(today + " 01:50AM") && time < DateTime.Parse(today + " 01:59AM"))
            {
                _highestL = candle.High;
                _highBarL = bar;
                _lowestL = candle.Low;
                _lowBarL = bar;
            }

            if (time > DateTime.Parse(today + " 02:00AM") && time < DateTime.Parse(today + " 03:00AM"))
            {
                if (candle.High > _highestL)
                {
                    _highestL = candle.High;
                    _highBarL = bar;
                }
                if (candle.Low < _lowestL)
                {
                    _lowestL = candle.Low;
                    _lowBarL = bar;
                }
            }
        }

        #endregion

    }
}