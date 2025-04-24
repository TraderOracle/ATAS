
namespace ATAS.Indicators.Technical
{
    #region INCLUDES

    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using ATAS.Indicators;
    using ATAS.Indicators.Drawing;
    using static ATAS.Indicators.Technical.SampleProperties;
    using Color = System.Drawing.Color;
    using MColors = System.Windows.Media.Colors;
    using MColor = System.Windows.Media.Color;
    using Pen = System.Drawing.Pen;
    using System.Text;
    using Newtonsoft.Json;
    using System.Net;

    #endregion

    [DisplayName("TO VolImb")]
    public class TOVolImb : Indicator
    {
        #region VARIABLES
        private const String sVersion = "1.0";

        private const int VOL_IMB_WICK_G = 1;
        private const int VOL_IMB_WICK_R = 2;
        private const int VOL_IMB_FILL = 3;
        private const int VOL_IMB_REVERSE = 4;
        private const int VOL_IMB_TOUCH_G = 5;
        private const int VOL_IMB_TOUCH_R = 6;
        private const int VOL_IMB_ENGULFING = 7;
        private const int VOL_IMB_DROP_G = 8;
        private const int VOL_IMB_DROP_R = 9;
        private const int DOUBLE_VOL_IMB_DROP_G = 10;
        private const int DOUBLE_VOL_IMB_DROP_R = 11;
        private const int TRAMPOLINE = 12;
        private const int ENG_BB = 13;
        private int iLastTouchBar = 0;
        private decimal iLastTouchPrice = 0;
        private int iLastWickBar = 0;
        private decimal iLastWickPrice = 0;
        private HttpClient client;
        private WebClient client1 = new WebClient();
        private readonly PaintbarsDataSeries _paintBars = new("Paint bars");
        List<int> LineTouches = new List<int>();
        private int iFutureSound = 0;
        private String sWavDir = @"C:\Program Files (x86)\ATAS Platform\Sounds";
        private int _lastBar = -1;
        private bool _lastBarCounted;
        private int iOffset = 9;
        private int iFontSize = 10;
        private int iMinBars = 3;
        private int iLineWidth = 2;

        [Display(Name = "Line Width", GroupName = "General", Order = int.MaxValue)]
        [Range(1, 90)]
        public int LineWidth { get => iLineWidth; set { iLineWidth = value; RecalculateValues(); } }

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

            ab.Days = 90;
            ab.AbsorptionRatio = 5;
            ab.AbsorptionRange = 3;
            ab.AbsorptionVolume = 18;

            Add(ab);
        }

        private readonly RSI _rsi = new() { Period = 14 };
        private readonly BollingerBands _bb = new BollingerBands() { Period = 20, Shift = 0, Width = 2 };
        private readonly Absorption ab = new Absorption();

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

            var pbar = bar - 1;
            var pcandle = GetCandle(bar - 1);
            var cc = GetCandle(bar);
            var candle = GetCandle(bar);

            value = candle.Close;
            var chT = ChartInfo.ChartType;

            decimal _tick = ChartInfo.PriceChartContainer.Step;
            var p1C = GetCandle(bar - 1);
            var p2C = GetCandle(bar - 2);
            var p3C = GetCandle(bar - 3);
            var p4C = GetCandle(bar - 4);

            var close = cc.Close;
            var open = cc.Open;
            var pclose = cc.Close;
            var popen = cc.Open;

            var red = cc.Close < cc.Open;
            var green = cc.Close > cc.Open;
            var c0G = cc.Open < cc.Close;
            var c0R = cc.Open > cc.Close;
            var c1G = p1C.Open < p1C.Close;
            var c1R = p1C.Open > p1C.Close;
            var c2G = p2C.Open < p2C.Close;
            var c2R = p2C.Open > p2C.Close;

            var c0Body = Math.Abs(candle.Close - candle.Open);
            var c1Body = Math.Abs(p1C.Close - p1C.Open);
            var c2Body = Math.Abs(p2C.Close - p2C.Open);
            var c3Body = Math.Abs(p3C.Close - p3C.Open);
            var c4Body = Math.Abs(p4C.Close - p4C.Open);

