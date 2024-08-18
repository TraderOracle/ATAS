
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

    #endregion

    #region VARIABLES
    using Color = System.Drawing.Color; 
    using Pen = System.Drawing.Pen;

    [DisplayName("MenthorQ")]
    public class MenthorQ : Indicator
    {
        private const string sVersion = "1.0";

        private struct shit
        {
            public string label;
            public string ticker;
            public decimal price;
            public Color c;
        }

        private List<shit> lsS = new List<shit>();
        private bool bShowMain = true;
        private bool bShowGEX = true;
        private bool bShowBlind = true;
        private bool bUseAlerts = true;
        private bool bAlertWick = false;
        private bool bShowText = true;
        private int _lastBar = -1;
        private string sReturn;
        private int currBar = 0;
        private int prevBar = 0;
        private int iIndex = 0;
        private bool _lastBarCounted;

        private string sLvl1 = string.Empty;
        private string sLvl2 = string.Empty;
        private string sLvl3 = string.Empty;
        private string sLvl4 = string.Empty;
        private string sLvl5 = string.Empty;
        private string sBS1 = string.Empty;
        private string sBS2 = string.Empty;
        private string sBS3 = string.Empty;
        private string sBS4 = string.Empty;
        private string sBS5 = string.Empty;

        Color cLong = Color.FromArgb(255, 2, 71, 1);
        Color cShort = Color.FromArgb(255, 82, 1, 1);
        Color cLine = Color.FromArgb(255, 128, 125, 125);
        Color cMTS = Color.FromArgb(255, 68, 100, 242);
        Color cTS = Color.FromArgb(255, 68, 100, 242);

        public MenthorQ() :
            base(true)
        {
            EnableCustomDrawing = true;
            DenyToChangePanel = true;
            SubscribeToDrawingEvents(DrawingLayouts.Historical);
        }
        #endregion

    #region SETTINGS
        [Display(GroupName = "Levels Input", Name = "Levels 1")]
        public string Lvl1 { get => sLvl1; set { sLvl1 = value; RecalculateValues(); } }
        [Display(GroupName = "Levels Input", Name = "Levels 2")]
        public string Lvl2 { get => sLvl2; set { sLvl2 = value; RecalculateValues(); } }
        [Display(GroupName = "Levels Input", Name = "Levels 3")]
        public string Lvl3 { get => sLvl3; set { sLvl3 = value; RecalculateValues(); } }
        [Display(GroupName = "Levels Input", Name = "Levels 4")]
        public string Lvl4 { get => sLvl4; set { sLvl4 = value; RecalculateValues(); } }
        [Display(GroupName = "Levels Input", Name = "Levels 5")]
        public string Lvl5 { get => sLvl5; set { sLvl5 = value; RecalculateValues(); } }
        [Display(GroupName = "Levels Input", Name = "Blind Spots 1")]
        public string BS1 { get => sBS1; set { sBS1 = value; RecalculateValues(); } }
        [Display(GroupName = "Levels Input", Name = "Blind Spots 2")]
        public string BS2 { get => sBS2; set { sBS2 = value; RecalculateValues(); } }
        [Display(GroupName = "Levels Input", Name = "Blind Spots 3")]
        public string BS3 { get => sBS3; set { sBS3 = value; RecalculateValues(); } }
        [Display(GroupName = "Levels Input", Name = "Blind Spots 4")]
        public string BS4 { get => sBS4; set { sBS4 = value; RecalculateValues(); } }
        [Display(GroupName = "Levels Input", Name = "Blind Spots 5")]
        public string BS5 { get => sBS5; set { sBS5 = value; RecalculateValues(); } }

        [Display(GroupName = "Options", Name = "Show Text")]
        public bool ShowText { get => bShowText; set { bShowText = value; RecalculateValues(); } }

        [Display(GroupName = "Options", Name = "Show GEX Lines")]
        public bool ShowGEX { get => bShowGEX; set { bShowGEX = value; RecalculateValues(); } }

        //[Display(GroupName = "Options", Name = "Alert on Touch Line")]
        //public bool UseAlerts { get => bUseAlerts; set { bUseAlerts = value; RecalculateValues(); } }

        //[Display(GroupName = "Options", Name = "Alert on Wick Through Line")]
        //public bool AlertWick { get => bAlertWick; set { bAlertWick = value; RecalculateValues(); } }

        //[Display(GroupName = "Colors", Name = "Long Color")]
        //public Color brLong { get => cLong; set { cLong = value; RecalculateValues(); } }
  
        //[Display(GroupName = "Colors", Name = "Short Color")]
        //public Color caShort { get => cShort; set { cShort = value; RecalculateValues(); } }

        //[Display(GroupName = "Colors", Name = "LIS Color")]
        //public Color caLine { get => cLine; set { cLine = value; RecalculateValues(); } }

        //[Display(GroupName = "Colors", Name = "GEX Line Color")]
        //public Color caMTS { get => cMTS; set { cMTS = value; RecalculateValues(); } }

        //[Display(GroupName = "Colors", Name = "Blind Spot Color")]
        //public Color caTS { get => cTS; set { cTS = value; RecalculateValues(); } }
        #endregion

    #region RENDER
        protected override void OnRender(RenderContext context, DrawingLayouts layout)
        {
            if (ChartInfo is null || InstrumentInfo is null)
                return;

            Color color = Color.Gray;
            if (bShowText)
            foreach (var l in lsS)
                if (l.ticker.Substring(0, 2) == InstrumentInfo.Instrument.Substring(0, 2))
                {
                    var highPen = new RenderPen(color) { Width = 2 };
                    var xH = ChartInfo.PriceChartContainer.GetXByBar(CurrentBar, false);
                    var yH = ChartInfo.PriceChartContainer.GetYByPrice(l.price, false);
                    context.DrawString(l.label, new RenderFont("Arial", 7), l.c, xH, yH);
                }
        }
    #endregion

    #region PROCESS DATA
        protected void DoParse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return;

            try
            {
                string sTicker = s.Split(':')[0].Replace("$","").Trim();
                string[] spts = s.Split(':')[1].Trim().Split(',');
                int i = 0;
                string price = string.Empty, desc = string.Empty;
                foreach (string sr in spts)
                { 
                    i++;
                    if (i % 2 != 0)
                        desc = sr;
                    else
                        price = sr;

                    if (!string.IsNullOrEmpty(price) && !string.IsNullOrEmpty(desc))
                    {
                        Color cl = Color.White;
                        if (desc.Contains("Support") || desc.Contains("Min"))
                            cl = Color.Lime;
                        else if (desc.Contains("GEX"))
                        {
                            cl = Color.LightSteelBlue;
                            if (!bShowGEX)
                                return;
                        }
                        else if (desc.Contains("BL "))
                            cl = Color.DarkCyan;
                        else if (desc.Contains("HVL") || desc.Contains("Wall"))
                            cl = Color.DimGray;
                        else if (desc.Contains("Resist") || desc.Contains("Max"))
                            cl = Color.Red;
                        shit a = new shit();
                        a.price = Convert.ToDecimal(price);
                        a.label = desc;
                        if (sTicker.Contains("SPX"))
                            sTicker = "SPY";
                        a.ticker = sTicker;
                        a.c = cl;
                        lsS.Add(a);
                        price = string.Empty;
                        desc = string.Empty;
                    }
                }
            }
            catch { }
        }
        #endregion

        protected void DrawShit()
        {
            foreach (var l in lsS)
                if (l.ticker.Substring(0, 2) == InstrumentInfo.Instrument.Substring(0, 2))
                {
                    var highPen = new Pen(new SolidBrush(l.c)) { Width = 1 };
                    DrawingRectangle dr = new DrawingRectangle(1, l.price, CurrentBar, l.price, highPen, new SolidBrush(l.c));
                    if (!Rectangles.Contains(dr))
                        Rectangles.Add(dr);
                }
        }

        protected override void OnCalculate(int bar, decimal value)
        {
            if (bar == 0)
            {
                Rectangles.Clear();
                _lastBarCounted = false;
                return;
            }

            var candle = GetCandle(bar);

            if (bar > CurrentBar - 3)
            {
                DoParse(sLvl1);
                DoParse(sLvl2);
                DoParse(sLvl3);
                DoParse(sLvl4);
                DoParse(sLvl5);
                DoParse(sBS1);
                DoParse(sBS2);
                DoParse(sBS3);
                DoParse(sBS4);
                DoParse(sBS5);
                DrawShit();
            }

            if (_lastBar != bar)
            {
                if (_lastBarCounted && bUseAlerts)
                    DrawShit();
                _lastBar = bar;
            }
            else
            {
                if (!_lastBarCounted)
                    _lastBarCounted = true;
            }
        }

    }
}