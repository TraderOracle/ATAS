namespace ATAS.Indicators.Technical
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Media;
    using OFT.Attributes.Editors;
    using static ATAS.Indicators.Technical.SampleProperties;

    [DisplayName("Linda MACD")]
    public class LindaMACD : Indicator
    {
        private int imaType = 1;
        private class maType : Collection<Entity>
        {
            public maType()
                : base(new[]
                {
                    new Entity { Value = 1, Name = "SMA" },
                    new Entity { Value = 2, Name = "EMA" },
                    new Entity { Value = 3, Name = "WMA" },
                    new Entity { Value = 4, Name = "HMA" },
                })
            { }
        }
        public int Short { get; set; } = 3;
        public int Long { get; set; } = 10;
        public int Signal { get; set; } = 16;

        private readonly EMA _Eshort = new() { Period = 3 };
        private readonly EMA _Elong = new() { Period = 10 };
        private readonly EMA _Esignal = new() { Period = 16 };

        private readonly SMA _Sshort = new SMA() { Period = 3 };
        private readonly SMA _Slong = new SMA() { Period = 10 };
        private readonly SMA _Ssignal = new SMA() { Period = 16 };

        private readonly WMA _Wshort = new WMA() { Period = 3 };
        private readonly WMA _Wlong = new WMA() { Period = 10 };
        private readonly WMA _Wsignal = new WMA() { Period = 16 };

        private readonly HMA _Hshort = new HMA() { Period = 3 };
        private readonly HMA _Hlong = new HMA() { Period = 10 };
        private readonly HMA _Hsignal = new HMA() { Period = 16 };

        [ComboBoxEditor(typeof(maType), DisplayMember = nameof(Entity.Name), ValueMember = nameof(Entity.Value))]
        public int EMA_Type { get => imaType; set { if (value < 0) return; imaType = value; RecalculateValues(); } }

        private readonly ValueDataSeries _neg = new("Down Bar")
        {
            Color = Colors.Red,
            VisualType = VisualMode.Histogram
        };

        private readonly ValueDataSeries _pos = new("Up Bar")
        {
            Color = Colors.Green,
            VisualType = VisualMode.Histogram
        };

        private readonly ValueDataSeries _macd = new("MACD Line")
        {
            Color = Colors.Orange,
            VisualType = VisualMode.Line,
            Width = 4
        };

        private readonly ValueDataSeries _signal = new("Signal Line")
        {
            Color = Colors.Lime,
            VisualType = VisualMode.Line,
            Width = 4            
        };

        public LindaMACD()
        {
            Panel = IndicatorDataProvider.NewPanel;

            DataSeries[0] = _pos;
            DataSeries.Add(_neg);
            DataSeries.Add(_macd);
            DataSeries.Add(_signal);
        }
        protected override void OnCalculate(int bar, decimal value)
        {
            decimal bars = 0;
            decimal macd = 0;
            decimal signal = 0;

            if (imaType == 1)
            {
                _Sshort.Period = Short;
                _Slong.Period = Long;
                _Ssignal.Period = Signal;
                macd = _Sshort.Calculate(bar, value) - _Slong.Calculate(bar, value);
                signal = _Ssignal.Calculate(bar, macd);
                bars = macd - signal;
            }
            if (imaType == 2)
            {
                _Eshort.Period = Short;
                _Elong.Period = Long;
                _Esignal.Period = Signal;
                macd = _Eshort.Calculate(bar, value) - _Elong.Calculate(bar, value);
                signal = _Esignal.Calculate(bar, macd);
                bars = macd - signal;
            }
            if (imaType == 3)
            {
                _Wshort.Period = Short;
                _Wlong.Period = Long;
                _Wsignal.Period = Signal;
                macd = _Wshort.Calculate(bar, value) - _Wlong.Calculate(bar, value);
                signal = _Wsignal.Calculate(bar, macd);
                bars = macd - signal;
            }
            if (imaType == 4)
            {
                _Hshort.Period = Short;
                _Hlong.Period = Long;
                _Hsignal.Period = Signal;
                macd = _Hshort.Calculate(bar, value) - _Hlong.Calculate(bar, value);
                signal = _Hsignal.Calculate(bar, macd);
                bars = macd - signal;
            }

            _macd[bar] = macd;
            _signal[bar] = signal;

            if (bars >= 0)
                _pos[bar] = bars;
            else
                _neg[bar] = bars;
        }

    }
}