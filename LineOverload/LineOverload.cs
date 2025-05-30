﻿
namespace ATAS.Indicators.Technical
{
  #region INCLUDES
  using System;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using ATAS.Indicators;
  using ATAS.Indicators.Drawing;
  using OFT.Rendering.Context;
  using OFT.Rendering.Tools;
  #endregion

  #region VARIABLES
  using Color = System.Drawing.Color;

  [DisplayName("LineOverload")]
  public class LineOverload : Indicator
  {
    private const string sVersion = "1.2";
    private string sKPValues = "";
    private string sMQValues = "";
    private string sBlindSpots = "";
    private string sDarkPool = "";
    private string sCTLevels = "";
    private string sManciniSupport = "";
    private string sManciniResist = "";

    private struct days
    {
      public int idx;
      public string label;
      public string ticker;
      public decimal price;
      public decimal price2;
      public Color c;
      public string source;
    }
    private List<days> lsS = new List<days>();

    private int iFontSize = 11;
    private int iLineWidth = 2;

    private bool bShowText = true;
    private int _lastBar = -1;
    private int currBar = 0;
    private int prevBar = 0;
    private bool _lastBarCounted;
    private bool bFirstDisplay = false;
    private string filePath = @"c:\temp\motivelines.csv";

    Color cBL = Color.DeepPink;
    Color cmin = Color.Red;
    Color cmax = Color.Red;
    Color ckvo2 = Color.Cyan;
    Color crange = Color.Lime;
    Color ckvo1 = Color.GreenYellow;
    Color cDP = Color.PeachPuff;
    Color cGex = Color.Green;
    Color cResist = Color.OrangeRed;
    Color cWall = Color.White;
    Color cManSupp = Color.OrangeRed;
    Color cManRest = Color.Lime;
    Color cDefault = Color.White;

    public LineOverload() :
        base(true)
    {
      EnableCustomDrawing = true;
      DenyToChangePanel = true;
      SubscribeToDrawingEvents(DrawingLayouts.Historical);
    }
    #endregion

    #region SETTINGS

    [Display(GroupName = "Values", Name = "Killpips Values")]
    public string KPValues { get => sKPValues; set { sKPValues = value; RecalculateValues(); } }

    [Display(GroupName = "Values", Name = "MenthorQ Values")]
    public string MQValues { get => sMQValues; set { sMQValues = value; RecalculateValues(); } }
    [Display(GroupName = "Values", Name = "Blind Spots")]
    public string BlindSpots { get => sBlindSpots; set { sBlindSpots = value; RecalculateValues(); } }
    [Display(GroupName = "Values", Name = "Dark Pool")]
    public string DarkPool { get => sDarkPool; set { sDarkPool = value; RecalculateValues(); } }
    [Display(GroupName = "Values", Name = "CT Levels")]
    public string CTLevels { get => sCTLevels; set { sCTLevels = value; RecalculateValues(); } }

    [Display(GroupName = "Values", Name = "Mancini Support")]
    public string ManciniSupport { get => sManciniSupport; set { sManciniSupport = value; RecalculateValues(); } }
    [Display(GroupName = "Values", Name = "Mancini Resist")]
    public string ManciniResist { get => sManciniResist; set { sManciniResist = value; RecalculateValues(); } }

    [Display(GroupName = "Options", Name = "Show Text")]
    public bool ShowText { get => bShowText; set { bShowText = value; RecalculateValues(); } }

    [Display(GroupName = "Options", Name = "Text Font Size")]
    public int FontSize { get => iFontSize; set { iFontSize = value; RecalculateValues(); } }
    [Display(GroupName = "Options", Name = "Text Font Size")]
    public int LineWidth { get => iLineWidth; set { iLineWidth = value; RecalculateValues(); } }

    [Display(GroupName = "Colors", Name = "Gex/VIX Color")]
    public Color Gex { get => cGex; set { cGex = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "Resistance Color")]
    public Color Resist { get => cResist; set { cResist = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "Wall Color")]
    public Color call { get => cWall; set { cWall = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "crange Color")]
    public Color range { get => crange; set { crange = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "BL/RD Color")]
    public Color cBLa { get => cBL; set { cBL = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "Min/Support Color")]
    public Color Min { get => cmin; set { cmin = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "Max Color")]
    public Color Max { get => cmax; set { cmax = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "kvo2 Color")]
    public Color kvo2 { get => ckvo2; set { ckvo2 = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "DarkPool Color")]
    public Color DP { get => cDP; set { cDP = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "ckvo1 Color")]
    public Color kvo1 { get => ckvo1; set { ckvo1 = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "Default Color")]
    public Color Default { get => cDefault; set { cDefault = value; RecalculateValues(); } }

    [Display(GroupName = "Colors", Name = "Mancini Support Color")]
    public Color ManSupp { get => cManSupp; set { cManSupp = value; RecalculateValues(); } }
    [Display(GroupName = "Colors", Name = "Mancini Resist Color")]
    public Color ManRest { get => cManRest; set { cManRest = value; RecalculateValues(); } }

    #endregion

