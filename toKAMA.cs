namespace ATAS.Indicators.Technical
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ATAS.Indicators.Technical.Properties;
    using static System.Reflection.Metadata.BlobBuilder;
    using Color = System.Drawing.Color;

    [DisplayName("toKAMA")]
    public class toKAMA : Indicator
    {
        #region Fields

        private readonly List<decimal> _closeList = new();

        private int _efficiencyRatioPeriod = 9;
        private int _lastBar = -1;
        private int _longPeriod = 109;
        private int _shortPeriod = 2;

        #endregion

        #region Properties

        [Display(ResourceType = typeof(Resources), Name = "Period", GroupName = "Common")]
        [Range(1, 10000)]
        public int EfficiencyRatioPeriod
        {
            get => _efficiencyRatioPeriod;
            set
            {
                _efficiencyRatioPeriod = value;
                RecalculateValues();
            }
        }

        #endregion

        #region ctor

        public toKAMA()
            : base(true)
        {
            DenyToChangePanel = true;
        }

        private Color AMD(int bar)
        {
            var candle = GetCandle(bar);
            var diff = InstrumentInfo.TimeZone;
            var time = candle.Time.AddHours(diff);

            // Distribution
            if (
                (time.Hour == 9 && time.Minute >= 11 && time.Minute <= 47) ||
                (time.Hour == 10 && time.Minute >= 26 && time.Minute <= 50) ||
                (time.Hour == 11 && time.Minute >= 19 && time.Minute <= 37) ||
                (time.Hour == 12 && time.Minute >= 07 && time.Minute <= 25)
                )
            {
                return Color.Lime;
            }

            return Color.FromArgb(80, 244, 252, 0);
        }

        #endregion

        #region Protected methods

        protected override void OnCalculate(int bar, decimal value)
        {
            var currentCandle = GetCandle(bar);
            var pastCandle = GetCandle(Math.Max(bar - EfficiencyRatioPeriod, 0));

            if (bar == 0)
            {
                _closeList.Clear();
                _closeList.Add(currentCandle.Close);
                this[bar] = currentCandle.Close;
                return;
            }

            if (_closeList.Count > EfficiencyRatioPeriod)
                _closeList.RemoveAt(0);

            var change = currentCandle.Close - pastCandle.Close;
            var volatilitySum = Math.Abs(currentCandle.Close - _closeList.LastOrDefault());

            for (var i = _closeList.Count - 1; i > 0; i--)
                volatilitySum += Math.Abs(_closeList[i] - _closeList[i - 1]);

            decimal er;

            if (volatilitySum == 0.0m)
                er = 0.000001m;
            else
                er = change / volatilitySum;

            var fastestConst = 2.0m / (_shortPeriod + 1.0m);
            var slowestConst = 2.0m / (_longPeriod + 1.0m);

            var sc = (er * (fastestConst - slowestConst)) + slowestConst;
            sc *= sc;

            this[bar] = this[bar - 1] + (sc * (currentCandle.Close - this[bar - 1]));

            if (bar != _lastBar)
                _lastBar = bar;
            else
                _closeList.RemoveAt(_closeList.Count - 1);

            _closeList.Add(currentCandle.Close);
        }

        #endregion
    }
}