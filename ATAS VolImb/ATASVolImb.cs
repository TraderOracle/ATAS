
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
  using System.Net;
  using System.Drawing.Imaging;
  using ScreenCapture.NET;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.Windows.Forms;
  using System.Windows.Media;
  using System.Linq.Expressions;
  using Org.BouncyCastle.Crypto.Macs;
  using System.Media;


  #endregion

  [DisplayName("TO VolImb")]
  public class TOVolImb : Indicator
  {
    #region VARIABLES
    private const String sVersion = "1.4";

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
    private bool bFirstDisplay = false;

    private const int LINE_WICK_ENG = 16;
    private const int LINE_TOUCH_ENG = 17;
    private const int DOUBLE_VOL_IMB_DROP_R = 11;
    private const int DOUBLE_VOL_IMB_DROP_G = 10;
    private const int ENG_BB = 13;
    private const int TRAMPOLINE = 12;
    private const int VOL_IMB_DROP_BB_G = 14;
    private const int VOL_IMB_DROP_BB_R = 15;
    private const int VOL_IMB_DROP_G = 8;
    private const int VOL_IMB_DROP_R = 9;
    private const int VOL_IMB_ENGULFING = 7;
    private const int VOL_IMB_FILL = 3;
    private const int VOL_IMB_REVERSE = 4;
    private const int VOL_IMB_TOUCH_G = 5;
    private const int VOL_IMB_TOUCH_R = 6;
    private const int VOL_IMB_WICK_G = 1;
    private const int VOL_IMB_WICK_R = 2;
    private int iLastTouchBar = 0;
    private decimal iLastTouchPrice = 0;
    private int iLastWickBar = 0;
    private decimal iLastWickPrice = 0;
    private WebClient client1 = new WebClient();
    private readonly PaintbarsDataSeries _paintBars = new("Paint bars");
    List<int> LineTouches = new List<int>();
    private int iFutureSound = 0;
    private int iPrevSound = 0;
    private String sWavDir = @"C:\Program Files (x86)\ATAS Platform\Sounds";
    private int _lastBar = -1;
    private bool _lastBarCounted;
    private int iOffset = 9;
    private int iFontSize = 10;
    private int iMinBars = 3;
    private int iLineWidth = 2;
    private int iScreenShotX = 300;
    private int iScreenShotY = 400;
    private bool bScreenShotted = false;
    private string sKPValues = "BH2,6027.14,BMid,5981.25,BL2,5935.36,CH2,6047.91,CL2,5914.59,DH2,6064.95,DL2,5897.55,EH2,6080.52,EL2,5881.98,CH3,6006.46,CMid,5959.25,CL3,5910.28,DH3,6026.71,DL3,5891.70,EH3,6045.10,EL3,5874.15,DH4,5919.21,DMid,5859.25,DL4,5797.42,EH4,5944.78,EL4,5771.03,EH5,5915.46,EMid,5858.50,EL5,5801.54";

    #region SETTINGS
    [Display(GroupName = "Values", Name = "General")]
    public string KPValues { get => sKPValues; set { sKPValues = value; RecalculateValues(); } }

    [Display(Name = "Line Width", GroupName = "General", Order = int.MaxValue)]
    public int LineWidth { get => iLineWidth; set { iLineWidth = value; RecalculateValues(); } }

    [Display(Name = "Screen Shot X", GroupName = "General", Order = int.MaxValue)]
    public int ScreenShotX { get => iScreenShotX; set { iScreenShotX = value; RecalculateValues(); } }

    [Display(Name = "Screen Shot Y", GroupName = "General", Order = int.MaxValue)]
    public int ScreenShotY { get => iScreenShotY; set { iScreenShotY = value; RecalculateValues(); } }

    [Display(Name = "Font Size", GroupName = "General", Order = int.MaxValue)]
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

    private readonly ValueDataSeries _posSeries = new("Vol Imbalance Sell") { Color = MColors.White, VisualType = VisualMode.Dots, Width = 3 };
    private readonly ValueDataSeries _negSeries = new("Vol Imbalance Buy") { Color = MColors.White, VisualType = VisualMode.Dots, Width = 3 };
    private readonly ValueDataSeries _posUP = new("Vol Imbalance Up") { Color = MColors.Lime, VisualType = VisualMode.Block, Width = 3 };
    private readonly ValueDataSeries _negDN = new("Vol Imbalance Down") { Color = MColors.Red, VisualType = VisualMode.Block, Width = 3 };

    public TOVolImb() :
        base(true)
    {
      DenyToChangePanel = true;
      SubscribeToDrawingEvents(DrawingLayouts.Historical);
      EnableCustomDrawing = true;

      DataSeries[0] = _posSeries;
      DataSeries.Add(_negSeries);
      DataSeries.Add(_paintBars);
      DataSeries.Add(_posUP);
      DataSeries.Add(_negDN);

      ab.Days = 90;
      ab.AbsorptionRatio = 5;
      ab.AbsorptionRange = 3;
      ab.AbsorptionVolume = 18;

      Add(ab);
      Add(ateer);
    }
    #endregion

    private readonly RSI _rsi = new() { Period = 14 };
    private readonly BollingerBands _bb = new BollingerBands() { Period = 20, Shift = 0, Width = 2 };
    private readonly Absorption ab = new Absorption();
    private readonly ATR ateer = new ATR() { Period = 14 };

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

    private void LoadLines(String sLevels)
    {
      string sDesc = string.Empty;
      string sPrice = string.Empty;

      try
      {
        int i = 2;
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
            days a = new days();
            a.idx = i;
            a.price = Convert.ToDecimal(price);
            a.label = desc;
            lsS.Add(a);
          }
          i++;
        }
      }
      catch (Exception ex)
      {
      }
    }

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

      if (!bFirstDisplay)
      {
        LoadLines(sKPValues);
        bFirstDisplay = true;
      }

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

      //var abb = ((ValueDataSeries)ab.DataSeries[0])[pbar];
      var bb_mid = ((ValueDataSeries)_bb.DataSeries[0])[bar]; // mid
      var bb_top = ((ValueDataSeries)_bb.DataSeries[1])[bar]; // top
      var bb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[bar]; // bottom
      var pbb_top = ((ValueDataSeries)_bb.DataSeries[1])[bar - 1]; // top
      var ppbb_top = ((ValueDataSeries)_bb.DataSeries[1])[bar - 2]; // top
      var pbb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[bar - 1]; // bottom
      var ppbb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[bar - 2]; // bottom
      var rsi = ((ValueDataSeries)_rsi.DataSeries[0])[bar];
      var rsi1 = ((ValueDataSeries)_rsi.DataSeries[0])[bar - 1];
      var rsi2 = ((ValueDataSeries)_rsi.DataSeries[0])[bar - 2];
      var atr = ((ValueDataSeries)ateer.DataSeries[0])[bar];

      #endregion

      // LINE BOUNCES
      foreach (days s in lsS)
      {
        if (s.label != "")
        {
          // GREEN DOUBLE WICK, engulfing
          if (c0G && candle.Low < s.price && candle.Open > s.price &&
            c1R && p1C.Low < s.price && p1C.Close > s.price && c0Body > c1Body &&
            c2R && p2C.Close > p1C.Close)
          {
            _paintBars[bar] = MColor.FromRgb(255, 204, 0);
            iFutureSound = LINE_WICK_ENG;
          }
          // RED DOUBLE WICK, engulfing
          else if (c0R && candle.High > s.price && candle.Open < s.price &&
            c1G && p1C.High > s.price && p1C.Close < s.price && c0Body > c1Body &&
            c2G && p2C.Close < p1C.Close)
          {
            _paintBars[bar] = MColor.FromRgb(255, 204, 0);
            iFutureSound = LINE_WICK_ENG;
          }

          // GREEN DOUBLE TOUCH, engulfing
          else if (c0G && candle.High > s.price && candle.Low < s.price && c1R && p1C.Low < s.price && c0Body > c1Body)
          {
            _paintBars[bar] = MColor.FromRgb(255, 204, 0);
            iFutureSound = LINE_TOUCH_ENG;
          }
          // RED DOUBLE TOUCH, engulfing
          else if (c0R && candle.Low < s.price && candle.High > s.price && c1G && p1C.High > s.price && p1C.Low < s.price && c0Body > c1Body)
          {
            _paintBars[bar] = MColor.FromRgb(255, 204, 0);
            iFutureSound = LINE_TOUCH_ENG;
          }
        }
      }

      // ENGULFING CANDLE OFF THE BOLLINGER BAND
      if (candle.High > bb_top && p1C.High > pbb_top &&
         c0Body > c1Body && c0R && c1G)
      {
        _paintBars[pbar] = MColor.FromRgb(255, 255, 255);
        _negSeries[pbar] = candle.High + (_tick * 2);
        iFutureSound = ENG_BB;
      }
      else if (candle.Low < bb_bottom && p1C.Low < pbb_bottom &&
         c1Body > c2Body && c1G && c2R)
      {
        _paintBars[pbar] = MColor.FromRgb(255, 255, 255);
        _posSeries[pbar] = candle.Low - (_tick * 2);
        iFutureSound = ENG_BB;
      }

      #region REVERSAL ALERTS
      /*
            // INSANT reversal, next bar
            if (c0G && iLastWickBar == bar - 1 && iLastWickPrice < close && iLastWickPrice < open)
              iFutureSound = VOL_IMB_REVERSE;
            // INSANT reversal, next bar
            if (c0R && iLastWickBar == bar - 1 && iLastWickPrice > close && iLastWickPrice > open)
              iFutureSound = VOL_IMB_REVERSE;

            // WICK WICK WICK - Two or three candle reversal
            if (c0R &&
                (iLastWickBar == bar - 1 || iLastWickBar == bar - 2 || iLastWickBar == bar - 3) &&
                ((p1C.Close > iLastWickPrice && p1C.Open > iLastWickPrice) ||
                (p2C.Close > iLastWickPrice && p2C.Open > iLastWickPrice) ||
                (close > iLastWickPrice && open > iLastWickPrice)))
              iFutureSound = VOL_IMB_REVERSE;
            // Two or three candle reversal
            if (c0G &&
                (iLastWickBar == bar - 1 || iLastWickBar == bar - 2 || iLastWickBar == bar - 3) &&
                ((p1C.Close < iLastWickPrice && p1C.Open < iLastWickPrice) ||
                (p2C.Close < iLastWickPrice && p2C.Open < iLastWickPrice) ||
                (close < iLastWickPrice && open < iLastWickPrice)))
              iFutureSound = VOL_IMB_REVERSE;

            // REGULAR TOUCH - Two or three candle reversal
            if (c0R &&
                (iLastTouchBar == bar - 1 || iLastTouchBar == bar - 2 || iLastTouchBar == bar - 3) &&
                ((p1C.Close > iLastTouchPrice && p1C.Open > iLastTouchPrice) ||
                (p2C.Close > iLastTouchPrice && p2C.Open > iLastTouchPrice) ||
                (close > iLastTouchPrice && open > iLastTouchPrice)))
              iFutureSound = VOL_IMB_REVERSE;
            // Two or three candle reversal
            if (c0G &&
                (iLastTouchBar == bar - 1 || iLastTouchBar == bar - 2 || iLastTouchBar == bar - 3) &&
                ((p1C.Close < iLastTouchPrice && p1C.Open < iLastTouchPrice) ||
                (p2C.Close < iLastTouchPrice && p2C.Open < iLastTouchPrice) ||
                (close < iLastTouchPrice && open < iLastTouchPrice)))
              iFutureSound = VOL_IMB_REVERSE;
      */
      #endregion

      #region VOLUME IMBALANCES

      var highPen = new Pen(new SolidBrush(Color.FromArgb(255, 156, 227, 255)))
      { Width = iLineWidth, DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };

      // REGULAR VOLUME IMBALANCE
      if (c0G && c1G && open > p1C.Close)
      {
        if (LineTouches.IndexOf(pbar) == -1)
        {
          LineTouches.Add(pbar);
          HorizontalLinesTillTouch.Add(new LineTillTouch(pbar, pcandle.Open, highPen));
          if (LineTouches.Contains(pbar - 1))
            iFutureSound = DOUBLE_VOL_IMB_DROP_G;
          _posUP[pbar] = candle.Low - (_tick);
        }
      }
      else if (c0R && c1R && open < p1C.Close)
      {
        if (LineTouches.IndexOf(pbar) == -1)
        {
          LineTouches.Add(pbar);
          HorizontalLinesTillTouch.Add(new LineTillTouch(pbar, pcandle.Open, highPen));
          if (LineTouches.Contains(pbar - 1))
            iFutureSound = DOUBLE_VOL_IMB_DROP_R;
          _negDN[pbar] = candle.High + (_tick);
        }
      }

      // VOLUME IMBALANCE PLUS BB
      if (c0G && c1G && open > p1C.Close && p1C.Low > pbb_bottom)
      {
        iFutureSound = VOL_IMB_DROP_BB_G;
      }
      else if (c0R && c1R && open < p1C.Close && p1C.High > pbb_top)
      {
        iFutureSound = VOL_IMB_DROP_BB_R;
      }

      foreach (LineTillTouch ltt in HorizontalLinesTillTouch)
      {
        int iDistance = ltt.SecondBar - ltt.FirstBar;

        /*
          // WICKED THE VOLUME IMBALANCE
          if (ltt.Finished && ltt.SecondBar == bar && iDistance > iMinBars && cc.High > ltt.SecondPrice && cc.Close < ltt.SecondPrice && cc.Open < ltt.SecondPrice)
          {
            iLastWickBar = bar;
            iLastWickPrice = ltt.SecondPrice;
            iFutureSound = c0G ? VOL_IMB_WICK_G : VOL_IMB_WICK_R;
            _paintBars[bar] = MColor.FromRgb(255, 204, 1);
            break;
          }
          // TOUCHED THE VOLUME IMBALANCE
          else if (ltt.Finished && ltt.SecondBar == bar && iDistance > iMinBars)
          {
            iLastTouchBar = bar;
            iLastTouchPrice = ltt.SecondPrice;
            iFutureSound = c0G ? VOL_IMB_TOUCH_G : VOL_IMB_TOUCH_R;
            _paintBars[bar] = MColor.FromRgb(255, 204, 1);
            break;
          }
          else if (ltt.Finished && iDistance < iMinBars)
          {
            HorizontalLinesTillTouch.Remove(ltt);
            break;
          }
        */
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
        if (_lastBarCounted && iPrevSound != iFutureSound)
        {
          var atrUp = candle.High + (atr * 1.6m);
          var atrDown = candle.Low - (atr * 1.6m);
          var extras = $" {InstrumentInfo.Exchange}:{InstrumentInfo.Instrument} at {candle.Close.ToString()}";
          var extras2 = $" Take Profit: , Stop Loss: ";
          var extras3 = $" Seen on {DateTime.Now.ToShortTimeString()} ticks";

          switch (iFutureSound)
          {
            case LINE_WICK_ENG:
              Task.Run(() => PlaySoundAsync(@"c:\temp\sounds\linetouch.wav"));
              break;
            case LINE_TOUCH_ENG:
              Task.Run(() => PlaySoundAsync(@"c:\temp\sounds\linetouch.wav"));
              break;

            // TWO VOLUME IMBALANCEs dropped right after each other.  Good buy/sell signals
            case DOUBLE_VOL_IMB_DROP_G:
              Task.Run(() => Hooks("BUY SIGNAL on" + extras, "buy"));
              break;
            case DOUBLE_VOL_IMB_DROP_R:
              Task.Run(() => Hooks("SELL SIGNAL on" + extras, "sell"));
              break;

            // VOLUME IMBALANCE dropped near the low/high of the bollinger bands
            case VOL_IMB_DROP_BB_G:
              Task.Run(() => Hooks("BB with Gap Green on" + extras, "engulf"));
              break;
            case VOL_IMB_DROP_BB_R:
              Task.Run(() => Hooks("BB with Gap Red on" + extras, "engulf"));
              break;

            case VOL_IMB_TOUCH_G:
              Task.Run(() => Hooks("VolImb filled on" + extras, ""));
              break;
            case VOL_IMB_TOUCH_R:
              Task.Run(() => Hooks("VolImb filled on" + extras, ""));
              break;

            case TRAMPOLINE:
              Task.Run(() => Hooks("TRAMPOLINE on" + extras, "trampoline"));
              break;
            case ENG_BB:
              Task.Run(() => Hooks("ENG OFF BB on" + extras, "engulf"));
              break;

            case VOL_IMB_WICK_G:
              Task.Run(() => Hooks("VolImb WICK on" + extras, ""));
              break;
            case VOL_IMB_WICK_R:
              Task.Run(() => Hooks("VolImb WICK on" + extras, ""));
              break;
            case VOL_IMB_FILL:
              Task.Run(() => Hooks("VolImb FILL on" + extras, ""));
              break;
            case VOL_IMB_ENGULFING:
              Task.Run(() => Hooks("VolImb ENGULFING on" + extras, ""));
              break;
            case VOL_IMB_REVERSE:
              Task.Run(() => Hooks("VolImb REVERSE on" + extras, ""));
              break;

            default: break;
          }
          iPrevSound = iFutureSound;
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

    private async Task SendWebhook(string message)
    {
      String whurl = "https://discord.com/api/webhooks/1363535853695271022/GhWnTvrfkRHsC0BRcuOsiRZdq1ED_JfNO0eWT7rxwNsvnCiDqg1ypwa56vULTveVQ12L";
      Uri uri = new Uri(whurl);
      client1.Headers.Add("Content-Type", "application/json");

      client1.UploadData(whurl, Encoding.UTF8.GetBytes(message));
      // client1.UploadFileAsync(uri, sFil);
    }
    private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    private async Task Hooks(string message, string wav)
    {
      var SoundFilePath = @"c:\temp\sounds\" + wav + ".wav";

      return;
      var sendWebhookTask = SendWebhook(message);
      var Sound = PlaySoundAsync(SoundFilePath);
      await Task.WhenAll(sendWebhookTask, Sound);
    }

    private async Task PlaySoundAsync(String SoundFilePath)
    {
      SoundPlayer _soundPlayer = new();
          try
          {
            if (File.Exists(SoundFilePath))
            {
                _soundPlayer.SoundLocation = SoundFilePath;
                _soundPlayer.Play();
            }
          }
          catch (Exception ex)
          {
            //
          }
    }

    #endregion

  }
}