            _bb.Calculate(bar, value);
            _rsi.Calculate(bar, value);
            //ab.Calculate(pbar, value);

            //var abb = ((ValueDataSeries)ab.DataSeries[0])[pbar];
            var bb_mid = ((ValueDataSeries)_bb.DataSeries[0])[bar]; // mid
            var bb_top = ((ValueDataSeries)_bb.DataSeries[1])[bar]; // top
            var bb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[bar]; // bottom
            var rsi = ((ValueDataSeries)_rsi.DataSeries[0])[bar];
            var rsi1 = ((ValueDataSeries)_rsi.DataSeries[0])[bar - 1];
            var rsi2 = ((ValueDataSeries)_rsi.DataSeries[0])[bar - 2];

            #endregion

            //List<LineTillTouch> absorb = ab.HorizontalLinesTillTouch;
            //if (absorb.Count > 0)
            //{
            //    foreach (var line in absorb)
            //    {
            //        if (line.SecondBar == bar)
            //            _paintBars[bar] = MColor.FromRgb(255, 255, 255);
            //    }
            //}


            // ENGULFING CANDLE OFF THE BOLLINGER BAND
            if ((p1C.High > bb_top && p2C.High > bb_top) &&
                   (c1Body > c2Body) &&
                   (c1R && c2G) &&
                   (candle.Open < pcandle.Open || pcandle.Open == candle.Close))
            {
                _paintBars[pbar] = MColor.FromRgb(255, 255, 255);
                iFutureSound = ENG_BB;
            }
            else if ((p1C.Low < bb_bottom && p2C.Low < bb_bottom ) &&
                    (c1Body > c2Body) &&
                    (c1G && c2R) &&
                    (candle.Close > pcandle.Close || pcandle.Close == candle.Open))
            {
                _paintBars[pbar] = MColor.FromRgb(255, 255, 255);
                iFutureSound = ENG_BB;
            }



            #region REVERSAL ALERTS

            // INSANT reversal, next bar
            if (c0G && iLastWickBar == bar - 1 && iLastWickPrice < close && iLastWickPrice < open)
                iFutureSound = VOL_IMB_REVERSE;
            // INSANT reversal, next bar
            if (c0R && iLastWickBar == bar - 1 && iLastWickPrice > close && iLastWickPrice > open)
                iFutureSound = VOL_IMB_REVERSE;

            // Two or three candle reversal
            if (c0G &&
                (iLastWickBar == bar - 1 || iLastWickBar == bar - 2 || iLastWickBar == bar - 3) &&
                ((p1C.Close > iLastWickPrice && p1C.Open > iLastWickPrice) ||
                (p2C.Close > iLastWickPrice && p2C.Open > iLastWickPrice) ||
                (close > iLastWickPrice && open > iLastWickPrice)))
                iFutureSound = VOL_IMB_REVERSE;
            // Two or three candle reversal
            if (c0R &&
                (iLastWickBar == bar - 1 || iLastWickBar == bar - 2 || iLastWickBar == bar - 3) &&
                ((p1C.Close < iLastWickPrice && p1C.Open < iLastWickPrice) ||
                (p2C.Close < iLastWickPrice && p2C.Open < iLastWickPrice) ||
                (close < iLastWickPrice && open < iLastWickPrice)))
                iFutureSound = VOL_IMB_REVERSE;

            // Two or three candle reversal
            if (c0G &&
                (iLastTouchBar == bar - 1 || iLastTouchBar == bar - 2 || iLastTouchBar == bar - 3) &&
                ((p1C.Close > iLastTouchPrice && p1C.Open > iLastTouchPrice) ||
                (p2C.Close > iLastTouchPrice && p2C.Open > iLastTouchPrice) ||
                (close > iLastTouchPrice && open > iLastTouchPrice)))
                iFutureSound = VOL_IMB_REVERSE;
            // Two or three candle reversal
            if (c0R &&
                (iLastTouchBar == bar - 1 || iLastTouchBar == bar - 2 || iLastTouchBar == bar - 3) &&
                ((p1C.Close < iLastTouchPrice && p1C.Open < iLastTouchPrice) ||
                (p2C.Close < iLastTouchPrice && p2C.Open < iLastTouchPrice) ||
                (close < iLastTouchPrice && open < iLastTouchPrice)))
                iFutureSound = VOL_IMB_REVERSE;

