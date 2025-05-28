#region INCLUDES

namespace ATAS.Indicators.Technical;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net;
using Indicators;
using Drawing;
using OFT.Attributes.Editors;
using Newtonsoft.Json.Linq;
using OFT.Rendering.Context;
using OFT.Rendering.Tools;
using static SampleProperties;
using Color = System.Drawing.Color;
using MColor = System.Windows.Media.Color;
using MColors = System.Windows.Media.Colors;
using Pen = System.Drawing.Pen;
using String = string;
using System.Globalization;
using OFT.Rendering.Settings;
using System.Text;
using System.Linq;
using System.Windows.Media;
using System.Collections;

#endregion

[DisplayName("ATAS_Scan")]
public class ATAS_Scan : Indicator
{
  private const String sVersion = "1.6";

  #region PRIVATE FIELDS

  private struct MarketData
  {
    public string Symbol { get; set; }
    public string Desc { get; set; }
    public double Value { get; set; }

    public MarketData(string symbol, string desc, double value)
    {
      Symbol = symbol;
      Desc = desc;
      Value = value;
    }
    public override string ToString()
    {
      return $"Symbol: {Symbol}, Desc: {Desc}, Value: {Value}";
    }
  }

  private List<MarketData> markets;
  private readonly PaintbarsDataSeries _paintBars = new("Paint bars");
  private WebClient client1 = new();
  private string filePath = @"c:\temp\motivelines.csv";

  private int iFutureSound = 0;
  private int _highBar;
  private int _lastBar = -1;
  private bool _lastBarCounted;
  private bool bBuyShown = false;
  private bool bSellShown = false;

  private bool bCSVImported = false;
  private int iOffset = 9;
  private int iFontSize = 10;
  private int iWaddaSensitivity = 150;
  private int iMACDSensitivity = 70;

  private const int LINE_WICK_ENG = 1;
  private const int LINE_TOUCH_ENG = 2;
  private const int DOUBLE_VOL_IMB_DROP_R = 3;
  private const int DOUBLE_VOL_IMB_DROP_G = 4;
  private const int ENG_BB_G = 5;
  private const int ENG_BB_R = 22;
  private const int BUY_DOTS = 6;
  private const int SELL_DOTS = 7;
  private const int TRAMPOLINE = 8;
  private const int SQUEEZE = 9;
  private const int STAIRS = 10;
  private const int VOL_IMB_DROP_BB_G = 11;
  private const int VOL_IMB_DROP_BB_R = 12;
  private const int VOL_IMB_DROP_G = 13;
  private const int VOL_IMB_DROP_R = 14;
  private const int VOL_IMB_ENGULFING = 15;
  private const int VOL_IMB_FILL = 16;
  private const int VOL_IMB_REVERSE = 17;
  private const int VOL_IMB_TOUCH_G = 18;
  private const int VOL_IMB_TOUCH_R = 19;
  private const int VOL_IMB_WICK_G = 20;
  private const int VOL_IMB_WICK_R = 21;

  #endregion

  #region SETTINGS

  [Display(GroupName = "Alerts", Name = "Use Alert Sounds")]
  public bool UseAlerts { get; set; }

  private decimal VolSec(IndicatorCandle c)
  {
    return c.Volume / Convert.ToDecimal((c.LastTime - c.Time).TotalSeconds);
  }

  #endregion

  #region CONSTRUCTOR

  public ATAS_Scan() :
      base(true)
  {
    DenyToChangePanel = true;

    DataSeries[0] = _posSeries;
    DataSeries.Add(_negSeries);
    DataSeries.Add(_negWhite);
    DataSeries.Add(_posWhite);
    DataSeries.Add(_squeezie);
    DataSeries.Add(_paintBars);

    Add(_ft);
    Add(_sq);
    Add(_psar);
    Add(_atr);
    Add(_hma);
    Add(ateer);
  }

  #endregion

  #region INDICATORS

  private readonly SMA _Sshort = new() { Period = 3 };
  private readonly SMA _Slong = new() { Period = 10 };
  private readonly SMA _Ssignal = new() { Period = 16 };
  private readonly RSI _rsi = new() { Period = 14 };
  private readonly ATR _atr = new() { Period = 14 };
  private readonly ParabolicSAR _psar = new();
  private readonly EMA _21 = new() { Period = 21 };
  private readonly HMA _hma = new() { };
  private readonly EMA fastEma = new() { Period = 20 };
  private readonly EMA slowEma = new() { Period = 40 };
  private readonly FisherTransform _ft = new() { Period = 10 };