    #region RENDER
    protected override void OnRender(RenderContext context, DrawingLayouts layout)
    {
      if (ChartInfo is null || InstrumentInfo is null)
        return;

      foreach (var l in lsS)
      {
        var xH = ChartInfo.PriceChartContainer.GetXByBar(CurrentBar, false);
        var yH = ChartInfo.PriceChartContainer.GetYByPrice(l.price, false);
        var yH2 = ChartInfo.PriceChartContainer.GetYByPrice(l.price2, false);
        var yWidth = ChartInfo.ChartContainer.Region.Width;
        RenderPen KPPen = new RenderPen(l.c, 2, DashStyle.Dash);
        RenderPen MQPen = new RenderPen(l.c, 1, DashStyle.Solid);
        RenderPen BSPen = new RenderPen(l.c, 3, DashStyle.DashDot);
        RenderPen Pen = new RenderPen(l.c, 1, DashStyle.DashDotDot);
        var rectPen = new Pen(new SolidBrush(l.c)) { Width = 2 };

        if (l.price2 > 0)
        {
          DrawingRectangle dr = new DrawingRectangle(1, l.price, CurrentBar, l.price2, rectPen, new SolidBrush(l.c));
          if (!Rectangles.Contains(dr))
            Rectangles.Add(dr);
        }
        else {
          if (l.source == "KP")
            context.DrawLine(KPPen, 0, yH, xH, yH);
          else if (l.source == "MQ")
            context.DrawLine(MQPen, 0, yH, xH, yH);
          else if (l.source == "BS")
            context.DrawLine(BSPen, 0, yH, xH, yH);
          else
            context.DrawLine(Pen, 0, yH, xH, yH);
        }
        if (bShowText)
          context.DrawString(l.label, new RenderFont("Arial", iFontSize), l.c, xH, yH);
      }
    }
    #endregion

    #region LOAD MANCINI

    private void LoadMancini(String sLevels, Color cl)
    {
      try
      {
        int i = 0;
        string[] sb = sLevels.Split(", ");
        string price = string.Empty;
        foreach (string s in sb)
        {
          String sa = s.Replace("(major)", "").Trim();
          days a = new days();
          a.idx = i;
          if (s.Contains("-"))
          {
            a.price = Convert.ToDecimal(sa.Split("-")[0].Trim());
            if (sa.Split("-")[1].Trim().Length == 2)
              a.price2 = Convert.ToDecimal(sa.Substring(0, 2) + sa.Split("-")[1].Trim());
            else
              a.price2 = Convert.ToDecimal(sa.Split("-")[1].Trim());
          }
          else
          {
            a.price = Convert.ToDecimal(sa.Substring(0, 4).Trim());
          }
          a.label = s.Contains("major") ? "Mancini MAJOR" : "Mancini";
          a.c = cl;
          lsS.Add(a);
        }
        i++;
      }
      catch (Exception ex)
      {
      }
    }

    #endregion

    #region LOAD EVERYTHING ELSE

    private void LoadCSVLines()
    {
      string sDesc = string.Empty;
      string sPrice = string.Empty;

      try
      {
        string fileContent = File.ReadAllText(filePath);
        string[] lines = fileContent.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
          string[] parts = line.Split(',');
          if (parts.Length >= 3)
          {
            string symbol = parts[0].Trim();
            string desc = parts[1].Trim();
            if (double.TryParse(parts[2].Trim(), out double price))
            {
              days a = new days();
              a.price = Convert.ToDecimal(price);
              a.label = desc;
              lsS.Add(a);
            }
          }
        }
      }
      catch { }
    }

    private void LoadFromService(String sLevels, string source)
    {
      string sDesc = string.Empty;
      string sPrice = string.Empty;

      try
      {
        int i = 0;
        sLevels = sLevels.Replace(" ", "").Replace(";", ",");
        sLevels = sLevels.Replace(" ", "").Replace(";", ",");
        string[] sb = sLevels.Split(",");
        string price = string.Empty, desc = string.Empty;
        foreach (string sr in sb)
        {
          if (i % 2 != 0)
            price = sr;
          else
            desc = sr;

          if (!string.IsNullOrEmpty(price) && !string.IsNullOrEmpty(desc))
          {
            Color cl = Color.Lime;
            if (desc.Contains("range") || desc.Contains("HVL") || desc.Contains("HV") || desc.Contains("VAL"))
              cl = crange;
            if (desc.ToLower().Contains("min") || desc.Contains("Support") || 
              desc.ToLower().Contains("l1") || desc.ToLower().Contains("l2") || desc.ToLower().Contains("l3") || desc.ToLower().Contains("l4") || desc.ToLower().Contains("l5"))
              cl = cmin;
            else if (desc.Contains("kvo1"))
              cl = ckvo1;
            else if (desc.Contains("BL") || desc.Contains("RD") || desc.Contains("SD"))
              cl = cBL;
            else if (desc.Contains("kvo2"))
              cl = ckvo2;
            else if (desc.ToLower().Contains("max") || desc.Contains("Extreme") ||
              desc.ToLower().Contains("h4") || desc.ToLower().Contains("h5") || desc.ToLower().Contains("h3") || desc.ToLower().Contains("h2"))
              cl = cmax;
            else if (desc.Contains("GEX") || desc.Contains("VIX") ||
              desc.ToLower().Contains("mid"))
              cl = cGex;
            else if (desc.Contains("Resist"))
              cl = cResist;
            else if (desc.Contains("Wall"))
              cl = cWall;
            days a = new days();
            a.idx = i;
            a.price = Convert.ToDecimal(price);
            a.label = desc;
            a.c = cl;
            a.source = source;
            lsS.Add(a);
            price = string.Empty;
            desc = string.Empty;
          }
          i++;
        }
      }
      catch (Exception ex)
      {
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

      if (!bFirstDisplay)
      {
        bFirstDisplay = true;

        LoadFromService(sKPValues, "KP");
        LoadFromService(sMQValues, "MQ");
        LoadFromService(sBlindSpots, "BS");
        LoadFromService(sDarkPool, "DP");
        LoadFromService(sCTLevels, "CT");
        LoadCSVLines();
        LoadMancini(sManciniSupport, cManSupp);
        LoadMancini(sManciniResist, cManRest);
      }

      if (_lastBar != bar)
      {
        if (_lastBarCounted)
        {
        }
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