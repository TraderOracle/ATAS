namespace ATAS.Indicators.Technical
{
  #region INCLUDES
  using System;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.Media;
  using System.Net.Http;
  using System.Text;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using ATAS.Indicators;
  using ATAS.Indicators.Drawing;
  using OFT.Rendering.Context;
  using Color = System.Drawing.Color;
  using MColor = System.Windows.Media.Color;
  using MColors = System.Windows.Media.Colors;
  using Pen = System.Drawing.Pen;
  using SolidBrush = System.Drawing.SolidBrush;
  using OFT.Rendering.Tools;
  #endregion

  [DisplayName("TO VolImb")]
  public class TOVolImb : Indicator, IDisposable
  {
    #region CONSTANTS
    private const string VERSION = "1.8";

    // Pattern detection constants
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
    #endregion

    #region FIELDS
    private readonly struct PriceLevel
    {
      public int Index { get; }
      public string Label { get; }
      public string Ticker { get; }
      public decimal Price { get; }
      public decimal Price2 { get; }
      public Color Color { get; }
      public string Source { get; }

      public PriceLevel(int index, string label, string ticker, decimal price, Color color)
      {
        Index = index;
        Label = label;
        Ticker = ticker;
        Price = price;
        Price2 = 0;
        Color = color;
        Source = string.Empty;
      }
    }

    private readonly List<PriceLevel> _priceLevels = new List<PriceLevel>();
    private readonly HashSet<int> _lineTouches = new HashSet<int>();
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly SemaphoreSlim _webhookSemaphore = new SemaphoreSlim(1, 1);
    private readonly SoundPlayer _soundPlayer = new SoundPlayer();

    private bool _isInitialized;
    private string _lastScreenshotPath = string.Empty;
    private int _lastTouchBar;
    private decimal _lastTouchPrice;
    private int _lastWickBar;
    private decimal _lastWickPrice;
    private int _futureSoundAlert;
    private int _previousSoundAlert;
    private int _lastBar = -1;
    private bool _lastBarCounted;

    // Technical indicators
    private readonly RSI _rsi = new RSI { Period = 14 };
    private readonly BollingerBands _bb = new BollingerBands { Period = 20, Shift = 0, Width = 2 };
    private readonly Absorption _absorption = new Absorption { Days = 90, AbsorptionRatio = 5, AbsorptionRange = 3, AbsorptionVolume = 18 };
    private readonly ATR _atr = new ATR { Period = 14 };

    // Data series
    private readonly PaintbarsDataSeries _paintBars = new PaintbarsDataSeries("Paint bars");
    private readonly ValueDataSeries _posSeries = new ValueDataSeries("Vol Imbalance Sell") { Color = MColors.White, VisualType = VisualMode.Dots, Width = 3 };
    private readonly ValueDataSeries _negSeries = new ValueDataSeries("Vol Imbalance Buy") { Color = MColors.White, VisualType = VisualMode.Dots, Width = 3 };
    private readonly ValueDataSeries _posUp = new ValueDataSeries("Vol Imbalance Up") { Color = MColors.Lime, VisualType = VisualMode.Block, Width = 3 };
    private readonly ValueDataSeries _negDown = new ValueDataSeries("Vol Imbalance Down") { Color = MColors.Red, VisualType = VisualMode.Block, Width = 3 };
    #endregion

    #region PROPERTIES
    [Display(Name = "CSV File Path", GroupName = "Configuration", Order = 1)]
    public string CsvFilePath { get; set; } = @"c:\temp\motivelines.csv";

    [Display(Name = "Discord Webhook URL", GroupName = "Configuration", Order = 2)]
    public string DiscordWebhookUrl { get; set; } = string.Empty;

    [Display(Name = "Sound Directory", GroupName = "Configuration", Order = 3)]
    public string SoundDirectory { get; set; } = @"C:\Program Files (x86)\ATAS Platform\Sounds";

    [Display(Name = "Screenshot Directory", GroupName = "Configuration", Order = 4)]
    public string ScreenshotDirectory { get; set; } = @"c:\temp";

    [Display(Name = "Line Width", GroupName = "Appearance", Order = 10)]
    [Range(1, 10)]
    public int LineWidth { get; set; } = 2;

    [Display(Name = "Font Size", GroupName = "Appearance", Order = 11)]
    [Range(8, 20)]
    public int FontSize { get; set; } = 10;

    [Display(Name = "Text Offset", GroupName = "Appearance", Order = 12)]
    [Range(0, 50)]
    public int TextOffset { get; set; } = 9;

    [Display(Name = "Minimum Candle Gap", GroupName = "Detection", Order = 20)]
    [Range(1, 10)]
    public int MinimumBars { get; set; } = 3;

    [Display(Name = "Use Alert Sounds", GroupName = "Alerts", Order = 30)]
    public bool UseAlertSounds { get; set; } = true;

    [Display(Name = "Send Discord Alerts", GroupName = "Alerts", Order = 31)]
    public bool SendDiscordAlerts { get; set; } = true;

    [Display(Name = "Capture Screenshots", GroupName = "Alerts", Order = 32)]
    public bool CaptureScreenshots { get; set; } = true;

    [Display(Name = "ES Chart X", GroupName = "Screenshot Coordinates", Order = 40)]
    public int EsChartX { get; set; } = 1180;

    [Display(Name = "ES Chart Y", GroupName = "Screenshot Coordinates", Order = 41)]
    public int EsChartY { get; set; } = 100;

    [Display(Name = "CL Chart X", GroupName = "Screenshot Coordinates", Order = 42)]
    public int ClChartX { get; set; } = 2780;

    [Display(Name = "CL Chart Y", GroupName = "Screenshot Coordinates", Order = 43)]
    public int ClChartY { get; set; } = 100;

    [Display(Name = "NQ Chart X", GroupName = "Screenshot Coordinates", Order = 44)]
    public int NqChartX { get; set; } = 180;

    [Display(Name = "NQ Chart Y", GroupName = "Screenshot Coordinates", Order = 45)]
    public int NqChartY { get; set; } = 1100;

    [Display(Name = "YM Chart X", GroupName = "Screenshot Coordinates", Order = 46)]
    public int YmChartX { get; set; } = 2780;

    [Display(Name = "YM Chart Y", GroupName = "Screenshot Coordinates", Order = 47)]
    public int YmChartY { get; set; } = 1000;

    [Display(Name = "Chart Width", GroupName = "Screenshot Coordinates", Order = 48)]
    public int ChartWidth { get; set; } = 1050;

    [Display(Name = "Chart Height", GroupName = "Screenshot Coordinates", Order = 49)]
    public int ChartHeight { get; set; } = 800;
    #endregion

    #region CONSTRUCTOR AND INITIALIZATION
    public TOVolImb() : base(true)
    {
      EnableCustomDrawing = true;
      DenyToChangePanel = true;
      SubscribeToDrawingEvents(DrawingLayouts.Historical);

      DataSeries[0] = _posSeries;
      DataSeries.Add(_negSeries);
      DataSeries.Add(_paintBars);
      DataSeries.Add(_posUp);
      DataSeries.Add(_negDown);

      Add(_absorption);
      Add(_atr);
    }

    public void Dispose()
    {
      _httpClient?.Dispose();
      _soundPlayer?.Dispose();
      _webhookSemaphore?.Dispose();
    }
    #endregion

    #region FILE OPERATIONS
    private void LoadPriceLevels()
    {
      _priceLevels.Clear();

      if (!File.Exists(CsvFilePath))
      {
        AddInfoLog($"CSV file not found: {CsvFilePath}");
        return;
      }

      try
      {
        var lines = File.ReadAllLines(CsvFilePath);
        foreach (var line in lines)
        {
          var parts = line.Split(',');
          if (parts.Length >= 3)
          {
            var symbol = parts[0].Trim();
            var description = parts[1].Trim();

            if (decimal.TryParse(parts[2].Trim(), out decimal price))
            {
              var color = GetLevelColor(description);
              _priceLevels.Add(new PriceLevel(0, description, symbol, price, color));
            }
          }
        }

        AddInfoLog($"Loaded {_priceLevels.Count} price levels from CSV");
      }
      catch (Exception ex)
      {
        AddErrorLog($"Error loading CSV: {ex.Message}");
      }
    }

    private Color GetLevelColor(string description)
    {
      var lower = description.ToLower();
      if (lower.Contains("mid")) return Color.Gold;
      if (lower.Contains(" l")) return Color.LimeGreen;
      if (lower.Contains(" h")) return Color.Red;
      if (lower.Contains("mmz")) return Color.Aqua;
      return Color.White;
    }
    #endregion

    #region RENDERING
    protected override void OnRender(RenderContext context, DrawingLayouts layout)
    {
      if (ChartInfo == null || InstrumentInfo == null) return;

      foreach (var level in _priceLevels)
      {
        var xPosition = ChartInfo.PriceChartContainer.GetXByBar(CurrentBar, false);
        var yPosition = ChartInfo.PriceChartContainer.GetYByPrice(level.Price, false);

        var pen = new RenderPen(level.Color, LineWidth, System.Drawing.Drawing2D.DashStyle.Dot);
        context.DrawLine(pen, 0, yPosition, xPosition, yPosition);
        context.DrawString(level.Label, new RenderFont("Arial", FontSize), level.Color, xPosition, yPosition);
      }
    }

    private void DrawText(int bar, string text, Color textColor, Color backgroundColor, bool overridePosition = false, bool swapPosition = false)
    {
      var candle = GetCandle(bar);
      var tick = ChartInfo.PriceChartContainer.Step;
      decimal location;

      bool isBullish = candle.Close > candle.Open;

      if (text == "▼")
      {
        location = candle.High + (tick * TextOffset);
      }
      else if (text == "▲")
      {
        location = candle.Low - (tick * (TextOffset * 2));
      }
      else if (swapPosition)
      {
        location = isBullish ? candle.Low - (tick * (TextOffset * 2)) : candle.High + (tick * TextOffset);
      }
      else
      {
        location = (isBullish || overridePosition) ? candle.High + (tick * TextOffset) : candle.Low - (tick * TextOffset);
      }

      AddText($"Text_{bar}", text, true, bar, location, textColor, backgroundColor, FontSize, DrawingText.TextAlign.Center);
    }
    #endregion

    #region CALCULATION
    protected override void OnCalculate(int bar, decimal value)
    {
      if (bar == 0)
      {
        ResetIndicator();
        return;
      }

      if (bar < 6) return;

      if (!_isInitialized)
      {
        LoadPriceLevels();
        _isInitialized = true;
      }

      var candle = GetCandle(bar);
      value = candle.Close;

      // Calculate technical indicators
      _bb.Calculate(bar, value);
      _rsi.Calculate(bar, value);
      //_atr.Calculate(bar, value);

      // Pattern detection
      DetectLineBounces(bar);
      DetectEngulfingPatterns(bar);
      DetectVolumeImbalances(bar);
      DetectReversalPatterns(bar);
      DetectTrampolinePattern(bar);

      // Handle alerts
      ProcessAlerts(bar);
    }

    private void ResetIndicator()
    {
      DataSeries.ForEach(x => x.Clear());
      HorizontalLinesTillTouch.Clear();
      _lineTouches.Clear();
      _lastBarCounted = false;
      _futureSoundAlert = 0;
      _previousSoundAlert = 0;
    }
    #endregion

    #region PATTERN DETECTION
    private void DetectLineBounces(int bar)
    {
      var candle = GetCandle(bar);
      var p1 = GetCandle(bar - 1);
      var p2 = GetCandle(bar - 2);

      bool c0G = candle.Close > candle.Open;
      bool c0R = candle.Close < candle.Open;
      bool c1G = p1.Close > p1.Open;
      bool c1R = p1.Close < p1.Open;
      bool c2G = p2.Close > p2.Open;
      bool c2R = p2.Close < p2.Open;

      var c0Body = Math.Abs(candle.Close - candle.Open);
      var c1Body = Math.Abs(p1.Close - p1.Open);

      foreach (var level in _priceLevels)
      {
        if (string.IsNullOrEmpty(level.Label)) continue;

        // Green double wick, engulfing
        if (c0G && candle.Low < level.Price && candle.Open > level.Price &&
            c1R && p1.Low < level.Price && p1.Close > level.Price && c0Body > c1Body &&
            c2R && p2.Close > p1.Close)
        {
          _paintBars[bar] = MColor.FromRgb(255, 204, 0);
          _futureSoundAlert = LINE_WICK_ENG;
        }
        // Red double wick, engulfing
        else if (c0R && candle.High > level.Price && candle.Open < level.Price &&
                 c1G && p1.High > level.Price && p1.Close < level.Price && c0Body > c1Body &&
                 c2G && p2.Close < p1.Close)
        {
          _paintBars[bar] = MColor.FromRgb(255, 204, 0);
          _futureSoundAlert = LINE_WICK_ENG;
        }
        // Green double touch, engulfing
        else if (c0G && candle.High > level.Price && candle.Low < level.Price &&
                 c1R && p1.Low < level.Price && c0Body > c1Body)
        {
          _paintBars[bar] = MColor.FromRgb(255, 204, 0);
          _futureSoundAlert = LINE_TOUCH_ENG;
        }
        // Red double touch, engulfing
        else if (c0R && candle.Low < level.Price && candle.High > level.Price &&
                 c1G && p1.High > level.Price && p1.Low < level.Price && c0Body > c1Body)
        {
          _paintBars[bar] = MColor.FromRgb(255, 204, 0);
          _futureSoundAlert = LINE_TOUCH_ENG;
        }
      }
    }

    private void DetectReversalPatterns(int bar)
    {
      var candle = GetCandle(bar);
      var p1 = GetCandle(bar - 1);
      var p2 = GetCandle(bar - 2);

      bool c0G = candle.Close > candle.Open;
      bool c0R = candle.Close < candle.Open;

      // Instant reversal, next bar after wick
      if (c0G && _lastWickBar == bar - 1 && _lastWickPrice < candle.Close && _lastWickPrice < candle.Open)
        _futureSoundAlert = VOL_IMB_REVERSE;

      if (c0R && _lastWickBar == bar - 1 && _lastWickPrice > candle.Close && _lastWickPrice > candle.Open)
        _futureSoundAlert = VOL_IMB_REVERSE;

      // Two or three candle reversal after wick
      if (c0R && (_lastWickBar == bar - 1 || _lastWickBar == bar - 2 || _lastWickBar == bar - 3) &&
          ((p1.Close > _lastWickPrice && p1.Open > _lastWickPrice) ||
           (p2.Close > _lastWickPrice && p2.Open > _lastWickPrice) ||
           (candle.Close > _lastWickPrice && candle.Open > _lastWickPrice)))
        _futureSoundAlert = VOL_IMB_REVERSE;

      if (c0G && (_lastWickBar == bar - 1 || _lastWickBar == bar - 2 || _lastWickBar == bar - 3) &&
          ((p1.Close < _lastWickPrice && p1.Open < _lastWickPrice) ||
           (p2.Close < _lastWickPrice && p2.Open < _lastWickPrice) ||
           (candle.Close < _lastWickPrice && candle.Open < _lastWickPrice)))
        _futureSoundAlert = VOL_IMB_REVERSE;

      // Two or three candle reversal after touch
      if (c0R && (_lastTouchBar == bar - 1 || _lastTouchBar == bar - 2 || _lastTouchBar == bar - 3) &&
          ((p1.Close > _lastTouchPrice && p1.Open > _lastTouchPrice) ||
           (p2.Close > _lastTouchPrice && p2.Open > _lastTouchPrice) ||
           (candle.Close > _lastTouchPrice && candle.Open > _lastTouchPrice)))
        _futureSoundAlert = VOL_IMB_REVERSE;

      if (c0G && (_lastTouchBar == bar - 1 || _lastTouchBar == bar - 2 || _lastTouchBar == bar - 3) &&
          ((p1.Close < _lastTouchPrice && p1.Open < _lastTouchPrice) ||
           (p2.Close < _lastTouchPrice && p2.Open < _lastTouchPrice) ||
           (candle.Close < _lastTouchPrice && candle.Open < _lastTouchPrice)))
        _futureSoundAlert = VOL_IMB_REVERSE;
    }

    private void DetectEngulfingPatterns(int bar)
    {
      var candle = GetCandle(bar);
      var p1 = GetCandle(bar - 1);
      var tick = ChartInfo.PriceChartContainer.Step;

      var bbTop = ((ValueDataSeries)_bb.DataSeries[1])[bar];
      var bbBottom = ((ValueDataSeries)_bb.DataSeries[2])[bar];
      var prevBbTop = ((ValueDataSeries)_bb.DataSeries[1])[bar - 1];
      var prevBbBottom = ((ValueDataSeries)_bb.DataSeries[2])[bar - 1];

      bool c0R = candle.Close < candle.Open;
      bool c0G = candle.Close > candle.Open;
      bool c1G = p1.Close > p1.Open;
      bool c1R = p1.Close < p1.Open;

      var c0Body = Math.Abs(candle.Close - candle.Open);
      var c1Body = Math.Abs(p1.Close - p1.Open);

      // Engulfing candle off Bollinger Band
      if (candle.High > bbTop && p1.High > prevBbTop && c0Body > c1Body && c0R && c1G)
      {
        _paintBars[bar] = MColor.FromRgb(255, 255, 255);
        _negSeries[bar - 1] = candle.High + (tick * 2);
        _futureSoundAlert = ENG_BB;
      }
      else if (candle.Low < bbBottom && p1.Low < prevBbBottom && c0Body > c1Body && c0G && c1R)
      {
        _paintBars[bar] = MColor.FromRgb(255, 255, 255);
        _posSeries[bar - 1] = candle.Low - (tick * 2);
        _futureSoundAlert = ENG_BB;
      }
    }

    private void DetectVolumeImbalances(int bar)
    {
      var candle = GetCandle(bar);
      var p1 = GetCandle(bar - 1);
      var tick = ChartInfo.PriceChartContainer.Step;

      bool c0G = candle.Close > candle.Open;
      bool c0R = candle.Close < candle.Open;
      bool c1G = p1.Close > p1.Open;
      bool c1R = p1.Close < p1.Open;

      var bbTop = ((ValueDataSeries)_bb.DataSeries[1])[bar - 1];
      var bbBottom = ((ValueDataSeries)_bb.DataSeries[2])[bar - 1];

      // Regular volume imbalance
      if (c0G && c1G && candle.Open > p1.Close)
      {
        if (!_lineTouches.Contains(bar))
        {
          _lineTouches.Add(bar);
          var pen = new Pen(new SolidBrush(Color.FromArgb(255, 156, 227, 255)))
          { Width = LineWidth, DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };
          HorizontalLinesTillTouch.Add(new LineTillTouch(bar, p1.Close, pen));

          if (_lineTouches.Contains(bar - 1))
            _futureSoundAlert = DOUBLE_VOL_IMB_DROP_G;

          DrawText(bar, "▲", Color.Yellow, Color.Transparent, false, true);
        }
      }
      else if (c0R && c1R && candle.Open < p1.Close)
      {
        if (!_lineTouches.Contains(bar))
        {
          _lineTouches.Add(bar);
          var pen = new Pen(new SolidBrush(Color.FromArgb(255, 156, 227, 255)))
          { Width = LineWidth, DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };
          HorizontalLinesTillTouch.Add(new LineTillTouch(bar, p1.Close, pen));

          if (_lineTouches.Contains(bar - 1))
            _futureSoundAlert = DOUBLE_VOL_IMB_DROP_R;

          DrawText(bar, "▼", Color.Yellow, Color.Transparent, false, true);
        }
      }

      // Volume imbalance plus BB
      if (c0G && c1G && candle.Open > p1.Close && p1.Low > bbBottom)
      {
        _futureSoundAlert = VOL_IMB_DROP_BB_G;
      }
      else if (c0R && c1R && candle.Open < p1.Close && p1.High > bbTop)
      {
        _futureSoundAlert = VOL_IMB_DROP_BB_R;
      }

      // Process volume imbalance lines for touches/wicks
      var linesToRemove = new List<LineTillTouch>();

      foreach (var line in HorizontalLinesTillTouch)
      {
        int distance = line.SecondBar - line.FirstBar;

        // Check if line is wicked through
        if (line.Finished && line.SecondBar == bar && distance > MinimumBars &&
            candle.High > line.SecondPrice && candle.Close < line.SecondPrice && candle.Open < line.SecondPrice)
        {
          _lastWickBar = bar;
          _lastWickPrice = line.SecondPrice;
          _futureSoundAlert = c0G ? VOL_IMB_WICK_G : VOL_IMB_WICK_R;
          _paintBars[bar] = MColor.FromRgb(255, 204, 1);
          linesToRemove.Add(line);
          break;
        }
        // Check if line is touched
        else if (line.Finished && line.SecondBar == bar && distance > MinimumBars)
        {
          _lastTouchBar = bar;
          _lastTouchPrice = line.SecondPrice;
          _futureSoundAlert = c0G ? VOL_IMB_TOUCH_G : VOL_IMB_TOUCH_R;
          _paintBars[bar] = MColor.FromRgb(255, 204, 1);
          linesToRemove.Add(line);
          break;
        }
        // Remove lines that are too close (less than minimum bars)
        else if (line.Finished && distance < MinimumBars)
        {
          linesToRemove.Add(line);
          break;
        }
      }

      // Remove processed lines
      foreach (var line in linesToRemove)
      {
        HorizontalLinesTillTouch.Remove(line);
      }
    }

    private void DetectTrampolinePattern(int bar)
    {
      var candle = GetCandle(bar);
      var p1 = GetCandle(bar - 1);
      var p2 = GetCandle(bar - 2);
      var tick = ChartInfo.PriceChartContainer.Step;

      var rsi = ((ValueDataSeries)_rsi.DataSeries[0])[bar];
      var rsi1 = ((ValueDataSeries)_rsi.DataSeries[0])[bar - 1];
      var rsi2 = ((ValueDataSeries)_rsi.DataSeries[0])[bar - 2];
      var bbTop = ((ValueDataSeries)_bb.DataSeries[1])[bar];
      var bbBottom = ((ValueDataSeries)_bb.DataSeries[2])[bar];

      bool c0R = candle.Close < candle.Open;
      bool c0G = candle.Close > candle.Open;
      bool c1R = p1.Close < p1.Open;
      bool c1G = p1.Close > p1.Open;
      bool c2G = p2.Close > p2.Open;
      bool c2R = p2.Close < p2.Open;

      if (c0R && c1R && candle.Close < p1.Close && (rsi >= 70 || rsi1 >= 70 || rsi2 >= 70) &&
          c2G && p2.High >= (bbTop - (tick * 30)))
      {
        DrawText(bar, "TR", Color.Black, Color.Lime, false, true);
        _futureSoundAlert = TRAMPOLINE;
      }

      if (c0G && c1G && candle.Close > p1.Close && (rsi < 25 || rsi1 < 25 || rsi2 < 25) &&
          c2R && p2.Low <= (bbBottom + (tick * 30)))
      {
        DrawText(bar, "TR", Color.Black, Color.Lime, false, true);
        _futureSoundAlert = TRAMPOLINE;
      }
    }
    #endregion

    #region ALERTS
    private void ProcessAlerts(int bar)
    {
      if (_lastBar != bar)
      {
        if (_lastBarCounted && _previousSoundAlert != _futureSoundAlert && _futureSoundAlert != 0)
        {
          var candle = GetCandle(bar);
          var atr = ((ValueDataSeries)_atr.DataSeries[0])[bar];
          var instrument = InstrumentInfo.Instrument;
          var message = GetAlertMessage(_futureSoundAlert, instrument, candle.Close);

          if (CaptureScreenshots)
          {
            _lastScreenshotPath = CaptureChartScreenshot(instrument);
          }

          if (SendDiscordAlerts && !string.IsNullOrEmpty(DiscordWebhookUrl))
          {
            Task.Run(() => SendDiscordAlert(message));
          }

          if (UseAlertSounds)
          {
            Task.Run(() => PlayAlertSound(_futureSoundAlert));
          }

          _previousSoundAlert = _futureSoundAlert;
          _futureSoundAlert = 0;
        }
        _lastBar = bar;
      }
      else
      {
        if (!_lastBarCounted)
          _lastBarCounted = true;
      }
    }

    private string GetAlertMessage(int alertType, string instrument, decimal price)
    {
      var baseMessage = $"{instrument} at {price}";

      return alertType switch
      {
        LINE_WICK_ENG or LINE_TOUCH_ENG => $"Line Touch/Wick Engulfing on {baseMessage}",
        DOUBLE_VOL_IMB_DROP_G => $"BUY SIGNAL on {baseMessage}",
        DOUBLE_VOL_IMB_DROP_R => $"SELL SIGNAL on {baseMessage}",
        VOL_IMB_DROP_BB_G => $"BB with Gap Green on {baseMessage}",
        VOL_IMB_DROP_BB_R => $"BB with Gap Red on {baseMessage}",
        VOL_IMB_TOUCH_G => $"Volume Imbalance Filled (Green) on {baseMessage}",
        VOL_IMB_TOUCH_R => $"Volume Imbalance Filled (Red) on {baseMessage}",
        VOL_IMB_WICK_G => $"Volume Imbalance Wicked (Green) on {baseMessage}",
        VOL_IMB_WICK_R => $"Volume Imbalance Wicked (Red) on {baseMessage}",
        VOL_IMB_REVERSE => $"Volume Imbalance Reversal on {baseMessage}",
        TRAMPOLINE => $"TRAMPOLINE on {baseMessage}",
        ENG_BB => $"ENG OFF BB on {baseMessage}",
        _ => $"Alert on {baseMessage}"
      };
    }

    private static Bitmap CaptureRegion(int x, int y, int width, int height)
    {
      var bitmap = new Bitmap(width, height);
      using (var graphics = Graphics.FromImage(bitmap))
        graphics.CopyFromScreen(x, y, 0, 0, new Size(width, height));
      return bitmap;
    }

    private static void CaptureRegionToFile(int x, int y, int width, int height, string filePath, ImageFormat format = null)
    {
      if (format == null)
        format = ImageFormat.Png;

      using (var bitmap = CaptureRegion(x, y, width, height))
        bitmap.Save(filePath, format);
    }

    private string CaptureChartScreenshot(string instrument)
    {
      try
      {
        var filename = $"{instrument}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var filepath = Path.Combine(ScreenshotDirectory, filename);

        // Use configurable coordinates per instrument
        int x = 100, y = 100, width = ChartWidth, height = ChartHeight;

        switch (instrument)
        {
          case var i when i.Contains("ES"):
            x = EsChartX; y = EsChartY;
            break;
          case var i when i.Contains("CL"):
            x = ClChartX; y = ClChartY;
            break;
          case var i when i.Contains("NQ"):
            x = NqChartX; y = NqChartY;
            width = 1000; height = 830; // NQ has different dimensions
            break;
          case var i when i.Contains("YM"):
            x = YmChartX; y = YmChartY;
            width = 1010; height = 830; // YM has different dimensions
            break;
        }

        CaptureRegionToFile(x, y, width, height, filepath);
        return filepath;
      }
      catch (Exception ex)
      {
        AddErrorLog($"Screenshot capture failed: {ex.Message}");
        return string.Empty;
      }
    }

    private async Task SendDiscordAlert(string message)
    {
      await _webhookSemaphore.WaitAsync();
      try
      {
        using var form = new MultipartFormDataContent();

        var payload = new
        {
          content = message,
          username = "VolImb Alert"
        };

        var jsonPayload = JsonSerializer.Serialize(payload);
        form.Add(new StringContent(jsonPayload, Encoding.UTF8, "application/json"), "payload_json");

        if (!string.IsNullOrEmpty(_lastScreenshotPath) && File.Exists(_lastScreenshotPath))
        {
          var fileContent = new ByteArrayContent(File.ReadAllBytes(_lastScreenshotPath));
          fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
          form.Add(fileContent, "file", Path.GetFileName(_lastScreenshotPath));
        }

        var response = await _httpClient.PostAsync(DiscordWebhookUrl, form);

        if (!response.IsSuccessStatusCode)
        {
          var error = await response.Content.ReadAsStringAsync();
          AddErrorLog($"Discord webhook failed: {response.StatusCode} - {error}");
        }
      }
      catch (Exception ex)
      {
        AddErrorLog($"Discord alert error: {ex.Message}");
      }
      finally
      {
        _webhookSemaphore.Release();
      }
    }

    private async Task PlayAlertSound(int alertType)
    {
      try
      {
        var soundFile = GetSoundFileName(alertType);
        var soundPath = Path.Combine(SoundDirectory, soundFile);

        if (File.Exists(soundPath))
        {
          _soundPlayer.SoundLocation = soundPath;
          _soundPlayer.Play();
        }
      }
      catch (Exception ex)
      {
        AddErrorLog($"Sound playback error: {ex.Message}");
      }
    }

    private void AddInfoLog(String sal)
    {

    }

    private void AddErrorLog(String sal)
    {

    }

    private string GetSoundFileName(int alertType)
    {
      return alertType switch
      {
        LINE_WICK_ENG or LINE_TOUCH_ENG => "linetouch.wav",
        DOUBLE_VOL_IMB_DROP_G or DOUBLE_VOL_IMB_DROP_R => "buy.wav",
        VOL_IMB_DROP_BB_G or VOL_IMB_DROP_BB_R or ENG_BB => "engulf.wav",
        VOL_IMB_TOUCH_G or VOL_IMB_TOUCH_R => "fill.wav",
        VOL_IMB_WICK_G or VOL_IMB_WICK_R => "wick.wav",
        VOL_IMB_REVERSE => "reverse.wav",
        TRAMPOLINE => "trampoline.wav",
        _ => "alert.wav"
      };
    }
    #endregion
  }
}