  private readonly BollingerBands _bb = new()
  { Period = 20, Shift = 0, Width = 2 };

  private readonly SqueezeMomentum _sq = new()
  {
    BBPeriod = 20,
    BBMultFactor = 2,
    KCPeriod = 20,
    KCMultFactor = 1.5m,
    UseTrueRange = false
  };

  private readonly Absorption ab = new();
  private readonly ATR ateer = new() { Period = 14 };

  #endregion

  protected void DrawText(int bBar, String strX, Color cI, Color cB, bool bOverride = false, bool bSwap = false)
  {
    var candle = GetCandle(bBar);

    var _tick = ChartInfo.PriceChartContainer.Step;
    decimal loc = 0;

    if (candle.Close > candle.Open || bOverride)
      loc = candle.High + _tick * iOffset;
    else
      loc = candle.Low - _tick * iOffset;

    if (candle.Close > candle.Open && bSwap)
      loc = candle.Low - _tick * (iOffset * 2);
    else if (candle.Close < candle.Open && bSwap)
      loc = candle.High + _tick * iOffset;

    if (strX == "▼")
      loc = candle.High + _tick * iOffset;
    if (strX == "▲")
      loc = candle.Low - _tick * (iOffset * 2);

    AddText("Aver" + bBar, strX, true, bBar, loc, cI, cB, iFontSize,
        DrawingText.TextAlign.Center);
  }

  #region DATA SERIES

  [Display(Name = "Font Size", GroupName = "Drawing", Order = int.MaxValue)]
  [Range(1, 90)]
  public int TextFont
  {
    get => iFontSize;
    set
    {
      iFontSize = value;
      RecalculateValues();
    }
  }

  [Display(Name = "Text Offset", GroupName = "Drawing", Order = int.MaxValue)]
  [Range(0, 900)]
  public int Offset
  {
    get => iOffset;
    set
    {
      iOffset = value;
      RecalculateValues();
    }
  }

  private readonly ValueDataSeries _squeezie = new("Squeeze Relaxer")
  { Color = MColors.Yellow, VisualType = VisualMode.Dots, Width = 3 };

  private readonly ValueDataSeries _posWhite = new("Vol Imbalance Sell")
  { Color = MColors.White, VisualType = VisualMode.DownArrow, Width = 1 };

  private readonly ValueDataSeries _negWhite = new("Vol Imbalance Buy")
  { Color = MColors.White, VisualType = VisualMode.UpArrow, Width = 1 };

  private readonly ValueDataSeries _posSeries = new("Regular Buy Signal")
  {
    Color = MColor.FromArgb(255, 0, 255, 0),
    VisualType = VisualMode.Dots,
    Width = 2
  };

  private readonly ValueDataSeries _negSeries = new("Regular Sell Signal")
  {
    Color = MColor.FromArgb(255, 255, 104, 48),
    VisualType = VisualMode.Dots,
    Width = 2
  };

  #endregion

