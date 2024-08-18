
namespace ATAS.Indicators.Technical
{
    #region INCLUDES
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using ATAS.Indicators;
    using ATAS.Indicators.Drawing;
    using DomV10;
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

        private int iFontSize = 7;
        private List<shit> lsS = new List<shit>();
        private bool bShowMain = true;
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

        private bool bShowGEX1 = true;
        private bool bShowGEX2 = true;
        private bool bShowGEX3 = true;
        private bool bShowGEX4 = true;
        private bool bShowGEX5 = true;
        private bool bShowGEX6 = true;
        private bool bShowGEX7 = true;
        private bool bShowGEX8 = true;
        private bool bShowGEX9 = true;
        private bool bShowGEX10 = true;
        private bool bShowMax = true;
        private bool bShowMin = true;
        private bool bShowHVL = true;
        private bool bShowWall = true;
        private bool bShowResist = true;
        private bool bShowSupport = true;

        Color cBL = Color.DarkCyan;
        Color cMin = Color.Red;
        Color cMax = Color.Red;
        Color cHVL = Color.Cyan;
        Color cSupport = Color.Lime;
        Color cResist = Color.Red;
        Color cWall = Color.GreenYellow;
        Color cGEX = Color.CadetBlue;

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

        [Display(GroupName = "Options", Name = "Text Font Size")]
        public int FontSize { get => iFontSize; set { iFontSize = value; RecalculateValues(); } }


        //[Display(GroupName = "Options", Name = "Alert on Touch Line")]
        //public bool UseAlerts { get => bUseAlerts; set { bUseAlerts = value; RecalculateValues(); } }

        //[Display(GroupName = "Options", Name = "Alert on Wick Through Line")]
        //public bool AlertWick { get => bAlertWick; set { bAlertWick = value; RecalculateValues(); } }

        [Display(GroupName = "Colors", Name = "GEX Color")]
        public Color GEX { get => cGEX; set { cGEX = value; RecalculateValues(); } }
        [Display(GroupName = "Colors", Name = "BlindSpot Color")]
        public Color cBLa { get => cBL; set { cBL = value; RecalculateValues(); } }
        [Display(GroupName = "Colors", Name = "Min Color")]
        public Color Min { get => cMin; set { cMin = value; RecalculateValues(); } }
        [Display(GroupName = "Colors", Name = "Max Color")]
        public Color Max { get => cMax; set { cMax = value; RecalculateValues(); } }
        [Display(GroupName = "Colors", Name = "HVL Color")]
        public Color HVL { get => cHVL; set { cHVL = value; RecalculateValues(); } }
        [Display(GroupName = "Colors", Name = "Support Color")]
        public Color Support { get => cSupport; set { cSupport = value; RecalculateValues(); } }
        [Display(GroupName = "Colors", Name = "Resistance Color")]
        public Color Resist { get => cResist; set { cResist = value; RecalculateValues(); } }
        [Display(GroupName = "Colors", Name = "Wall Color")]
        public Color Wall { get => cWall; set { cWall = value; RecalculateValues(); } }

