#region Using declarations
using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

namespace NinjaTrader.NinjaScript.Indicators
{
	public class ChartData : Indicator
	{
		private string chartdata = @"D:\chartdata.csv";
		private ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Export chart data for price predictor";
				Name										= "ChartData";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
			}
			else if (State == State.Configure)
			{
				try
                {
                    if (File.Exists(chartdata))
                    {
                        File.Delete(chartdata);
                    }
                    using (StreamWriter writer = new StreamWriter(chartdata, false))
                    {
                        writer.WriteLine("Time,Open,High,Low,Close");
                    }
                }
                catch (Exception e)
                {
                    Print("Error initializing file: " + e.Message);
                }
			}
		}

		protected override void OnBarUpdate()
		{
			lockSlim.EnterWriteLock();
            try
            {
                using (StreamWriter writer = new StreamWriter(chartdata, true))
                {
                    writer.WriteLine($"{Time[0]},{Open[0]},{High[0]},{Low[0]},{Close[0]}");
                    writer.Flush();
                }
            }
            catch (Exception e)
            {
                Print("Error writing to file: " + e.Message);
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
		}
	}
}



#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private ChartData[] cacheChartData;
		public ChartData ChartData()
		{
			return ChartData(Input);
		}

		public ChartData ChartData(ISeries<double> input)
		{
			if (cacheChartData != null)
				for (int idx = 0; idx < cacheChartData.Length; idx++)
					if (cacheChartData[idx] != null &&  cacheChartData[idx].EqualsInput(input))
						return cacheChartData[idx];
			return CacheIndicator<ChartData>(new ChartData(), input, ref cacheChartData);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.ChartData ChartData()
		{
			return indicator.ChartData(Input);
		}

		public Indicators.ChartData ChartData(ISeries<double> input )
		{
			return indicator.ChartData(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.ChartData ChartData()
		{
			return indicator.ChartData(Input);
		}

		public Indicators.ChartData ChartData(ISeries<double> input )
		{
			return indicator.ChartData(input);
		}
	}
}

#endregion