  protected void ImportCSVFile() 
  {
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
          if (double.TryParse(parts[2].Trim(), out double value))
            markets.Add(new MarketData(symbol, desc, value));
        }
      }
      bCSVImported = true;
    }
    catch { }   
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

    if (bar < 6)
      return;

    if (!bCSVImported)
      ImportCSVFile();

    #region CANDLE CALCULATIONS

    var candle = GetCandle(bar);
    var pcandle = GetCandle(bar - 1);
    var ppcandle = GetCandle(bar - 2);
    var pbar = bar - 1;
    var ppbar = bar - 2;
    value = candle.Close;
    var cc = GetCandle(bar);

    var _tick = ChartInfo.PriceChartContainer.Step;
    var p1C = GetCandle(pbar - 1);
    var p2C = GetCandle(pbar - 2);
    var p3C = GetCandle(pbar - 3);
    var p4C = GetCandle(pbar - 4);

    var close = cc.Close;
    var open = cc.Open;
    var plow = pcandle.Low;
    var pclose = pcandle.Close;
    var ppclose = ppcandle.Close;
    var high = cc.High;
    var phigh = pcandle.High;
    var low = cc.Low;
    var popen = pcandle.Open;
    var ppopen = ppcandle.Open;
    var red = candle.Close < candle.Open;
    var green = candle.Close > candle.Open;

    var c00G = pcandle.Open < pcandle.Close;
    var c00R = pcandle.Open > pcandle.Close;
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

    var c00Body = Math.Abs(pcandle.Close - pcandle.Open);
    var c0Body = Math.Abs(candle.Close - candle.Open);
    var c1Body = Math.Abs(p1C.Close - p1C.Open);
    var c2Body = Math.Abs(p2C.Close - p2C.Open);
    var c3Body = Math.Abs(p3C.Close - p3C.Open);
    var c4Body = Math.Abs(p4C.Close - p4C.Open);

    var upWickLarger = c0R && Math.Abs(candle.High - candle.Open) >
        Math.Abs(candle.Low - candle.Close);
    var downWickLarger = c0G && Math.Abs(candle.Low - candle.Open) >
        Math.Abs(candle.Close - candle.High);

    var ThreeOutUp = c2R && c1G && c0G && p1C.Open < p2C.Close &&
                     p2C.Open < p1C.Close &&
                     Math.Abs(p1C.Open - p1C.Close) >
                     Math.Abs(p2C.Open - p2C.Close) &&
                     candle.Close > p1C.Low;

    var ThreeOutDown = c2G && c1R && c0R && p1C.Open > p2C.Close &&
                       p2C.Open > p1C.Close &&
                       Math.Abs(p1C.Open - p1C.Close) >
                       Math.Abs(p2C.Open - p2C.Close) &&
                       candle.Close < p1C.Low;

    #endregion

    #region INDICATORS CALCULATE

    fastEma.Calculate(pbar, value);
    slowEma.Calculate(pbar, value);
    _21.Calculate(pbar, value);

    _bb.Calculate(pbar, value);
    _rsi.Calculate(pbar, value);

    var ema21 = ((ValueDataSeries)_21.DataSeries[0])[pbar];
    var fast = ((ValueDataSeries)fastEma.DataSeries[0])[pbar];
    var fastM = ((ValueDataSeries)fastEma.DataSeries[0])[pbar - 1];
    var slow = ((ValueDataSeries)slowEma.DataSeries[0])[pbar];
    var slowM = ((ValueDataSeries)slowEma.DataSeries[0])[pbar - 1];
    var sq1 = ((ValueDataSeries)_sq.DataSeries[0])[pbar];
    var psq1 = ((ValueDataSeries)_sq.DataSeries[0])[pbar - 1];
    var ppsq1 = ((ValueDataSeries)_sq.DataSeries[0])[pbar - 2];
    var f1 = ((ValueDataSeries)_ft.DataSeries[0])[pbar];
    var f2 = ((ValueDataSeries)_ft.DataSeries[1])[pbar];
    var psar = ((ValueDataSeries)_psar.DataSeries[0])[pbar];
    var rsi = ((ValueDataSeries)_rsi.DataSeries[0])[pbar];
    var rsi1 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 1];
    var rsi2 = ((ValueDataSeries)_rsi.DataSeries[0])[pbar - 2];
    var hma = ((ValueDataSeries)_hma.DataSeries[0])[pbar];
    var phma = ((ValueDataSeries)_hma.DataSeries[0])[pbar - 1];

    var bb_top = ((ValueDataSeries)_bb.DataSeries[1])[pbar]; // top
    var bb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[pbar]; // bottom

    var pbb_top = ((ValueDataSeries)_bb.DataSeries[1])[pbar - 1]; // top
    var ppbb_top = ((ValueDataSeries)_bb.DataSeries[1])[pbar - 2]; // top

    var pbb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[pbar - 1]; // bottom
    var ppbb_bottom = ((ValueDataSeries)_bb.DataSeries[2])[pbar - 2]; // bottom

    var atr = ((ValueDataSeries)ateer.DataSeries[0])[bar];

    var macd = _Sshort.Calculate(pbar, value) -
               _Slong.Calculate(pbar, value);
    var signal = _Ssignal.Calculate(pbar, macd);

    var hullUp = hma > phma;
    var hullDown = hma < phma;
    var fisherUp = f1 < f2;
    var fisherDown = f2 < f1;
    var macdUp = macd > signal;
    var macdDown = macd < signal;

    var psarBuy = psar < candle.Close;
    var psarSell = psar > candle.Close;

    var t1 = (fast - slow - (fastM - slowM)) * iWaddaSensitivity;

    #endregion

    #region BUY / SELL

    if (!bBuyShown && close > ema21 && psarBuy && macdUp && fisherUp && t1 >= 0 && hullUp)
    {
      bBuyShown = true;
      bSellShown = false;
      _posSeries[pbar] = candle.Low - _tick * iOffset;
      iFutureSound = BUY_DOTS;
    }
    else if (!bSellShown && close < ema21 && psarSell && macdDown && fisherDown && t1 < 0 && hullDown)
    {
      bSellShown = true;
      bBuyShown = false;
      _negSeries[pbar] = candle.High + _tick * iOffset;
      iFutureSound = SELL_DOTS;
    }

    // ENGULFING CANDLE OFF THE BOLLINGER BAND
    if (high > bb_top && phigh > pbb_top &&
        c0Body > c1Body && c0R && c1G)
    {
      _paintBars[pbar] = MColor.FromRgb(255, 255, 255);
      _negSeries[pbar] = candle.High + _tick * 2;
      iFutureSound = ENG_BB_R;
    }
    else if (candle.Low < bb_bottom && p1C.Low < pbb_bottom &&
             c1Body > c2Body && c1G && c2R)
    {
      _paintBars[pbar] = MColor.FromRgb(255, 255, 255);
      _posSeries[pbar] = candle.Low - _tick * 2;
      iFutureSound = ENG_BB_G;
    }

    // VOLUME IMBALANCE PLUS BB
    if (c0G && c1G && open > pclose && plow < pbb_bottom)
    {
      iFutureSound = VOL_IMB_DROP_BB_G;
    }
    else if (c0R && c1R && open < pclose && phigh > pbb_top)
    {
      iFutureSound = VOL_IMB_DROP_BB_R;
    }

    // DOUBLE VOL IMB
    if (c0G && c1G && open > pclose && popen < ppclose)
    {
      iFutureSound = DOUBLE_VOL_IMB_DROP_G;
    }
    else if (c0R && c1R && open < pclose && popen < ppclose)
    {
      iFutureSound = DOUBLE_VOL_IMB_DROP_R;
    }

    #endregion

    #region ADVANCED LOGIC

    // Squeeze momentum relaxer show
    if (sq1 > 0 && sq1 < psq1 && psq1 > ppsq1)
    {
      //            DrawText(pbar, "▼", Color.Yellow, Color.Transparent, false, true);
      iFutureSound = SQUEEZE;
    }
    else if (sq1 < 0 && sq1 > psq1 && psq1 < ppsq1)
    {
      //            DrawText(pbar, "▲", Color.Yellow, Color.Transparent, false, true);
      iFutureSound = SQUEEZE;
    }

    if (true)
      if (c4Body > c3Body && c3Body > c2Body && c2Body > c1Body && c1Body > c0Body)
        if ((candle.Close > p1C.Close && p1C.Close > p2C.Close && p2C.Close > p3C.Close) ||
            (candle.Close < p1C.Close && p1C.Close < p2C.Close && p2C.Close < p3C.Close))
        {
          //                    DrawText(pbar, "Stairs", Color.Yellow, Color.Transparent);
          iFutureSound = STAIRS;
        }

    if (true)
    {
      //            if (c0G && c1R && c2R && VolSec(p1C) > VolSec(p2C) &&
      //              VolSec(p2C) > VolSec(p3C) && candle.Delta < 0)
      //                DrawText(pbar, "Vol\nRev", Color.Yellow, Color.Transparent, false, true);
      //            if (c0R && c1G && c2G && VolSec(p1C) > VolSec(p2C) &&
      //                VolSec(p2C) > VolSec(p3C) && candle.Delta > 0)
      //                DrawText(pbar, "Vol\nRev", Color.Lime, Color.Transparent, false, true);
      //            if (ThreeOutUp)
      //                DrawText(pbar, "3oU", Color.Yellow, Color.Transparent);
      //            if (ThreeOutDown)
      //                DrawText(pbar, "3oD", Color.Yellow, Color.Transparent);
    }

    // Trampoline
    if (true)
    {
      if (c0R && c1R && candle.Close < p1C.Close &&
          (rsi >= 70 || rsi1 >= 70 || rsi2 >= 70) &&
          c2G && p2C.High >= bb_top - _tick * 30)
      {
        DrawText(pbar, "TR", Color.Yellow, Color.BlueViolet, false,
            true);
        iFutureSound = TRAMPOLINE;
      }

      if (c0G && c1G && candle.Close > p1C.Close &&
          (rsi < 25 || rsi1 < 25 || rsi2 < 25) &&
          c2R && p2C.Low <= bb_bottom + _tick * 30)
      {
        DrawText(pbar, "TR", Color.Yellow, Color.BlueViolet, false,
            true);
        iFutureSound = TRAMPOLINE;
      }
    }

    #endregion

    #region ALERTS LOGIC

    if (_lastBar != bar)
    {
      if (_lastBarCounted)
      {
        var atrUp = candle.High + atr * 1.6m;
        var atrDown = candle.Low - atr * 1.6m;
        var extras =
            $" {InstrumentInfo!.Instrument} at {candle.Close.ToString()}";
        var extras2 = $" Take Profit: , Stop Loss: ";
        var extras3 =
            $" Seen on {DateTime.Now.ToShortTimeString()} ticks";

        var priceString = candle.Close.ToString();

        switch (iFutureSound)
        {
          // TWO VOLUME IMBALANCEs dropped right after each other.  Good buy/sell signals
          case DOUBLE_VOL_IMB_DROP_G:
            Task.Run(() => Hooks("BUY SIGNAL on" + extras, "buy"));
            break;
          case DOUBLE_VOL_IMB_DROP_R:
            Task.Run(() =>
                Hooks("SELL SIGNAL on" + extras, "sell"));
            break;

          // VOLUME IMBALANCE dropped near the low/high of the bollinger bands
          case VOL_IMB_DROP_BB_G:
            Task.Run(() =>
                Hooks("BB Gap GREEN on" + extras, "engulf"));
            break;
          case VOL_IMB_DROP_BB_R:
            Task.Run(() =>
                Hooks("BB Gap RED on" + extras, "engulf"));
            break;

          case VOL_IMB_TOUCH_G:
            Task.Run(() => Hooks("VolImb filled on" + extras, ""));
            break;
          case VOL_IMB_TOUCH_R:
            Task.Run(() => Hooks("VolImb filled on" + extras, ""));
            break;

          case TRAMPOLINE:
            Task.Run(() =>
                Hooks("TRAMPOLINE on" + extras, "trampoline"));
            break;
          case ENG_BB_G:
            Task.Run(() =>
                Hooks("Engulfing GREEN off BB on" + extras, "engulf"));
            break;
          case ENG_BB_R:
            Task.Run(() =>
                Hooks("Engulfing RED off BB on" + extras, "engulf"));
            break;

          case BUY_DOTS:
            Task.Run(() =>
                Hooks("BUY SIGNAL on" + extras, "buy"));
            break;
          case SELL_DOTS:
            Task.Run(() =>
                Hooks("SELL SIGNAL on" + extras, "sell"));
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
            Task.Run(() =>
                Hooks("VolImb ENGULFING on" + extras, ""));
            break;
          case VOL_IMB_REVERSE:
            Task.Run(() => Hooks("VolImb REVERSE on" + extras, ""));
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

  private async Task SendWebhook(string message)
  {
    var whurl =
        "https://discord.com/api/webhooks/1374027130643550332/ELKh65dhulJH_q2K7YHxYRmMhVOeSKNe1Kv_1Bx953KDvXe60rXLeSl__zlrFJynczbP";
    var uri = new Uri(whurl);
    client1.Headers.Add("Content-Type", "application/json");
    client1.UploadData(whurl, Encoding.UTF8.GetBytes("{ \"content\": \"" + message + "\" }"));
    //client1.UploadData(whurl, Encoding.UTF8.GetBytes(message));
    // client1.UploadFileAsync(uri, sFil);
  }

  private SemaphoreSlim semaphore = new(1, 1);

  private async Task Hooks(string message, string wav)
  {
    var SoundFilePath = @"c:\temp\sounds\" + wav + ".wav";
    var sendWebhookTask = SendWebhook(message);
    await Task.WhenAll(sendWebhookTask);
  }

  #endregion

}