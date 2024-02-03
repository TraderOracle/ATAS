namespace ATAS_Defibrillator
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ATAS.Indicators;
    using ATAS.Indicators.Drawing;
    using OFT.Rendering.Context;
    using OFT.Rendering.Settings;
    using OFT.Rendering.Tools;
    using Color = System.Drawing.Color;

    [DisplayName("Defibrillator")]
    public class Defibrillator : Indicator
    {
        private float fFontSize = 9;
        private int _highBar;
        private int _lowBar;
        private decimal _highest = 0;
        private decimal _lowest = 0;

        private decimal A05 = 0;
        private decimal A113 = 0;
        private decimal A150 = 0;
        private decimal A1618 = 0;
        private decimal A200 = 0;
        private decimal A213 = 0;
        private decimal A250 = 0;
        private decimal A2618 = 0;

        private decimal nA113 = 0;
        private decimal nA150 = 0;
        private decimal nA1618 = 0;
        private decimal nA200 = 0;
        private decimal nA213 = 0;
        private decimal nA250 = 0;
        private decimal nA2618 = 0;

        private String _highS = "Euro High";
        private String _lowS = "Euro Low";

        private bool bShowEuro = false;
        private bool bShowOvernight = true;

        [Display(Name = "Line", GroupName = "Misc")]
        public PenSettings defibPen { get; set; } = new()
        {
            Color = DefaultColors.Red.Convert(),
            Width = 1,
            LineDashStyle = LineDashStyle.Dot
        };

        [Display(Name = "Font Size", GroupName = "Misc", Order = int.MaxValue)]
        [Range(1, 90)]
        public float TextFont
        {
            get => fFontSize; set { fFontSize = value; RecalculateValues(); }
        }

        [Display(GroupName = "Misc", Name = "Show Overnight Session", Description = "Show lines for overnight session (1700 - 800 CST")]
        public bool Show_Overnight { get => bShowOvernight; set { bShowOvernight = value; RecalculateValues(); } }

        [Display(GroupName = "Misc", Name = "Show Euro Session", Description = "Show lines for Euro session (first hour, 2am - 3am CST.  Credit: FighterOfEvil on Discord")]
        public bool Show_Euro { get => bShowEuro; set { bShowEuro = value; RecalculateValues(); } }

        private void DrawString(RenderContext context, string renderText, int yPrice, Color color)
        {
            var textSize = context.MeasureString(renderText, new RenderFont("Arial", fFontSize));
            context.DrawString(renderText, new RenderFont("Arial", fFontSize), color, Container.Region.Right - textSize.Width - 5, yPrice - textSize.Height);
        }

        protected override void OnRender(RenderContext context, DrawingLayouts layout)
        {
            if (ChartInfo is null)
                return;

            var xH = ChartInfo.PriceChartContainer.GetXByBar(_highBar, false);
            var yH = ChartInfo.PriceChartContainer.GetYByPrice(_highest, false);
            context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
            DrawString(context, _highS, yH, defibPen.RenderObject.Color);

            var xL = ChartInfo.PriceChartContainer.GetXByBar(_lowBar, false);
            var yL = ChartInfo.PriceChartContainer.GetYByPrice(_lowest, false);
            context.DrawLine(defibPen.RenderObject, xL, yL, Container.Region.Right, yL);
            DrawString(context, _lowS, yL, defibPen.RenderObject.Color);

            if (bShowOvernight)
            {
                yH = ChartInfo.PriceChartContainer.GetYByPrice(A05, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "0.5", yH, defibPen.RenderObject.Color);

                yH = ChartInfo.PriceChartContainer.GetYByPrice(A113, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "1.13", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(A150, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "1.5", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(A1618, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "1.618", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(A200, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "2", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(A213, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "2.13", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(A250, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "2.5", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(A2618, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "2.618", yH, defibPen.RenderObject.Color);

                yH = ChartInfo.PriceChartContainer.GetYByPrice(nA113, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "1.13", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(nA150, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "1.5", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(nA1618, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "1.618", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(nA200, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "2", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(nA213, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "2.13", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(nA250, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "2.5", yH, defibPen.RenderObject.Color);
                yH = ChartInfo.PriceChartContainer.GetYByPrice(nA2618, false);
                context.DrawLine(defibPen.RenderObject, xH, yH, Container.Region.Right, yH);
                DrawString(context, "2.618", yH, defibPen.RenderObject.Color);
            }
        }

        public Defibrillator() :
            base(true)
        {
            Panel = IndicatorDataProvider.CandlesPanel;
            DenyToChangePanel = true;
            EnableCustomDrawing = true;
            SubscribeToDrawingEvents(DrawingLayouts.Final);
        }

        private void MarkEuroSession(int bar)
        {
            _highS = "Euro High";
            _lowS = "Euro Low";
            var candle = GetCandle(bar);
            var diff = InstrumentInfo.TimeZone;
            var time = candle.Time.AddHours(diff);

            if (time.Minute >= 0 && time.Hour >= 2 && time.Hour <= 3)
            {
                if (candle.High > _highest)
                {
                    _highest = candle.High;
                    _highBar = bar;
                }
                if (candle.Low > _lowest)
                {
                    _lowest = candle.Low;
                    _lowBar = bar;
                }
            }
        }

        private void MarkOvernightSession(int bar)
        {
            _highS = "1";
            _lowS = "0";

            var candle = GetCandle(bar);
            var diff = InstrumentInfo.TimeZone;
            var time = candle.Time.AddHours(diff);

            if ((time.Minute >= 0 && time.Hour >= 17 && time.Hour <= 24) ||
                (time.Minute >= 0 && time.Hour >= 0 && time.Hour <= 8))
            {
                if (candle.High > _highest)
                {
                    _highest = candle.High;
                    _highBar = bar;
                }
                if (candle.Low < _lowest)
                {
                    _lowest = candle.Low;
                    _lowBar = bar;
                }
            }
        }

        protected override void OnCalculate(int bar, decimal value)
        {
            var candle = GetCandle(bar);

            if (IsNewSession(bar))
            {
                _highest = candle.High;
                _lowest = candle.Low;
            }

            if (bShowEuro)
                MarkEuroSession(bar);

            if (bShowOvernight)
                MarkOvernightSession(bar);

            var fibrange = _highest - _lowest;
            decimal r = Math.Abs(fibrange);
            A05 = _lowest + 0.5m * r;
            A113 = _lowest + 1.13m * r;
            A150 = _lowest + 1.5m * r;
            A1618 = _lowest + 1.618m * r;
            A200 = _lowest + 2.00m * r;
            A213 = _lowest + 2.13m * r;
            A250 = _lowest + 2.5m * r;
            A2618 = _lowest + 2.618m * r;

            nA113 = _highest - 1.13m * r;
            nA150 = _highest - 1.5m * r;
            nA1618 = _highest - 1.618m * r;
            nA200 = _highest - 2.00m * r;
            nA213 = _highest - 2.13m * r;
            nA250 = _highest - 2.5m * r;
            nA2618 = _highest - 2.618m * r;
        }

    }
}