            #endregion


            #region VOLUME IMBALANCES

            var highPen = new Pen(new SolidBrush(Color.FromArgb(255, 156, 227, 255)))
            { Width = iLineWidth, DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };

            if (c2G && c1G && p1C.Open > p2C.Close)
            {
                if (LineTouches.IndexOf(pbar) == -1)
                {
                    LineTouches.Add(pbar);
                    HorizontalLinesTillTouch.Add(new LineTillTouch(pbar, pcandle.Open, highPen));
                    if (p1C.Close < bb_mid)
                    {
                        iFutureSound = VOL_IMB_DROP_G;
                    }
                    if (LineTouches.Contains(pbar - 1))
                        iFutureSound = DOUBLE_VOL_IMB_DROP_G;
                }
            }

            if (c2R && c1R && p1C.Open < p2C.Close)
            {
                if (LineTouches.IndexOf(pbar) == -1)
                {
                    LineTouches.Add(pbar);
                    HorizontalLinesTillTouch.Add(new LineTillTouch(pbar, pcandle.Open, highPen));
                    if (p1C.Close > bb_mid)
                    {
                        iFutureSound = VOL_IMB_DROP_R;
                    }
                    if (LineTouches.Contains(pbar - 1))
                        iFutureSound = DOUBLE_VOL_IMB_DROP_R;
                }
            }

            foreach (LineTillTouch ltt in HorizontalLinesTillTouch)
            {
                int iDistance = ltt.SecondBar - ltt.FirstBar;

                // WICKED THE VOLUME IMBALANCE
                if (ltt.Finished && ltt.SecondBar == bar && iDistance > iMinBars && cc.High > ltt.SecondPrice && cc.Close < ltt.SecondPrice && cc.Open < ltt.SecondPrice)
                {
                    iLastWickBar = bar;
                    iLastWickPrice = ltt.SecondPrice;
                    iFutureSound = c0G ? VOL_IMB_WICK_G : VOL_IMB_WICK_R;
                    _paintBars[bar] = MColor.FromRgb(255, 255, 255);
                    break;
                }
                // TOUCHED THE VOLUME IMBALANCE
                else if (ltt.Finished && ltt.SecondBar == bar && iDistance > iMinBars)
                {
                    iLastTouchBar = bar;
                    iLastTouchPrice = ltt.SecondPrice;
                    iFutureSound = c0G ? VOL_IMB_TOUCH_G : VOL_IMB_TOUCH_R;
                    _paintBars[bar] = MColor.FromRgb(255, 255, 255);
                    break;
                }
                else if (ltt.Finished && iDistance < iMinBars)
                {
                    HorizontalLinesTillTouch.Remove(ltt);
                    break;
                }
            }

            #endregion

            #region TRAMPOLINE

            if (c0R && c1R && candle.Close < p1C.Close && (rsi >= 70 || rsi1 >= 70 || rsi2 >= 70) &&
                c2G && p2C.High >= (bb_top - (_tick * 30)))
            {
                DrawText(bar, "TR", Color.Black, Color.Lime, false, true);
                iFutureSound = TRAMPOLINE;
            }

            if (c0G && c1G && candle.Close > p1C.Close && (rsi < 25 || rsi1 < 25 || rsi2 < 25) &&
                c2R && p2C.Low <= (bb_bottom + (_tick * 30)))
            {
                DrawText(bar, "TR", Color.Black, Color.Lime, false, true);
                iFutureSound = TRAMPOLINE;
            }

            #endregion

            #region ALERTS LOGIC

