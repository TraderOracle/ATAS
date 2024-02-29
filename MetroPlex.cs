using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Diagnostics;
using ATAS.DataFeedsCore;
using ATAS.Indicators;
using ATAS.Indicators.Technical;
using ATAS.Strategies.Chart;
using OFT.Rendering.Context;
using OFT.Rendering.Tools;
using static ATAS.Indicators.Technical.SampleProperties;
using Color = System.Drawing.Color;
using String = System.String;

namespace MetroPlex
{
    public class MetroPlex : ChartStrategy
    {
        private Stopwatch clock = new Stopwatch();
        private Rectangle rc = new Rectangle() { X = 50, Y = 50, Height = 200, Width = 400 };
        private DateTime dtStart = DateTime.Now;
        private String sLastTrade = String.Empty;
        private int iFontSize = 9;
        private int _prevBar = -1;
        private int iPrevOrderBar = -1;

        private const String sVersion = "Beta 1.0";

        [Display(Name = "Font Size", GroupName = "Drawing", Order = int.MaxValue)]
        [Range(1, 90)]
        public int TextFont { get => iFontSize; set { iFontSize = value; RecalculateValues(); } }

        private int iAdvMaxContracts = 6;
        private int iMaxLoss = 50000;
        private int iMaxProfit = 50000;

        [Display(Name = "Max simultaneous contracts", GroupName = "General", Order = int.MaxValue)]
        [Range(1, 90)]
        public int AdvMaxContracts { get => iAdvMaxContracts; set { iAdvMaxContracts = value; RecalculateValues(); } }

        [Display(GroupName = "General", Name = "Maximum Loss", Description = "Maximum amount of money lost before the bot shuts off")]
        [Range(1, 90000)]
        public int MaxLoss { get => iMaxLoss; set { iMaxLoss = value; RecalculateValues(); } }

        [Display(GroupName = "General", Name = "Maximum Profit", Description = "Maximum profit before the bot shuts off")]
        [Range(1, 90000)]
        public int MaxProfit { get => iMaxProfit; set { iMaxProfit = value; RecalculateValues(); } }

        private readonly SMA _LindaShort = new SMA() { Period = 3 };
        private readonly SMA _LindaLong = new SMA() { Period = 10 };
        private readonly SMA _LindaSignal = new SMA() { Period = 16 };

        private readonly HMA _hma = new HMA() { };
        private readonly RSI _rsi = new() { Period = 14 };
        private readonly ParabolicSAR _psar = new ParabolicSAR();
        private readonly ADX _adx = new ADX() { Period = 10 };
        private readonly EMA fastEma = new EMA() { Period = 20 };
        private readonly EMA slowEma = new EMA() { Period = 40 };
        private readonly FisherTransform _ft = new FisherTransform() { Period = 10 };
        private readonly SuperTrend _st = new SuperTrend() { Period = 10, Multiplier = 1m };
        private readonly KAMA _kama9 = new KAMA() { ShortPeriod = 2, LongPeriod = 109, EfficiencyRatioPeriod = 9 };
        private readonly T3 _t3 = new T3() { Period = 10, Multiplier = 1 };

        public MetroPlex()
        {
            EnableCustomDrawing = true;

            Add(_ft);
            Add(_psar);
            Add(_st);
            Add(_kama9);
            Add(_adx);
            Add(_hma);
        }

        protected override void OnRender(RenderContext context, DrawingLayouts layout)
        {
            var font = new RenderFont("Calibri", iFontSize);
            var fontB = new RenderFont("Calibri", iFontSize, FontStyle.Bold);
            int upY = 50;
            int upX = 50;
            var txt = "Howdy";
            var tsize = context.MeasureString(txt, fontB);

            txt = $"MetroPlex version " + sVersion;
            context.DrawString(txt, fontB, Color.Gold, upX, upY);
            upY += tsize.Height + 6;
            TimeSpan t = TimeSpan.FromMilliseconds(clock.ElapsedMilliseconds);
            String an = String.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            txt = $"ACTIVE on {TradingManager.Portfolio.AccountID} since " + dtStart.ToString() + " (" + an + ")";
            context.DrawString(txt, fontB, Color.Lime, upX, upY);
            if (!clock.IsRunning)
                clock.Start();

            tsize = context.MeasureString(txt, fontB);
            upY += tsize.Height + 6;

            if (TradingManager.Portfolio != null && TradingManager.Position != null)
            {
                txt = $"{TradingManager.MyTrades.Count()} trades, with PNL: {TradingManager.Position.RealizedPnL}";
                context.DrawString(txt, font, Color.White, upX, upY);
                upY += tsize.Height + 6;
                txt = sLastTrade;
                context.DrawString(txt, font, Color.White, upX, upY);
            }

        }

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar < (CurrentBar - 5))
                return;