        [Display(GroupName = "Show / Hide", Name = "Show GEX 1")]
        public bool ShowGEX1 { get => bShowGEX1; set { bShowGEX1 = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show GEX 2")]
        public bool ShowGEX2 { get => bShowGEX2; set { bShowGEX2 = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show GEX 3")]
        public bool ShowGEX3 { get => bShowGEX3; set { bShowGEX3 = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show GEX 4")]
        public bool ShowGEX4 { get => bShowGEX4; set { bShowGEX4 = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show GEX 5")]
        public bool ShowGEX5 { get => bShowGEX5; set { bShowGEX5 = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show GEX 6")]
        public bool ShowGEX6 { get => bShowGEX6; set { bShowGEX6 = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show GEX 7")]
        public bool ShowGEX7 { get => bShowGEX7; set { bShowGEX7 = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show GEX 8")]
        public bool ShowGEX8 { get => bShowGEX8; set { bShowGEX8 = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show GEX 9")]
        public bool ShowGEX9 { get => bShowGEX9; set { bShowGEX9 = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show GEX 10")]
        public bool ShowGEX10 { get => bShowGEX10; set { bShowGEX10 = value; RecalculateValues(); } }

        [Display(GroupName = "Show / Hide", Name = "Show Min")]
        public bool ShowMin { get => bShowMin; set { bShowMin = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show Max")]
        public bool ShowMax { get => bShowMax; set { bShowMax = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show Wall")]
        public bool ShowWall { get => bShowWall; set { bShowWall = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show Resistance")]
        public bool ShowResist { get => bShowResist; set { bShowResist = value; RecalculateValues(); } }
        [Display(GroupName = "Show / Hide", Name = "Show Support")]
        public bool ShowSupport { get => bShowSupport; set { bShowSupport = value; RecalculateValues(); } }

        #endregion

    #region RENDER
        protected override void OnRender(RenderContext context, DrawingLayouts layout)
        {
            if (ChartInfo is null || InstrumentInfo is null)
                return;

            Color color = Color.Gray;
            if (bShowText)
                foreach (var l in lsS)
                    if (l.ticker.Contains(InstrumentInfo.Instrument))
                    {
                        var highPen = new RenderPen(color) { Width = 2 };
                        var xH = ChartInfo.PriceChartContainer.GetXByBar(CurrentBar, false);
                        var yH = ChartInfo.PriceChartContainer.GetYByPrice(l.price, false);
                        context.DrawString(l.label, new RenderFont("Arial", iFontSize), l.c, xH, yH);
                    }
        }
    #endregion

    #region PROCESS DATA

        protected bool checkShow(string s)
        {
            if (s.Contains("GEX 10") && bShowGEX10) return true;
            else if (s.Equals("GEX 1") && bShowGEX1) return true;
            else if (s.Contains("GEX 2") && bShowGEX2) return true;
            else if (s.Contains("GEX 3") && bShowGEX3) return true;
            else if (s.Contains("GEX 4") && bShowGEX4) return true;
            else if (s.Contains("GEX 5") && bShowGEX5) return true;
            else if (s.Contains("GEX 6") && bShowGEX6) return true;
            else if (s.Contains("GEX 7") && bShowGEX7) return true;
            else if (s.Contains("GEX 8") && bShowGEX8) return true;
            else if (s.Contains("GEX 9") && bShowGEX9) return true;
            else if (s.Contains("HVL") && bShowHVL) return true;
            else if (s.Contains("Wall") && bShowWall) return true;
            else if (s.Contains("Min") && bShowMin) return true;
            else if (s.Contains("Max") && bShowMax) return true;
            else if (s.Contains("Support") && bShowSupport) return true;
            else if (s.Contains("Resist") && bShowResist) return true;
            else if (s.Contains("BL ") && bShowResist) return true;
            return false;
        }

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
                        if (!checkShow(desc))
                            continue;
                        Color cl = Color.White;
                        if (desc.Contains("Support"))
                            cl = cSupport;
                        if (desc.Contains("Min"))
                            cl = cMin;
                        else if (desc.Contains("GEX"))
                            cl = cGEX;
                        else if (desc.Contains("BL "))
                            cl = cBL;
                        else if (desc.Contains("HVL"))
                            cl = cHVL;
                        else if (desc.Contains("Wall"))
                            cl = cWall;
                        else if (desc.Contains("Resist"))
                            cl = cResist;
                        else if (desc.Contains("Max"))
                            cl = cMax;
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

        protected void DrawShit()
        {
            foreach (var l in lsS)
                if (l.ticker.Contains(InstrumentInfo.Instrument))
                {
                    var highPen = new Pen(new SolidBrush(l.c)) { Width = 1 };
                    DrawingRectangle dr = new DrawingRectangle(1, l.price, CurrentBar, l.price, highPen, new SolidBrush(l.c));
                    if (!Rectangles.Contains(dr))
                        Rectangles.Add(dr);
                }
        }

        private void LoadFromCSV(string sFile)
        {
            foreach (string s in File.ReadAllLines(sFile))
            {
                string sb = s.Split(':')[0].Trim();
                if (sb.Split('.')[0].Trim().Equals(InstrumentInfo.Instrument))
                {
                    DoParse(s);
                    break;
                }
            }
        }
        #endregion

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
                if (File.Exists(@"C:\Program Files (x86)\ATAS Platform\SierraChart Q-Levels.txt"))
                    LoadFromCSV(@"C:\Program Files (x86)\ATAS Platform\SierraChart Q-Levels.txt");
                if (File.Exists(@"C:\Program Files (x86)\ATAS Platform\ATAS Q-Levels.txt"))
                    LoadFromCSV(@"C:\Program Files (x86)\ATAS Platform\ATAS Q-Levels.txt");

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
                if (_lastBarCounted) //  && bUseAlerts)
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