            if (_lastBar != bar)
            {
                if (_lastBarCounted)
                {
                    var priceString = candle.Close.ToString();
                    DateTime now2 = DateTime.Now;

                    switch (iFutureSound)
                    {
                        // TWO VOLUME IMBALANCEs dropped right after each other.  Good buy/sell signals
                        case DOUBLE_VOL_IMB_DROP_G:
                            Task.Run(() => SendWebhookAndWriteToFile("BUY SIGNAL on", InstrumentInfo.Instrument, priceString, ""));
                            break;
                        case DOUBLE_VOL_IMB_DROP_R:
                            Task.Run(() => SendWebhookAndWriteToFile("SELL SIGNAL on", InstrumentInfo.Instrument, priceString, ""));
                            break;

                        // VOLUME IMBALANCE dropped near the low/high of the bollinger bands
                        case VOL_IMB_DROP_G:
                            Task.Run(() => SendWebhookAndWriteToFile("GREEN WATCH on", InstrumentInfo.Instrument, priceString, "trampoline"));
                            break;
                        case VOL_IMB_DROP_R:
                            Task.Run(() => SendWebhookAndWriteToFile("RED WATCH on", InstrumentInfo.Instrument, priceString, "trampoline"));
                            break;



                        // standard touch.  nothing to trade
                        case VOL_IMB_TOUCH_G:
                            Task.Run(() => SendWebhookAndWriteToFile("VolImb filled on", InstrumentInfo.Instrument, priceString, ""));
                            break;
                        case VOL_IMB_TOUCH_R:
                            Task.Run(() => SendWebhookAndWriteToFile("VolImb filled on", InstrumentInfo.Instrument, priceString, ""));
                            break;



                        case TRAMPOLINE:
                            Task.Run(() => SendWebhookAndWriteToFile("TRAMPOLINE on", InstrumentInfo.Instrument, priceString, ""));
                            break;
                        case ENG_BB:
                            Task.Run(() => SendWebhookAndWriteToFile("ENG off BB on", InstrumentInfo.Instrument, priceString, ""));
                            break;



                        case VOL_IMB_WICK_G:
                            Task.Run(() => SendWebhookAndWriteToFile("VolImb WICK on", InstrumentInfo.Instrument, priceString, ""));
                            break;
                        case VOL_IMB_WICK_R:
                            Task.Run(() => SendWebhookAndWriteToFile("VolImb WICK on", InstrumentInfo.Instrument, priceString, ""));
                            break;
                        case VOL_IMB_FILL:
                            Task.Run(() => SendWebhookAndWriteToFile("VolImb FILL on", InstrumentInfo.Instrument, priceString, ""));
                            break;

                        case VOL_IMB_ENGULFING:
                            Task.Run(() => SendWebhookAndWriteToFile("VolImb ENGULFING on", InstrumentInfo.Instrument, priceString, ""));
                            break;
                        case VOL_IMB_REVERSE:
                            Task.Run(() => SendWebhookAndWriteToFile("VolImb REVERSE on", InstrumentInfo.Instrument, priceString, ""));
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

        #region DISCORD INTEGRATION

        private async Task SendWebhook(string message, string ticker, string price)
        {
            DateTime bitches = new DateTime();
            bitches = DateTime.Now;
            bitches = bitches.AddHours(1);
            var fullMessage = $"{message} {ticker} at ${price} - " + bitches.ToString("h:mm:ss tt") + " EST";

            client1.Headers.Add("Content-Type", "application/json");
            string payload2 = "{\"content\": \"" + fullMessage + "\"}";
            client1.UploadData(whurl, Encoding.UTF8.GetBytes(payload2));
        }
        private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private async Task WriteToTextFile(string file)
        {
            await semaphore.WaitAsync();
            try
            {
                // System.Diagnostics.Process.Start("cmd.exe", "/c " + sWavDir + @"\copyimage.bat " + file);
            }
            finally
            {
                semaphore.Release();
            }
        }

        private async Task SendWebhookAndWriteToFile(string message, string ticker, string price, string file)
        {
            var sendWebhookTask = SendWebhook(message, ticker, price);
            //var writeToTextFileTask = WriteToTextFile(file);

            await Task.WhenAll(sendWebhookTask);
        }

        private void play(String s)
        {
            try
            {
                System.Diagnostics.Process.Start("cmd.exe", "/c " + sWavDir + "\\" + s + ".wav");
            }
            catch { }
        }

        #endregion

    }
}