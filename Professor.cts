{
  "Value": {
    "$type": "OFT.Platform.Settings.Charting.ChartSettings, OFT.Platform",
    "ScaleByLowerValue": false,
    "ChartTraderIsEnabled": false,
    "SelectedAccount": "",
    "UseLocalVolumes": false,
    "TIF": 0,
    "TemplateType": "snapshot",
    "IsContinious": false,
    "PanelsIsHidden": true,
    "DrawingObjectses": [],
    "EndDate": "2000-01-01T00:00:00",
    "Template": {
      "TemplateType": "template",
      "ScaleByLowerValue": false,
      "ChartScale": 1,
      "Panels": [
        {
          "IsNew": true,
          "SerializedIndicators": [
            "{\r\n  \"Type\": \"ATAS.Indicators.Technical.FairValueGap, ATAS.Indicators.Technical, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n  \"Settings\": \"{\\r\\n  \\\"HigherTimeframe\\\": \\\"hourly\\\",\\r\\n  \\\"MidpointTouch\\\": false,\\r\\n  \\\"HideOlds\\\": true,\\r\\n  \\\"Transparency\\\": 1,\\r\\n  \\\"ShowCurrentTF\\\": true,\\r\\n  \\\"BullishColorCurrentTF\\\": \\\"94, 34, 83, 143\\\",\\r\\n  \\\"BearishColorCurrentTF\\\": \\\"110, 34, 83, 143\\\",\\r\\n  \\\"ShowHigherTF\\\": false,\\r\\n  \\\"BullishColorHigherTF\\\": \\\"128, 128, 0\\\",\\r\\n  \\\"BearishColorHigherTF\\\": \\\"156, 39, 176\\\",\\r\\n  \\\"MidPointWidth\\\": 1,\\r\\n  \\\"MidPointColor\\\": \\\"120, 123, 134\\\",\\r\\n  \\\"ShowLabel\\\": false,\\r\\n  \\\"LabelSize\\\": 8,\\r\\n  \\\"LabelColor\\\": \\\"120, 123, 134\\\",\\r\\n  \\\"LabelOffsetX\\\": 0,\\r\\n  \\\"LabelOffsetY\\\": 10,\\r\\n  \\\"FullScreenMode\\\": false,\\r\\n  \\\"DenyToChangePanel\\\": true,\\r\\n  \\\"ShowDescription\\\": true,\\r\\n  \\\"Name\\\": \\\"Fair Value Gap\\\",\\r\\n  \\\"Panel\\\": \\\"Chart\\\",\\r\\n  \\\"IsVerticalIndicator\\\": false,\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"Locked\\\": false,\\r\\n  \\\"AllowedInteraction\\\": true\\r\\n}\",\r\n  \"SourceType\": \"bars\",\r\n  \"SourceSeriesId\": 0,\r\n  \"DataSeries\": [\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FFFF5252\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"FairValueGapDefaultDataSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Fair Value Gap\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    }\r\n  ],\r\n  \"LineSeries\": [],\r\n  \"ShowDescription\": true\r\n}"
          ],
          "Height": 60,
          "Collapsed": false
        },
        {
          "IsNew": true,
          "SerializedIndicators": [
            "{\r\n  \"Type\": \"ATAS.Indicators.Technical.ParabolicSAR, ATAS.Indicators.Technical, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n  \"Settings\": \"{\\r\\n  \\\"AccelStart\\\": 0.02,\\r\\n  \\\"AccelStep\\\": 0.02,\\r\\n  \\\"AccelMax\\\": 0.2,\\r\\n  \\\"FullScreenMode\\\": false,\\r\\n  \\\"DenyToChangePanel\\\": false,\\r\\n  \\\"ShowDescription\\\": true,\\r\\n  \\\"Name\\\": \\\"Parabolic SAR\\\",\\r\\n  \\\"Panel\\\": \\\"Chart\\\",\\r\\n  \\\"IsVerticalIndicator\\\": false,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"Locked\\\": false,\\r\\n  \\\"AllowedInteraction\\\": false\\r\\n}\",\r\n  \"SourceType\": \"bars\",\r\n  \"SourceSeriesId\": 0,\r\n  \"DataSeries\": [\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"dots\\\",\\r\\n  \\\"Color\\\": \\\"#FF74A6E2\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": false,\\r\\n  \\\"ShowCurrentValue\\\": false,\\r\\n  \\\"ScaleIt\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"ParabolicSARDefaultDataSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Parabolic SAR\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    }\r\n  ],\r\n  \"LineSeries\": [],\r\n  \"ShowDescription\": true\r\n}"
          ],
          "Height": 60,
          "Collapsed": false
        },
        {
          "IsNew": true,
          "SerializedIndicators": [
            "{\r\n  \"Type\": \"ATAS.Indicators.Technical.Pivots, ATAS.Indicators.Technical, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n  \"Settings\": \"{\\r\\n  \\\"ThirdFormula\\\": \\\"highLow\\\",\\r\\n  \\\"RenderPeriodsFilter\\\": {\\r\\n    \\\"EnabledVisible\\\": true,\\r\\n    \\\"Enabled\\\": false,\\r\\n    \\\"Value\\\": 3\\r\\n  },\\r\\n  \\\"PivotRange\\\": \\\"daily\\\",\\r\\n  \\\"UseCustomSession\\\": false,\\r\\n  \\\"SessionBegin\\\": \\\"00:00:00\\\",\\r\\n  \\\"SessionEnd\\\": \\\"23:59:59\\\",\\r\\n  \\\"ShowText\\\": true,\\r\\n  \\\"FontSize\\\": 9,\\r\\n  \\\"Location\\\": \\\"right\\\",\\r\\n  \\\"FullScreenMode\\\": false,\\r\\n  \\\"DenyToChangePanel\\\": true,\\r\\n  \\\"ShowDescription\\\": true,\\r\\n  \\\"Name\\\": \\\"Pivots\\\",\\r\\n  \\\"Panel\\\": \\\"Chart\\\",\\r\\n  \\\"IsVerticalIndicator\\\": false,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"Locked\\\": false,\\r\\n  \\\"AllowedInteraction\\\": false\\r\\n}\",\r\n  \"SourceType\": \"bars\",\r\n  \"SourceSeriesId\": 0,\r\n  \"DataSeries\": [\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#85DAA520\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"PpSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"PP\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"BaseLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#B4FFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"S1Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"S1\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"SupportLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#AAFFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"S2Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"S2\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"SupportLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#B2FFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"S3Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"S3\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"SupportLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#B7FFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"R1Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"R1\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"ResistanceLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#B4FFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"R2Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"R2\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"ResistanceLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#BAFFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"R3Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"R3\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"ResistanceLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#B2FFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"M1Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"M1\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"MidLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#B8FFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"M2Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"M2\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"MidLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#8EFFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"M3Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"M3\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"MidLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#B1FFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"dash\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"M4Series\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"M4\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"MidLineSettingsDescription\\\"\\r\\n}\"\r\n    }\r\n  ],\r\n  \"LineSeries\": [],\r\n  \"ShowDescription\": true\r\n}"
          ],
          "Height": 60,
          "Collapsed": false
        },
        {
          "IsNew": true,
          "SerializedIndicators": [
            "{\r\n  \"Type\": \"ATAS.Indicators.Technical.KAMA, ATAS.Indicators.Technical, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n  \"Settings\": \"{\\r\\n  \\\"EfficiencyRatioPeriod\\\": 9,\\r\\n  \\\"ShortPeriod\\\": 2,\\r\\n  \\\"LongPeriod\\\": 109,\\r\\n  \\\"FullScreenMode\\\": false,\\r\\n  \\\"DenyToChangePanel\\\": true,\\r\\n  \\\"ShowDescription\\\": true,\\r\\n  \\\"Name\\\": \\\"Kaufman Adaptive Moving Average\\\",\\r\\n  \\\"Panel\\\": \\\"Chart\\\",\\r\\n  \\\"IsVerticalIndicator\\\": false,\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"Locked\\\": false,\\r\\n  \\\"AllowedInteraction\\\": true\\r\\n}\",\r\n  \"SourceType\": \"bars\",\r\n  \"SourceSeriesId\": 0,\r\n  \"DataSeries\": [\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#FFFFA500\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 3,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"KAMADefaultDataSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Kaufman Adaptive Moving Average\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    }\r\n  ],\r\n  \"LineSeries\": [],\r\n  \"ShowDescription\": true\r\n}"
          ],
          "Height": 60,
          "Collapsed": false
        },
        {
          "IsNew": true,
          "SerializedIndicators": [
            "{\r\n  \"Type\": \"ATAS.Indicators.Technical.EMA, ATAS.Indicators.Technical, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n  \"Settings\": \"{\\r\\n  \\\"Period\\\": 200,\\r\\n  \\\"ColoredDirection\\\": false,\\r\\n  \\\"BullishColor\\\": \\\"#FF4CAF50\\\",\\r\\n  \\\"BearishColor\\\": \\\"#FFFF5252\\\",\\r\\n  \\\"UseAlerts\\\": false,\\r\\n  \\\"RepeatAlert\\\": false,\\r\\n  \\\"AlertSensitivity\\\": 1,\\r\\n  \\\"AlertFile\\\": \\\"alert1\\\",\\r\\n  \\\"FontColor\\\": \\\"#FFFFFFFF\\\",\\r\\n  \\\"BackgroundColor\\\": \\\"#FF696969\\\",\\r\\n  \\\"FullScreenMode\\\": false,\\r\\n  \\\"DenyToChangePanel\\\": false,\\r\\n  \\\"ShowDescription\\\": true,\\r\\n  \\\"Name\\\": \\\"EMA\\\",\\r\\n  \\\"Panel\\\": \\\"Chart\\\",\\r\\n  \\\"IsVerticalIndicator\\\": false,\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"Locked\\\": false,\\r\\n  \\\"AllowedInteraction\\\": true\\r\\n}\",\r\n  \"SourceType\": \"close\",\r\n  \"SourceSeriesId\": 0,\r\n  \"DataSeries\": [\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#FFABA99E\\\",\\r\\n  \\\"ValuesColor\\\": \\\"171, 169, 158\\\",\\r\\n  \\\"Width\\\": 4,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"RenderSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"EMA\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    }\r\n  ],\r\n  \"LineSeries\": [],\r\n  \"ShowDescription\": true\r\n}"
          ],
          "Height": 60,
          "Collapsed": false
        },
        {
          "IsNew": true,
          "SerializedIndicators": [
            "{\r\n  \"Type\": \"ATAS.Indicators.Technical.BollingerBands, ATAS.Indicators.Technical, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n  \"Settings\": \"{\\r\\n  \\\"Period\\\": 20,\\r\\n  \\\"Width\\\": 2.0,\\r\\n  \\\"Shift\\\": 0,\\r\\n  \\\"UseAlertsTop\\\": false,\\r\\n  \\\"RepeatAlertTop\\\": false,\\r\\n  \\\"AlertSensitivityTop\\\": 1,\\r\\n  \\\"AlertFileTop\\\": \\\"alert1\\\",\\r\\n  \\\"FontColorTop\\\": \\\"#FFFFFFFF\\\",\\r\\n  \\\"BackgroundColorTop\\\": \\\"#FF696969\\\",\\r\\n  \\\"UseAlertsMid\\\": false,\\r\\n  \\\"RepeatAlertMid\\\": false,\\r\\n  \\\"AlertSensitivityMid\\\": 1,\\r\\n  \\\"AlertFileMid\\\": \\\"alert1\\\",\\r\\n  \\\"FontColorMid\\\": \\\"#FFFFFFFF\\\",\\r\\n  \\\"BackgroundColorMid\\\": \\\"#FF696969\\\",\\r\\n  \\\"UseAlertsBot\\\": false,\\r\\n  \\\"RepeatAlertBot\\\": false,\\r\\n  \\\"AlertSensitivityBot\\\": 1,\\r\\n  \\\"AlertFileBot\\\": \\\"alert1\\\",\\r\\n  \\\"FontColorBot\\\": \\\"#FFFFFFFF\\\",\\r\\n  \\\"BackgroundColorBot\\\": \\\"#FF696969\\\",\\r\\n  \\\"FullScreenMode\\\": false,\\r\\n  \\\"DenyToChangePanel\\\": false,\\r\\n  \\\"ShowDescription\\\": true,\\r\\n  \\\"Name\\\": \\\"Bollinger Bands\\\",\\r\\n  \\\"Panel\\\": \\\"Chart\\\",\\r\\n  \\\"IsVerticalIndicator\\\": false,\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"Locked\\\": false,\\r\\n  \\\"AllowedInteraction\\\": true\\r\\n}\",\r\n  \"SourceType\": \"close\",\r\n  \"SourceSeriesId\": 0,\r\n  \"DataSeries\": [\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FF4CAF50\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"SmaSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Bollinger Bands\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"MidChannelSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#FFE18030\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 2,\\r\\n  \\\"LineDashStyle\\\": \\\"dot\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": true,\\r\\n  \\\"Id\\\": \\\"UpSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Up\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"TopChannelSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#FFF79646\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 2,\\r\\n  \\\"LineDashStyle\\\": \\\"dot\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": true,\\r\\n  \\\"Id\\\": \\\"DownSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Down\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"BottomChannelSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#3E6495ED\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Band\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Background Neutral\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"ChannelNeutralAreaSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#3E6495ED\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"ReserveBand\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Neutral Reserve\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#5A00FF00\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"UpBand\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Background Up\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"ChannelPositiveAreaSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#5A00FF00\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"UpReserveBand\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Up Reserve\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#5AFF0000\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"DownBand\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Background Down\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"ChannelNegativeAreaSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#5AFF0000\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"DownReserveBand\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Down Reserve\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    }\r\n  ],\r\n  \"LineSeries\": [],\r\n  \"ShowDescription\": true\r\n}"
          ],
          "Height": 60,
          "Collapsed": false
        },
        {
          "IsNew": true,
          "SerializedIndicators": [
            "{\r\n  \"Type\": \"ATAS.Indicators.Technical.VWAP, ATAS.Indicators.Technical, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n  \"Settings\": \"{\\r\\n  \\\"AllowCustomStartPoint\\\": false,\\r\\n  \\\"StartKeyFilter\\\": 49,\\r\\n  \\\"StartKey\\\": \\\"f\\\",\\r\\n  \\\"DeleteKeyFilter\\\": 50,\\r\\n  \\\"DeleteKey\\\": \\\"g\\\",\\r\\n  \\\"SavePointFilter\\\": {\\r\\n    \\\"EnabledVisible\\\": false,\\r\\n    \\\"Enabled\\\": false,\\r\\n    \\\"Value\\\": false\\r\\n  },\\r\\n  \\\"SavePoint\\\": false,\\r\\n  \\\"StartDate\\\": \\\"0001-01-01T00:00:00\\\",\\r\\n  \\\"ResetOnSessionFilter\\\": {\\r\\n    \\\"EnabledVisible\\\": false,\\r\\n    \\\"Enabled\\\": false,\\r\\n    \\\"Value\\\": false\\r\\n  },\\r\\n  \\\"ResetOnSession\\\": false,\\r\\n  \\\"ColoredDirection\\\": false,\\r\\n  \\\"BullishColor\\\": \\\"#FF2196F3\\\",\\r\\n  \\\"BearishColor\\\": \\\"#FFB22222\\\",\\r\\n  \\\"StartBar\\\": 0,\\r\\n  \\\"Type\\\": \\\"daily\\\",\\r\\n  \\\"TWAPMode\\\": \\\"vwap\\\",\\r\\n  \\\"VolumeMode\\\": \\\"total\\\",\\r\\n  \\\"Period\\\": 300,\\r\\n  \\\"StDev\\\": 1.0,\\r\\n  \\\"StDev1\\\": 2.0,\\r\\n  \\\"StDev2\\\": 3.0,\\r\\n  \\\"CustomSessionStart\\\": \\\"00:00:00\\\",\\r\\n  \\\"CustomSessionEnd\\\": \\\"23:59:59\\\",\\r\\n  \\\"Days\\\": 20,\\r\\n  \\\"ShowFirstPeriod\\\": false,\\r\\n  \\\"VWAPOnly\\\": true,\\r\\n  \\\"FullScreenMode\\\": false,\\r\\n  \\\"DenyToChangePanel\\\": false,\\r\\n  \\\"ShowDescription\\\": true,\\r\\n  \\\"Name\\\": \\\"VWAP/TWAP\\\",\\r\\n  \\\"Panel\\\": \\\"Chart\\\",\\r\\n  \\\"IsVerticalIndicator\\\": false,\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"Locked\\\": false,\\r\\n  \\\"AllowedInteraction\\\": true\\r\\n}\",\r\n  \"SourceType\": \"close\",\r\n  \"SourceSeriesId\": 0,\r\n  \"DataSeries\": [\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#FF74A6E2\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 4,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"VwapTwap\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"VWAP|TWAP\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"BaseLineSettingsDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FF00BCD4\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Lower2\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Lower Deviation 3\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FF00BCD4\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Upper2\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Upper Deviation 3\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FF00BCD4\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Lower1\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Lower Deviation 2\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FF00BCD4\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Upper1\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Upper Deviation 2\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FF00BCD4\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Lower\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Lower Deviation 1\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FF00BCD4\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Upper\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Upper Deviation 1\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FF4CAF50\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 5,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"PrevPosValueSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Previous upper value\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"hide\\\",\\r\\n  \\\"Color\\\": \\\"#FFCD5C5C\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 5,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"PrevNegValueSeries\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Previous lower value\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#99C30101\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Upper2Background\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Upper fill 2\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#99C30101\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Upper2BackgroundRes\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Upper Fill 2 res\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#99C30101\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"UpperBackground\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Upper fill\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#99C30101\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"UpperBackgroundRes\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Upper Fill res\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#99808080\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"MidUpBackground\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Middle fill up\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#99808080\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"MidUpBackgroundRes\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Middle Fill Up Res\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#99808080\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"MidDownBackground\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Middle fill down\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#99808080\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"MidDownBackgroundRes\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Middle Fill Down res\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#9900FF00\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"LowerBackground\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Lower fill\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#9900FF00\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"LowerBackgroundRes\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Lower Fill res\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#9900FF00\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Lower2Background\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Lower fill 2\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#9900FF00\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Lower2BackgroundRes\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Lower Fill 2 res\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    }\r\n  ],\r\n  \"LineSeries\": [],\r\n  \"ShowDescription\": true\r\n}"
          ],
          "Height": 60,
          "Collapsed": false
        },
        {
          "IsNew": true,
          "SerializedIndicators": [
            "{\r\n  \"Type\": \"ATAS.Indicators.Technical.DeltaIntensity, DeltaIntensity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\r\n  \"Settings\": \"{\\r\\n  \\\"Bigtrades_Threshold\\\": 25000,\\r\\n  \\\"FullScreenMode\\\": false,\\r\\n  \\\"DenyToChangePanel\\\": false,\\r\\n  \\\"ShowDescription\\\": true,\\r\\n  \\\"Name\\\": \\\"Delta Intensity\\\",\\r\\n  \\\"Panel\\\": \\\"1\\\",\\r\\n  \\\"IsVerticalIndicator\\\": false,\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"Locked\\\": false,\\r\\n  \\\"AllowedInteraction\\\": true\\r\\n}\",\r\n  \"SourceType\": \"bars\",\r\n  \"SourceSeriesId\": 0,\r\n  \"DataSeries\": [\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"histogram\\\",\\r\\n  \\\"Color\\\": \\\"#FF006400\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Positive Delta\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Positive Delta\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"histogram\\\",\\r\\n  \\\"Color\\\": \\\"#FFFF8C00\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Negative Delta\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Negative Delta\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"histogram\\\",\\r\\n  \\\"Color\\\": \\\"#FF32CD32\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Volume/Second Positive\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Volume/Second Positive\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"histogram\\\",\\r\\n  \\\"Color\\\": \\\"#FFFF0000\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Volume/Second Negative\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Volume/Second Negative\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    }\r\n  ],\r\n  \"LineSeries\": [],\r\n  \"ShowDescription\": true\r\n}"
          ],
          "Height": 202,
          "Collapsed": false
        },
        {
          "IsNew": true,
          "SerializedIndicators": [
            "{\r\n  \"Type\": \"ATAS.Indicators.Technical.BuySell, ATASBuySell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\r\n  \"Settings\": \"{\\r\\n  \\\"ShowRegularBuySell\\\": true,\\r\\n  \\\"ShowBigArrow\\\": false,\\r\\n  \\\"canColor\\\": 0,\\r\\n  \\\"ShowRevPattern\\\": true,\\r\\n  \\\"ShowBrooks\\\": true,\\r\\n  \\\"WaddaSensitivity\\\": 120,\\r\\n  \\\"MACDSensitivity\\\": 70,\\r\\n  \\\"UseAlerts\\\": false,\\r\\n  \\\"AlertFile\\\": \\\"alert1\\\",\\r\\n  \\\"ShowLines\\\": false,\\r\\n  \\\"ShowTripleSupertrend\\\": false,\\r\\n  \\\"Show_9_21_EMA_Cross\\\": false,\\r\\n  \\\"Show_Squeeze_Relax\\\": true,\\r\\n  \\\"Use_VolumeImbalances\\\": true,\\r\\n  \\\"Use_Cloud\\\": false,\\r\\n  \\\"ShowWaddahLine\\\": false,\\r\\n  \\\"Use_Tramp\\\": true,\\r\\n  \\\"ShowEvil\\\": true,\\r\\n  \\\"ShowStar\\\": false,\\r\\n  \\\"Show_News\\\": true,\\r\\n  \\\"Show_PNL\\\": false,\\r\\n  \\\"NewsFont\\\": 10,\\r\\n  \\\"Use_Waddah_Explosion\\\": true,\\r\\n  \\\"Use_Awesome\\\": false,\\r\\n  \\\"Use_PSAR\\\": true,\\r\\n  \\\"Use_Squeeze_Momentum\\\": false,\\r\\n  \\\"Use_MACD\\\": true,\\r\\n  \\\"Use_HMA\\\": true,\\r\\n  \\\"Use_SuperTrend\\\": false,\\r\\n  \\\"Use_T3\\\": false,\\r\\n  \\\"Use_Fisher_Transform\\\": true,\\r\\n  \\\"Min_ADX\\\": 0,\\r\\n  \\\"ShowClusters\\\": false,\\r\\n  \\\"ClusterRatio\\\": 3,\\r\\n  \\\"MinBid\\\": 9,\\r\\n  \\\"MinAsk\\\": 9,\\r\\n  \\\"Use_Custom_EMA\\\": false,\\r\\n  \\\"Custom_EMA_Period\\\": 21,\\r\\n  \\\"Use_KAMA\\\": false,\\r\\n  \\\"Custom_KAMA_Period\\\": 9,\\r\\n  \\\"TextFont\\\": 10,\\r\\n  \\\"Offset\\\": 1,\\r\\n  \\\"FullScreenMode\\\": false,\\r\\n  \\\"DenyToChangePanel\\\": true,\\r\\n  \\\"ShowDescription\\\": true,\\r\\n  \\\"Name\\\": \\\"TraderOracle Buy/Sell\\\",\\r\\n  \\\"Panel\\\": \\\"Chart\\\",\\r\\n  \\\"IsVerticalIndicator\\\": false,\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"Locked\\\": false,\\r\\n  \\\"AllowedInteraction\\\": true\\r\\n}\",\r\n  \"SourceType\": \"bars\",\r\n  \"SourceSeriesId\": 0,\r\n  \"DataSeries\": [\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"dots\\\",\\r\\n  \\\"Color\\\": \\\"#FF00FF00\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 4,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Regular Buy Signal\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Regular Buy Signal\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"dots\\\",\\r\\n  \\\"Color\\\": \\\"#FFFF6830\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 4,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Regular Sell Signal\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Regular Sell Signal\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"upArrow\\\",\\r\\n  \\\"Color\\\": \\\"#FFFFFFFF\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Vol Imbalance Buy\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Vol Imbalance Buy\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"downArrow\\\",\\r\\n  \\\"Color\\\": \\\"#FFFFFFFF\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 1,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Vol Imbalance Sell\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Vol Imbalance Sell\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"downArrow\\\",\\r\\n  \\\"Color\\\": \\\"#FFFFB6C1\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 2,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"MACD/PSAR Sell Arrow\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"MACD/PSAR Sell Arrow\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"upArrow\\\",\\r\\n  \\\"Color\\\": \\\"#FF90EE90\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 2,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"MACD/PSAR Buy Arrow\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"MACD/PSAR Buy Arrow\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"block\\\",\\r\\n  \\\"Color\\\": \\\"#FF00FF00\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 4,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"9 21 cross\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"9 21 cross\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"dots\\\",\\r\\n  \\\"Color\\\": \\\"#FFFFFF00\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 3,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Squeeze Relaxer\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Squeeze Relaxer\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.PaintbarsDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"HideChart\\\": false,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Paint bars\\\",\\r\\n  \\\"Type\\\": \\\"paintBars\\\",\\r\\n  \\\"Name\\\": \\\"Paint bars\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"square\\\",\\r\\n  \\\"Color\\\": \\\"#FFFF5252\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 2,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Down SuperTrend\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Down SuperTrend\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"square\\\",\\r\\n  \\\"Color\\\": \\\"#FF2196F3\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 2,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": false,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Up SuperTrend\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"Up SuperTrend\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#4900FF00\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Up Cloud\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Up Cloud\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.RangeDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"RangeColor\\\": \\\"#49FF0000\\\",\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"Visible\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": false,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"Down Cloud\\\",\\r\\n  \\\"Type\\\": \\\"band\\\",\\r\\n  \\\"Name\\\": \\\"Down Cloud\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#B41E72FA\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 4,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"VWAP\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"VWAP\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#FFA5A6A4\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 4,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"EMA 200\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"EMA 200\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    },\r\n    {\r\n      \"Type\": \"ATAS.Indicators.ValueDataSeries, ATAS.Indicators, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7\",\r\n      \"Settings\": \"{\\r\\n  \\\"Digits\\\": 4,\\r\\n  \\\"StringFormat\\\": \\\"{0:0.####}\\\",\\r\\n  \\\"ShowOnlyNonZeroLabels\\\": false,\\r\\n  \\\"VisualType\\\": \\\"line\\\",\\r\\n  \\\"Color\\\": \\\"#B4FCBA03\\\",\\r\\n  \\\"ValuesColor\\\": \\\"White\\\",\\r\\n  \\\"Width\\\": 3,\\r\\n  \\\"LineDashStyle\\\": \\\"solid\\\",\\r\\n  \\\"ShowZeroValue\\\": true,\\r\\n  \\\"ShowCurrentValue\\\": true,\\r\\n  \\\"ScaleIt\\\": true,\\r\\n  \\\"DrawAbovePrice\\\": true,\\r\\n  \\\"IgnoredByAlerts\\\": false,\\r\\n  \\\"Id\\\": \\\"KAMA 9\\\",\\r\\n  \\\"Type\\\": \\\"value\\\",\\r\\n  \\\"Name\\\": \\\"KAMA 9\\\",\\r\\n  \\\"DescriptionKey\\\": \\\"VisualizationDescription\\\"\\r\\n}\"\r\n    }\r\n  ],\r\n  \"LineSeries\": [],\r\n  \"ShowDescription\": true\r\n}"
          ],
          "Height": 60,
          "Collapsed": false
        }
      ],
      "Indicators": [],
      "clusterSettings": {
        "Caption": "Cluster Settings",
        "Volumes": "#FF2962FF",
        "Bid": "#FFF71427",
        "Ask": "#FF90EE90",
        "ClusterBorderWidth": 1,
        "Foreground": "#57FFFFFF",
        "AutoSize": false,
        "FontSize": 9.0,
        "CutLongText": true,
        "MinimumClusterWidthToShowText": 10,
        "ValueAreaColor": "#67446EA2",
        "ShowValueArea": true,
        "ImbalanceBid": "#FFFF0000",
        "ImbalanceAsk": "#FF90EE90",
        "MinimumImbalanceDifference": 0,
        "IgnoreZeroValues": false,
        "ImbalanceVolumeFilter": 0,
        "ImbalanceRate": 300,
        "ShowDirectionMarker": true,
        "DirectionMakerWidth": 0,
        "ClusterBorderPen": {
          "LineDashStyle": "solid",
          "Width": 1,
          "Color": "#FF2A2E39"
        },
        "BorderColorByDirection": true,
        "SecondClusters": {
          "Caption": "",
          "Enabled": false,
          "ClustersContentModeFilter": {
            "EnumType": "OFT.Controls.Chart.enums.ClustersContentModes, OFT.Controls, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7",
            "Value": "volume",
            "Enabled": false,
            "EnabledVisible": false
          },
          "ClustersContentMode": "volume",
          "ClustersModeFilter": {
            "EnumType": "OFT.Controls.Chart.enums.ClustersVisualModes, OFT.Controls, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7",
            "Value": "volumeHistogram",
            "Enabled": false,
            "EnabledVisible": false
          },
          "ClustersMode": "volumeHistogram",
          "UseBorderOfEachPriceLevelFilter": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": false
          },
          "UseBorderOfEachPriceLevel": false,
          "ColorSchemeFilter": {
            "EnumType": "OFT.Controls.Chart.enums.ClustersColorSchemes, OFT.Controls, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7",
            "Value": "volumeProportion",
            "Enabled": false,
            "EnabledVisible": false
          },
          "ColorScheme": "volumeProportion",
          "ClusterBGFilter": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": "#00FFFFFF"
          },
          "ClusterBG": "#00FFFFFF",
          "VolumeColor": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": "#FF2962FF"
          },
          "BidColor": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": "#FFF71427"
          },
          "AskColor": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": "#FF2962FF"
          },
          "HeatmapType": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": 0
          },
          "UpperCutOff": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": 20
          },
          "Contrast": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": 0
          },
          "MaxVolTypeFilter": {
            "EnumType": "Advanced_Time_And_Sales.ClusterSettings+MaxVolSelectionType, OFT.Controls, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7",
            "Value": "volume",
            "Enabled": false,
            "EnabledVisible": false
          },
          "MaxVolType": "volume",
          "MaxVolColorFilter": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": "#00000000"
          },
          "MaxVolColor": "#00000000",
          "MaxVolSelectionWidthFilter": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": 1
          },
          "MaxVolSelectionWidth": 1,
          "MaxVolTextColorFilter": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": "#FFD1D4DC"
          },
          "MaxVolTextColor": "#FFD1D4DC",
          "MaxLevelBoldFilter": {
            "EnabledVisible": false,
            "Enabled": false,
            "Value": true
          },
          "MaxLevelBold": true
        },
        "BorderType": "candle",
        "BorderColorByDirectionFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": true
        },
        "ClusterBorderPenFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": {
            "Color": "#FF2A2E39",
            "LineDashStyle": 0,
            "Width": 1
          }
        },
        "ShowDirectionMarkerFilter": {
          "EnabledVisible": true,
          "Enabled": true,
          "Value": 0
        },
        "Showtext": true,
        "ForegroundFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": "#57FFFFFF"
        },
        "AutoSizeFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": false
        },
        "FontSizeFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": 9.0
        },
        "Divider": 1.0,
        "CutLongTextFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": true
        },
        "MinimumClusterWidthToShowTextFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": 10
        },
        "ValueAreaColorFilter": {
          "EnabledVisible": true,
          "Enabled": true,
          "Value": "#67446EA2"
        },
        "ProportionalMode": "visibleRegion",
        "ProportionalHistogram": true,
        "ProportionByAllBars": false,
        "GradientRate": 100,
        "CustomProportionValue": {
          "EnabledVisible": false,
          "Enabled": false,
          "Value": 0.0
        },
        "CustomproportionVolume": 0.0,
        "UpperCutOffGradient": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": 0
        },
        "EnableBidAskImbalance": true,
        "ImbalanceBidFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": "#FFFF0000"
        },
        "ImbalanceAskFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": "#FF90EE90"
        },
        "ImbalanceRateFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": 300
        },
        "ImbalanceVolumeFilterInt": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": 0
        },
        "MinimumImbalanceDifferenceFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": 0
        },
        "IgnoreZeroValuesFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": false
        },
        "BoldFontForImbalances": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": true
        },
        "CustomRowHeight": {
          "EnabledVisible": true,
          "Enabled": false,
          "Value": 16
        },
        "Filters": [],
        "ClustersContentModeFilter": {
          "EnumType": "OFT.Controls.Chart.enums.ClustersContentModes, OFT.Controls, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7",
          "Value": "bidAskCentered",
          "Enabled": true,
          "EnabledVisible": false
        },
        "ClustersContentMode": "bidAskCentered",
        "ClustersModeFilter": {
          "EnumType": "OFT.Controls.Chart.enums.ClustersVisualModes, OFT.Controls, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7",
          "Value": "bidAskHistogram",
          "Enabled": true,
          "EnabledVisible": false
        },
        "ClustersMode": "bidAskHistogram",
        "UseBorderOfEachPriceLevelFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": false
        },
        "UseBorderOfEachPriceLevel": false,
        "ColorSchemeFilter": {
          "EnumType": "OFT.Controls.Chart.enums.ClustersColorSchemes, OFT.Controls, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7",
          "Value": "delta",
          "Enabled": true,
          "EnabledVisible": false
        },
        "ColorScheme": "delta",
        "ClusterBGFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": "#00FFFFFF"
        },
        "ClusterBG": "#00FFFFFF",
        "VolumeColor": {
          "EnabledVisible": false,
          "Enabled": false,
          "Value": "#FF2962FF"
        },
        "BidColor": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": "#FFF71427"
        },
        "AskColor": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": "#FF90EE90"
        },
        "HeatmapType": {
          "EnabledVisible": false,
          "Enabled": false,
          "Value": 1
        },
        "UpperCutOff": {
          "EnabledVisible": false,
          "Enabled": false,
          "Value": 31
        },
        "Contrast": {
          "EnabledVisible": false,
          "Enabled": false,
          "Value": 11
        },
        "MaxVolTypeFilter": {
          "EnumType": "Advanced_Time_And_Sales.ClusterSettings+MaxVolSelectionType, OFT.Controls, Version=6.1.3.216, Culture=neutral, PublicKeyToken=330427d8594115c7",
          "Value": "volume",
          "Enabled": true,
          "EnabledVisible": false
        },
        "MaxVolType": "volume",
        "MaxVolColorFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": "#D7FFFF00"
        },
        "MaxVolColor": "#D7FFFF00",
        "MaxVolSelectionWidthFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": 1
        },
        "MaxVolSelectionWidth": 1,
        "MaxVolTextColorFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": "#FFFFFF00"
        },
        "MaxVolTextColor": "#FFFFFF00",
        "MaxLevelBoldFilter": {
          "EnabledVisible": false,
          "Enabled": true,
          "Value": true
        },
        "MaxLevelBold": true
      },
      "colorsettings": {
        "BuyCandlePricePen": {
          "Color": "13, 191, 13",
          "DashStyle": "dot",
          "Width": 1.0
        },
        "SellCandlePricePen": {
          "Color": "217, 13, 13",
          "DashStyle": "dot",
          "Width": 1.0
        },
        "BuyBarPricePen": {
          "Color": "8, 153, 129",
          "DashStyle": "dot",
          "Width": 1.0
        },
        "SellBarPricePen": {
          "Color": "242, 56, 90",
          "DashStyle": "dot",
          "Width": 1.0
        },
        "DojiBarPricePen": {
          "Color": "255, 255, 255",
          "DashStyle": "dot",
          "Width": 1.0
        },
        "LineChartPricePen": {
          "Color": "41, 98, 255",
          "DashStyle": "dot",
          "Width": 1.0
        },
        "MountainChartPricePen": {
          "Color": "71, 107, 150",
          "DashStyle": "dot",
          "Width": 1.0
        },
        "SolidCurrentPricePen": {
          "Color": "54, 58, 69",
          "DashStyle": "dot",
          "Width": 1.0
        },
        "Caption": "Chart Color Settings",
        "BackgroundBrushSettings": {
          "UseEndColor": false,
          "StartColor": "#FF000000",
          "EndColor": "#FF000000"
        },
        "BackGround": "#FF000000",
        "StateColor": "#FFABAEB8",
        "TextHistogramColor": "#FFB2B5BE",
        "LabelsBackgroundColor": "#00000000",
        "IndicatorsSeparator": {
          "LineDashStyle": "solid",
          "Width": 1,
          "Color": "#FF434651"
        },
        "CurrentPriceColor": {
          "EnabledVisible": true,
          "Enabled": false,
          "Value": "#FF363A45"
        },
        "PriceLinePen": {
          "LineDashStyle": "dash",
          "Width": 1,
          "Color": "#FFEF5350"
        },
        "PriceLineDashStyle": "dot",
        "PriceLineWidth": 1,
        "ExtendPriceLine": false,
        "CrosshairPen": {
          "LineDashStyle": "dash",
          "Width": 1,
          "Color": "#FF9598A1"
        },
        "AVersion": 1,
        "AxisCurrentBackColor": "#FF363A45",
        "AxisCurrentForeColor": "#FFF1F1F1",
        "GridPen": {
          "LineDashStyle": "solid",
          "Width": 1,
          "Color": "#FF21252F"
        },
        "ShowVHorizontalGrid": false,
        "CustomGridStep": 0,
        "ShowVericalGrid": false,
        "CustomVertGridStep": 0,
        "NewSessionPen": {
          "LineDashStyle": "dash",
          "Width": 1,
          "Color": "#FF4985E7"
        },
        "AxisColorFilter": {
          "EnabledVisible": true,
          "Enabled": false,
          "Value": "#FF151B26"
        },
        "AxisColor": "#FF151B26",
        "AxisTextColor": "#FFABAEB8",
        "FontSizeDecimal": 8.0,
        "TimeFormat": "hourMinSecMillisec",
        "HidePriceAxis": false,
        "DrawAxisBorders": false,
        "BuyColor": "#FF0DBF0D",
        "SellColor": "#FFD90D0D",
        "DrawCandleBorder": false,
        "CandleBorderColorAsBody": true,
        "CellBorderColor": "#00FFFFFF",
        "CandleBorderWidth": 1,
        "BarsWidth": 1,
        "DojiBarColor": "#FFFFFFFF",
        "DownBarColor": "#FFF2385A",
        "UpBarColor": "#FF089981",
        "ShowOpenOfBar": true,
        "ShowCloseOfBar": true,
        "ChartLinePen": {
          "LineDashStyle": "solid",
          "Width": 1,
          "Color": "#FF2962FF"
        },
        "MountainLinePen": {
          "LineDashStyle": "solid",
          "Width": 1,
          "Color": "#FF2962FF"
        },
        "MountainBrushSettings": {
          "UseEndColor": true,
          "StartColor": "#B62962FF",
          "EndColor": "#062962FF"
        },
        "UseSessionTime": false,
        "SessionStartFilter": {
          "EnabledVisible": false,
          "Enabled": false,
          "Value": "00:00:00"
        },
        "SessionStart": "00:00:00",
        "SessionEndFilter": {
          "EnabledVisible": false,
          "Enabled": false,
          "Value": "23:59:59"
        },
        "SessionEnd": "23:59:59",
        "PriceBorder": false,
        "ShowIndicatorsValuesOnMouseOver": false,
        "AutoTransFormCandles": false,
        "CandleSizeTotransformToClusters": 20,
        "HideClustersPanel": true,
        "AutoTransformHorizontalLines": true,
        "AutoTransformVerticalLines": true,
        "PriceOffset": 200,
        "ChartOffset": 30,
        "UseSmoothing": true,
        "MouseWheelBehavior": "zoom",
        "MinimizeValues": false,
        "DigitsAfterComma": 0,
        "KeepSelectedObjects": false,
        "ObjectsSelectionDrawingColor": "30, 83, 228",
        "ObjectsSelectionColor": "#FF1E53E4",
        "Sensitivity": 0,
        "UseCustomUpdateIntervals": false,
        "CustomFPS": {
          "EnabledVisible": false,
          "Enabled": false,
          "Value": {
            "Start": 10,
            "End": 50
          }
        },
        "IndicatorsListIsCollapsed": true,
        "VolumeValuesFormat": "{0:0.}",
        "RedrawInterval": 100,
        "NewSessionLineColor": "#FF4985E7",
        "Сl_crosshair": "#FF9598A1",
        "GridColor": "#FF21252F",
        "HistogramLinesColor": "#C8ADD8E6",
        "HistogramColor": "#FF808080",
        "ShowCumulativeValues": true,
        "BidColor": "#FFE84548",
        "AskColor": "#FF49D149",
        "ValueAreaColor": "#FFC8D8FF",
        "PocColor": "#FFC8D8FF",
        "HistogramTextColor": "#FF35212E",
        "ExtendPoc": false,
        "ExtendValueArea": false
      },
      "histogramInterval": "contract",
      "typeHistogram": "bidAsk",
      "ShowHistogram": false,
      "ShowingDigital": false,
      "UseOpacility": false,
      "ShowTxtMouse": true,
      "VisibleCrossHair": true,
      "ShowValueArea": true,
      "ShowPoc": true,
      "AlwaysOnTop": false,
      "BarsType": "candles",
      "ClusterWidth": 23.302926000050626119009075570,
      "TypeCluster": "volume",
      "RowHeight": 2.5864237611459227,
      "ChangeHeightmanualy": true,
      "PaintingMode": "drawCrossHair",
      "TradingSettings": {
        "PnLMode": "money",
        "Caption": "",
        "ShowText": true,
        "OneClickMode": false,
        "TradingMode": "autoDetection",
        "StopMode": "stop",
        "Slippage": 5,
        "ShowTrades": "visible",
        "Size": 6,
        "TradesColor": "#FF2962FF",
        "TradesSellColor": "#FF2962FF",
        "ShowOrders": "visible",
        "OrdersOffset": 115,
        "BuyOrdersColors": "#FF089981",
        "SellOrdersColor": "#FFF2385A",
        "LimitColors": "#FF089981",
        "StopColor": "#FFF2385A",
        "OrderstextColor": "#FFD1D4DC",
        "ShowPosition": "visible",
        "PositionOffset": 115,
        "PositionTextColors": "#FFF1F1F1",
        "PositionBackGround": "#FF151B26",
        "PositionPositive": "#FF089981",
        "PositionNegative": "#FFF2385A"
      },
      "ClusterVisualizationType": "bidAskLadder",
      "Version": 1,
      "UseAutoScale": true,
      "LayoutString": "<XtraSerializer version=\"1.0\" application=\"\">\r\n  <property name=\"#LayoutVersion\">1.3</property>\r\n  <property name=\"$BarManager\" iskey=\"true\" value=\"BarManager\">\r\n    <property name=\"RuntimeCustomizations\" iskey=\"true\" value=\"35\">\r\n      <property name=\"Item1\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">Visible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">ToolBar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">260335546</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item2\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.Column</property>\r\n        <property name=\"NewValue\" type=\"System.Int32\">0</property>\r\n        <property name=\"OldValue\" type=\"System.Int32\">1</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">DrawingObjectsToolbar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">260479625</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item3\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.IsFloating</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">DrawingObjectsToolbar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">260479640</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item4\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName7BFFD76487B951DEE54EC20B06A26A03</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338181187</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item5\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName3BBEB7B14F17E2EC0A4E6A9D24394151</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338198046</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item6\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName5CEEAAFFF7DBEE71FE3A5757FF646DD4</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338199375</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item7\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName4DC114DE6771699A6F121EB059414617</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338200312</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item8\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cNameAAD7AA824FE19C5E15CDAA86DB47C2A8</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338201328</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item9\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName22E3C45BE629D6FB07A308F6D1D89D46</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338202296</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item10\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName4B4EED3C57238F8EC8E749F527AA551C</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338203234</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item11\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cNameB5B4320C41C79BF301E748902AAF6699</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338204640</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item12\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cNameEABC43E0F0F3CA5CF766658136C9DF53</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338206171</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item13\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName35200AA0D3DF6DF6D6D6467499AA0B2D</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338208671</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item14\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cNameDCB84391CE977376E842381A5CF696C3</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338209781</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item15\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cNameCA5CA33C987891869141F15D4C9B8501</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338211031</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item16\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName741B3C02984CDF371CDE9B089FD19EA2</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338212312</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item17\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName37D8FB6EE1DEDAFF3A1BD15E0A232C7E</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338213484</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item18\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName463B47F2D175C1D9BA50FF7EB52EF3AC</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338214578</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item19\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cNameA3CD80A3E4A15DC72984D148F87142FB</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338216093</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item20\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName38323ECF322455035A2A482CB5AC44F4</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338217046</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item21\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName03DDB9BD221360C2A89E8AFF1CDF75A1</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338217890</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item22\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName4D29A3C38FD73AB13DB9AF5C85FDA045</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338218921</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item23\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cName52542CA05DE3E3901A1A4644929A439E</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338219859</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item24\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">IsVisible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">cNameEA7B867E33DC0575829E5F1ADB67725B</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338220796</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item25\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.Offset</property>\r\n        <property name=\"NewValue\" type=\"System.Double\">432.66666666666515</property>\r\n        <property name=\"OldValue\" type=\"System.Double\">0</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">DrawingObjectsToolbar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338225890</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item26\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.ContainerType</property>\r\n        <property name=\"NewValue\" type=\"System.String\">Floating</property>\r\n        <property name=\"OldValue\" type=\"DevExpress.Xpf.Bars.BarContainerType\">Left</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">DrawingObjectsToolbar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">338225953</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item27\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.FloatBarOffset</property>\r\n        <property name=\"NewValue\" type=\"System.String\">611.3333333333334,-201.33333333333334</property>\r\n        <property name=\"OldValue\" type=\"System.Windows.Point\">0,0</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">DrawingObjectsToolbar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">678657656</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item28\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.ContainerName</property>\r\n        <property name=\"NewValue\" type=\"System.String\" />\r\n        <property name=\"OldValue\" isnull=\"true\" />\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">ToolBar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">773408750</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item29\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.ContainerName</property>\r\n        <property name=\"NewValue\" type=\"System.String\" />\r\n        <property name=\"OldValue\" isnull=\"true\" />\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">ClusterBar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">773408750</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item30\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.ContainerName</property>\r\n        <property name=\"NewValue\" type=\"System.String\">PART_FloatingBarContainer</property>\r\n        <property name=\"OldValue\" isnull=\"true\" />\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">DrawingObjectsToolbar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">773409250</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item31\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">Visible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">false</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">ClusterBar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">45792671</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item32\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.ContainerName</property>\r\n        <property name=\"NewValue\" type=\"System.String\" />\r\n        <property name=\"OldValue\" type=\"System.String\" />\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">ToolBar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">45793281</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item33\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.ContainerName</property>\r\n        <property name=\"NewValue\" type=\"System.String\" />\r\n        <property name=\"OldValue\" type=\"System.String\" />\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">ClusterBar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">45793281</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item34\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">DockInfo.ContainerName</property>\r\n        <property name=\"NewValue\" type=\"System.String\">PART_FloatingBarContainer</property>\r\n        <property name=\"OldValue\" type=\"System.String\">PART_FloatingBarContainer</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">DrawingObjectsToolbar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">45906015</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n      <property name=\"Item35\" isnull=\"true\" iskey=\"true\">\r\n        <property name=\"PropertyName\">Visible</property>\r\n        <property name=\"NewValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"OldValue\" type=\"System.Boolean\">true</property>\r\n        <property name=\"ActOnHost\">false</property>\r\n        <property name=\"TargetName\">DrawingObjectsToolbar</property>\r\n        <property name=\"Overwrite\">true</property>\r\n        <property name=\"Timestamp\">45906062</property>\r\n        <property name=\"CustomizationType\">DevExpress.Xpf.Bars.RuntimePropertyCustomization</property>\r\n        <property name=\"AffectedTargets\" iskey=\"true\" value=\"0\" />\r\n      </property>\r\n    </property>\r\n  </property>\r\n  <property name=\"$BarManager$ChartLayoutControl\" iskey=\"true\" value=\"BarManager$ChartLayoutControl\" />\r\n</XtraSerializer>"
    },
    "TimeFrame": {
      "Period": "tick",
      "PeriodParameters": {
        "$type": "OFT.Controls.Chart.PeriodTimeFrame, OFT.Controls",
        "Period": 2000,
        "Label": "@value",
        "DefaultDaysToLoad": 20,
        "VisualName": "2000"
      },
      "LoadCustomDaysCount": false,
      "CustomDaysCount": 2,
      "LoadCustomEndDate": false,
      "CustomEndDate": "2024-03-29T11:15:33.55933-05:00"
    },
    "Canvas": {
      "Elements": []
    },
    "LinkedPanels": [],
    "Width": 23.302926000050626119009075570,
    "StrategySettings": [],
    "TraderSettings": {
      "SelectedTIF": "goodTillCancel",
      "SelectedRoute": "",
      "Volume": 1.0,
      "VolumeByCurrency": {},
      "IsPercentMode": false,
      "MarketOrderFlags": {},
      "LimitOrderFlags": {},
      "ConditionalMarketOrderFlags": {},
      "ConditionalLimitOrderFlags": {}
    },
    "IsVisibleDrawingObjectsBar": true
  },
  "IsSystem": false
}