            var pbar = bar - 1;
            var prevBar = _prevBar;
            _prevBar = bar;

            if (prevBar == bar)
                return;

            var candle = GetCandle(pbar);
            value = candle.Close;

            var p1C = GetCandle(pbar - 1);
            _t3.Calculate(pbar, value);
            fastEma.Calculate(pbar, value);
            slowEma.Calculate(pbar, value);
            _rsi.Calculate(pbar, value);

            var kama9 = ((ValueDataSeries)_kama9.DataSeries[0])[pbar];
            var t3 = ((ValueDataSeries)_t3.DataSeries[0])[pbar];
            var fast = ((ValueDataSeries)fastEma.DataSeries[0])[pbar];
            var fastM = ((ValueDataSeries)fastEma.DataSeries[0])[pbar - 1];
            var slow = ((ValueDataSeries)slowEma.DataSeries[0])[pbar];
            var slowM = ((ValueDataSeries)slowEma.DataSeries[0])[pbar - 1];
            var f1 = ((ValueDataSeries)_ft.DataSeries[0])[pbar];
            var f2 = ((ValueDataSeries)_ft.DataSeries[1])[pbar];
            var st = ((ValueDataSeries)_st.DataSeries[0])[pbar];
            var x = ((ValueDataSeries)_adx.DataSeries[0])[pbar];
            var psar = ((ValueDataSeries)_psar.DataSeries[0])[pbar];
            var rsi = ((ValueDataSeries)_rsi.DataSeries[0])[pbar];
            var rsi1 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 1];
            var rsi2 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 2];
            var hma = ((ValueDataSeries)_hma.DataSeries[0])[pbar];
            var phma = ((ValueDataSeries)_hma.DataSeries[0])[pbar - 1];

            var t1 = ((fast - slow) - (fastM - slowM)) * 150; // iWaddaSensitivity;

            var hullUp = hma > phma;
            var hullDown = hma < phma;
            var fisherUp = (f1 < f2);
            var fisherDown = (f2 < f1);
            var psarBuy = (psar < candle.Close);
            var psarSell = (psar > candle.Close);

            var lmacd = _LindaShort.Calculate(pbar, value) - _LindaLong.Calculate(pbar, value);
            var signal = _LindaSignal.Calculate(pbar, lmacd);
            var Linda = lmacd - signal;

            if (fisherUp && st > 0 && t1 > 0)
                OpenPosition("Standard Buy Signal", candle, bar, 1);

            if (fisherDown && st < 0 && t1 < 0)
                OpenPosition("Standard Sell Signal", candle, bar, -1);

        }

        private void OpenPosition(String sReason, IndicatorCandle c, int bar, int iDirection = -1)
        {
            String sD = String.Empty;

            if (iDirection == 1)
            {
                sLastTrade = "Bar " + bar + " - " + sReason + " LONG at " + c.Close;
                sD = sReason + " LONG (" + bar + ")";
            }
            else
            {
                sLastTrade = "Bar " + bar + " - " + sReason + " SHORT at " + c.Close;
                sD = sReason + " SHORT (" + bar + ")";
            }

            if (iPrevOrderBar == bar)
                return;
            else
                iPrevOrderBar = bar;

            OrderDirections d = OrderDirections.Buy;
            if (c.Open > c.Close || iDirection == -1)
                d = OrderDirections.Sell;
            if (c.Open < c.Close || iDirection == 1)
                d = OrderDirections.Buy;

            Order _order = new Order
            {
                Portfolio = Portfolio,
                Security = Security,
                Direction = d,
                Type = OrderTypes.Market,
                QuantityToFill = 1, // GetOrderVolume(),
                Comment = sD
            };
            OpenOrder(_order);
        }

    }
}
