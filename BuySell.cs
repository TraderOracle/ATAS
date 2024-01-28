namespace ATAS.Indicators.Technical
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using System.Resources;
    using System.Runtime.Intrinsics.X86;
    using System.Security.Principal;
    using System.Windows.Media;
    using ATAS.Indicators;
    using ATAS.Indicators.Technical.Properties;
    using OFT.Attributes;
    using OFT.Rendering.Context;
    using OFT.Rendering.Settings;
    using OFT.Rendering.Tools;

    [DisplayName("TraderOracle Buy/Sell")]
    public class BuySell : Indicator
    {
        private bool bUseFisher = true;
        private bool bUseT3 = true;
        private bool bUseSuperTrend = true;
        private bool bUseSTC = false;
        private bool bUseSqueeze = false;
        private bool bUseMACD = false;
        private bool bUseWaddah = true;
        private bool bUseHMA = true;
        private bool bUseKAMA = true;
        private bool bUseMyEMA = false;
        private bool bUsePSAR = false;

        private bool bShow921 = false;
        private bool bShowSqueeze = false;

        private bool bShowUp = true;
        private bool bShowDown = true;

        private int iMinDelta = 40;
        private int iMinDeltaPercent = 0;
        private int iMinADX = 0;
        private int iMyEMAPeriod = 21;
        private int iKAMAPeriod = 9;

        private readonly int Sensitivity = 150;

        // ========================================================================
        // ==========================    INDICATORS    ============================
        // ========================================================================

        private readonly ParabolicSAR _psar = new ParabolicSAR();
        private readonly ADX _adx = new ADX() { Period = 10 };
        private readonly EMA _myEMA = new EMA() { Period = 21 };
        private readonly EMA _9 = new EMA() { Period = 9 };
        private readonly EMA _21 = new EMA() { Period = 21 };
        private readonly SMA fastSma = new SMA() { Period = 20 };
        private readonly SMA slowSma = new SMA() { Period = 40 };
        private readonly FisherTransform _ft = new FisherTransform() { Period = 10 };
        private readonly SuperTrend _st = new SuperTrend() { Period = 10, Multiplier = 1.7m };

        private readonly KAMA _kama = new KAMA()
        {
            ShortPeriod = 2,
            LongPeriod = 30,
            EfficiencyRatioPeriod = 9
        };
        private readonly SchaffTrendCycle _stc = new SchaffTrendCycle()
        {
            Period = 10,
            ShortPeriod = 23,
            LongPeriod = 50
        };
        private readonly MACD _macd = new MACD()
        {
            ShortPeriod = 12,
            LongPeriod = 26,
            SignalPeriod = 9
        };
        private readonly T3 _t3 = new T3()
        {
            Period = 10,
            Multiplier = 1
        };
        private readonly SqueezeMomentum _sq = new SqueezeMomentum()
        {
            BBPeriod = 20,
            BBMultFactor = 2,
            KCPeriod = 20,
            KCMultFactor = 1.5m,
            UseTrueRange = false
        };

        // ========================================================================
        // =========================    DATA SERIES    ============================
        // ========================================================================

        private readonly ValueDataSeries _squeezie = new("Squeeze Relaxer")
        {
            Color = System.Windows.Media.Colors.Yellow,
            VisualType = VisualMode.Dots,
            Width = 4
        };

        private readonly ValueDataSeries _nine21 = new("9 21 cross")
        {
            Color = System.Windows.Media.Color.FromArgb(255, 0, 255, 0),
            VisualType = VisualMode.Block,
            Width = 4
        };

        private readonly ValueDataSeries _posSeries = new("Buy Signal")
        {
            Color = System.Windows.Media.Color.FromArgb(255, 0, 255, 0),
            VisualType = VisualMode.UpArrow,
            Width = 1
        };

        private readonly ValueDataSeries _negSeries = new("Sell Signal")
        {
            Color = System.Windows.Media.Color.FromArgb(255, 255, 0, 0),
            VisualType = VisualMode.DownArrow,
            Width = 1
        };

        // ========================================================================
        // =======================    EXTRA INDICATORS    =========================
        // ========================================================================


        [Display(GroupName = "Extra Indicators", Name = "Show 9/21 EMA Cross", Order = int.MinValue)]
        public bool Show_9_21_EMA_Cross
        {
            get => bShow921;
            set { bShow921 = value; RecalculateValues(); }
        }
        [Display(GroupName = "Extra Indicators", Name = "Show Squeeze Relaxer", Order = int.MinValue)]
        public bool Show_Squeeze_Relax
        {
            get => bShowSqueeze;
            set { bShowSqueeze = value; RecalculateValues(); }
        }

        [Display(GroupName = "Buy/Sell Filters", Name = "Waddah Explosion", Order = int.MinValue, Description = "The Waddah Explosion must be the correct color, and have a value")]
        public bool Use_Waddah_Explosion
        {
            get => bUseWaddah;
            set { bUseWaddah = value; RecalculateValues(); }
        }
        /*
                [Display(GroupName = "Buy/Sell Filters", Name = "Schaff Trend Cycle", Order = int.MinValue, Description = "Standard STC 25/75 cross")]
                public bool Use_Schaff_Trend_Cycle
                {
                    get => bUseSTC;
                    set { bUseSTC = value; RecalculateValues(); }
                }
        */
        [Display(GroupName = "Buy/Sell Filters", Name = "Parabolic SAR", Order = int.MinValue, Description = "The PSAR must be signaling a buy/sell signal same as the arrow")]
        public bool Use_PSAR
        {
            get => bUsePSAR;
            set { bUsePSAR = value; RecalculateValues(); }
        }
        [Display(GroupName = "Buy/Sell Filters", Name = "Squeeze Momentum", Order = int.MinValue, Description = "The squeeze must be the correct color")]
        public bool Use_Squeeze_Momentum
        {
            get => bUseSqueeze;
            set { bUseSqueeze = value; RecalculateValues(); }
        }
        [Display(GroupName = "Buy/Sell Filters", Name = "MACD", Order = int.MinValue, Description = "Standard 12/26/9 MACD crossing in the correct direction")]
        public bool Use_MACD
        {
            get => bUseMACD;
            set { bUseMACD = value; RecalculateValues(); }
        }
        [Display(GroupName = "Buy/Sell Filters", Name = "SuperTrend", Order = int.MinValue, Description = "Price must align to the current SuperTrend trend")]
        public bool Use_SuperTrend
        {
            get => bUseSuperTrend;
            set { bUseSuperTrend = value; RecalculateValues(); }
        }
        [Display(GroupName = "Buy/Sell Filters", Name = "T3", Order = int.MinValue, Description = "Price must cross the T3")]
        public bool Use_T3
        {
            get => bUseT3;
            set { bUseT3 = value; RecalculateValues(); }
        }
        [Display(GroupName = "Buy/Sell Filters", Name = "Fisher Transform", Order = int.MinValue, Description = "Fisher Transform must cross to the correct direction")]
        public bool Use_Fisher_Transform
        {
            get => bUseFisher;
            set { bUseFisher = value; RecalculateValues(); }
        }

        [Display(GroupName = "Custom EMA", Name = "Use Custom EMA", Order = int.MinValue, Description = "Price crosses your own EMA period")]
        public bool Use_Custom_EMA
        {
            get => bUseMyEMA;
            set { bUseMyEMA = value; RecalculateValues(); }
        }
        [Display(GroupName = "Custom EMA", Name = "Custom EMA Period", Order = int.MinValue, Description = "Price crosses your own EMA period")]
        [Range(1, 1000)]
        public int Custom_EMA_Period
        {
            get => iMyEMAPeriod;
            set
            {
                if (value < 1)
                    return;
                iMyEMAPeriod = _myEMA.Period = value;
                RecalculateValues();
            }
        }

        [Display(GroupName = "Kaufman Moving Avg", Name = "Use KAMA", Order = int.MinValue, Description = "Price crosses KAMA")]
        public bool Use_KAMA
        {
            get => bUseKAMA;
            set { bUseKAMA = value; RecalculateValues(); }
        }
        [Display(GroupName = "Kaufman Moving Avg", Name = "KAMA Period", Order = int.MinValue, Description = "Price crosses KAMA")]
        [Range(1, 1000)]
        public int Custom_KAMA_Period
        {
            get => iKAMAPeriod;
            set
            {
                if (value < 1)
                    return;
                iKAMAPeriod = _kama.EfficiencyRatioPeriod = value;
                RecalculateValues();
            }
        }

        // ========================================================================
        // ========================    VALUE FILTERS    ===========================
        // ========================================================================

        [Display(GroupName = "Value Filters", Name = "Minimum Delta", Order = int.MinValue, Description = "The minimum candle delta value to show buy/sell")]
        [Range(0, 9000)]
        public int Min_Delta
        {
            get => iMinDelta;
            set
            {
                if (value < 0)
                    return;
                iMinDelta = value;
                RecalculateValues();
            }
        }

        [Display(GroupName = "Value Filters", Name = "Minimum Delta Percent", Order = int.MinValue, Description = "Minimum diff between max delta and delta to show buy/sell")]
        [Range(0, 100)]
        public int Min_Delta_Percent
        {
            get => iMinDeltaPercent;
            set
            {
                if (value < 0)
                    return;
                iMinDeltaPercent = value;
                RecalculateValues();
            }
        }

        [Display(GroupName = "Value Filters", Name = "Minimum ADX", Order = int.MinValue, Description = "Minimum ADX value before showing buy/sell")]
        [Range(0, 100)]
        public int Min_ADX
        {
            get => iMinADX;
            set
            {
                if (value < 0)
                    return;
                iMinADX = value;
                RecalculateValues();
            }
        }

        public BuySell() :
            base(true)
        {
            EnableCustomDrawing = true;

            DataSeries[0] = _posSeries;
            DataSeries.Add(_negSeries);
            DataSeries.Add(_nine21);
            DataSeries.Add(_squeezie);

            Add(_ft);
            Add(_sq);
            Add(_psar);
            Add(_st);
            Add(_adx);
            Add(_kama);
        }

        protected override void OnRender(RenderContext context, DrawingLayouts layout)
        {
            //context.DrawRectangle(RenderPens.Blue, new Rectangle(5, 10, 100, 200));
            //context.DrawLine(RenderPens.AliceBlue, 10, 20, 50, 60);
            context.DrawString("Sample string", new RenderFont("Arial", 15), System.Drawing.Color.Black, 50, 60);
            //context.DrawEllipse(new RenderPen(System.Drawing.Color.Bisque), new Rectangle(10, 10, 100, 100));

            var candle = GetCandle(MouseLocationInfo.BarBelowMouse);

            if (candle != null)
            {
                var font = new RenderFont("Arial", 10);
                var text = $"Volume={candle.Volume} Delta={candle.Delta} Max={candle.MaxDelta} Min={candle.MinDelta}";
                context.DrawString(text, font, System.Drawing.Color.AliceBlue, MouseLocationInfo.LastPosition.X + 10, MouseLocationInfo.LastPosition.Y + 10);
            }
        }

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar < 5)
                return;

            bShowDown = true;
            bShowUp = true;

            var candle = GetCandle(bar);

            var p1Candle = GetCandle(bar - 1);
            var p2Candle = GetCandle(bar - 2);
            var p3Candle = GetCandle(bar - 3);
            var p4Candle = GetCandle(bar - 4);

            value = candle.Close;

            _myEMA.Calculate(bar, value);
            _t3.Calculate(bar, value);
            fastSma.Calculate(bar, value);
            slowSma.Calculate(bar, value);
            _9.Calculate(bar, value);
            _21.Calculate(bar, value);
            _stc.Calculate(bar, value);
            _macd.Calculate(bar, value);
            //_k9.Calculate(bar, value);

            var deltaPer = candle.Delta > 0 ? (candle.Delta / candle.MaxDelta) * 100 : (candle.Delta / candle.MinDelta) *100;

            // ========================================================================
            // ========================    SERIES FETCH    ============================
            // ========================================================================

            var kama = ((ValueDataSeries)_kama.DataSeries[0])[bar];
            var m1 = ((ValueDataSeries)_macd.DataSeries[0])[bar];
            var m2 = ((ValueDataSeries)_macd.DataSeries[1])[bar];
            var m3 = ((ValueDataSeries)_macd.DataSeries[2])[bar];
            var t3 = ((ValueDataSeries)_t3.DataSeries[0])[bar];
            var fast = ((ValueDataSeries)fastSma.DataSeries[0])[bar];
            var fastM = ((ValueDataSeries)fastSma.DataSeries[0])[bar - 1];
            var slow = ((ValueDataSeries)slowSma.DataSeries[0])[bar];
            var slowM = ((ValueDataSeries)slowSma.DataSeries[0])[bar - 1];
            var sq1 = ((ValueDataSeries)_sq.DataSeries[0])[bar];
            var sq2 = ((ValueDataSeries)_sq.DataSeries[1])[bar];
            var psq1 = ((ValueDataSeries)_sq.DataSeries[0])[bar - 1];
            var psq2 = ((ValueDataSeries)_sq.DataSeries[1])[bar - 1];
            var ppsq1 = ((ValueDataSeries)_sq.DataSeries[0])[bar - 2];
            var ppsq2 = ((ValueDataSeries)_sq.DataSeries[1])[bar - 2];
            var f1 = ((ValueDataSeries)_ft.DataSeries[0])[bar];
            var f2 = ((ValueDataSeries)_ft.DataSeries[1])[bar];
            var st1 = ((ValueDataSeries)_st.DataSeries[0])[bar];
            var st2 = ((ValueDataSeries)_st.DataSeries[1])[bar];
            var x = ((ValueDataSeries)_adx.DataSeries[0])[bar];
            var nn = ((ValueDataSeries)_9.DataSeries[0])[bar];
            var prev_nn = ((ValueDataSeries)_9.DataSeries[0])[bar - 1];
            var twone = ((ValueDataSeries)_21.DataSeries[0])[bar];
            var prev_twone = ((ValueDataSeries)_21.DataSeries[0])[bar - 1];
            var stc1 = ((ValueDataSeries)_stc.DataSeries[0])[bar];
            var myema = ((ValueDataSeries)_myEMA.DataSeries[0])[bar];
            var psar = ((ValueDataSeries)_psar.DataSeries[0])[bar];

            var t1 = ((fast - slow) - (fastM - slowM)) * Sensitivity;
             
            var fisherUp = (f1 < f2);
            var fisherDown = (f2 < f1);
            var macdUp = (m1 > m2);
            var macdDown = (m1 < m2);
            var red = candle.Close < candle.Open;
            var green = candle.Close > candle.Open;
            var psarBuy = (psar < candle.Close);
            var psarSell = (psar > candle.Close);

            // ========================================================================
            // ====================    SHOW/OTHER CONDITIONS    =======================
            // ========================================================================

            if (deltaPer < iMinDeltaPercent)
            {
                bShowUp = false;
                bShowDown = false;
            }

            // Squeeze momentum relaxer show
            if (sq1 > 0 && sq1 < psq1 && psq1 > ppsq1 && bShowSqueeze)
                _squeezie[bar] = candle.High + InstrumentInfo.TickSize * 4;
            if (sq1 < 0 && sq1 > psq1 && psq1 < ppsq1 && bShowSqueeze)
                _squeezie[bar] = candle.Low - InstrumentInfo.TickSize * 4;
             
            // 9/21 cross show
            if (nn > twone && prev_nn <= prev_twone && bShow921)
                _nine21[bar] = candle.Low - InstrumentInfo.TickSize * 2;
            if (nn < twone && prev_nn >= prev_twone && bShow921)
                _nine21[bar] = candle.High + InstrumentInfo.TickSize * 2;


            // ========================================================================
            // ========================    UP CONDITIONS    ===========================
            // ========================================================================

            if (candle.Delta < iMinDelta)
                bShowUp = false;

            if (!macdUp && bUseMACD)
                bShowUp = false;

            if (psarSell && bUsePSAR)
                bShowUp = false;

            if (!fisherUp && bUseFisher)
                bShowUp = false;

            if (candle.Close < t3 && bUseT3)
                bShowUp = false;

            if (candle.Close < kama && bUseKAMA)
                bShowUp = false;

            if (candle.Close < myema && bUseMyEMA)
                bShowUp = false;

            if (t1 < 0 && bUseWaddah)
                bShowUp = false;

            if (st1 <= 0 && bUseSuperTrend)
                bShowUp = false;

            if (sq1 < 0 && bUseSqueeze)
                bShowUp = false;

            if (x < iMinADX)
                bShowUp = false;

            if (green && bShowUp)
                _posSeries[bar] = candle.Low - InstrumentInfo.TickSize * 2;

            // ========================================================================
            // ========================    DOWN CONDITIONS    =========================
            // ========================================================================

            if (candle.Delta > (iMinDelta * -1))
                bShowDown = false;

            if (psarBuy && bUsePSAR)
                bShowDown = false;

            if (!macdDown && bUseMACD)
                bShowDown = false;

            if (!fisherDown && bUseFisher)
                bShowDown = false;

            if (candle.Close > kama && bUseKAMA)
                bShowDown = false;

            if (candle.Close > t3 && bUseT3)
                bShowDown = false;

            if (candle.Close > myema && bUseMyEMA)
                bShowDown = false;

            if (t1 >= 0 && bUseWaddah)
                bShowDown = false;

            if (st2 <= 0 && bUseSuperTrend)
                bShowDown = false;

            if (sq1 > 0 && bUseSqueeze)
                bShowDown = false;

            if (x < iMinADX)
                bShowDown = false;

            if (red && bShowDown)
                _negSeries[bar] = candle.High + InstrumentInfo.TickSize * 2;

        }

    }
}