using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoTracker.Core.Enums;
using CryptoTracker.Core.Services;
using CryptoTracker.WinFormsUI.Common;
using CryptoTracker.WinFormsUI.Extensions;
using CryptoTracker.WinFormsUI.Models;

namespace CryptoTracker.WinFormsUI.Listeners;

internal class RestListener
{
	private readonly ITickerService[] _services;
	private readonly BindingList<TickerModel> _tickerModels;
	private readonly Timer _timer = new();
	private readonly ILogger _logger;
	
	private TradingPair _pair;
	
	public RestListener(int interval, ITickerService[] services, BindingList<TickerModel> tickerModels, ILogger logger)
	{
		_timer.Interval = interval;
		_timer.Tick += TimerHandler;
		
		_services = services;
		_tickerModels = tickerModels;
		_logger = logger;
	}
	
	public async Task StartAsync(TradingPair pair)
	{
		if (_timer.Enabled) return;
		_pair = pair;
		
		await TimerAsyncHandler();
		_timer.Start();
		_logger.LogMessage("Rest client is started");
	}
	
	public void Stop()
	{
		if (!_timer.Enabled) return;
		
		_timer.Stop();
		_logger.LogMessage("Rest client is stopped");
	}
	
	public Task RestartAsync(TradingPair pair)
	{
		Stop();
		return StartAsync(pair);
	}
	
	private async void TimerHandler(object? _, EventArgs __) => await TimerAsyncHandler();
	
	private async Task TimerAsyncHandler()
	{
		var requests = _services
			.Select(x => (x.Exchange, Task: x.GetTickerAsync(_pair)))
			.ToArray();
		
		await foreach (var task in Task.WhenEach(requests.Select(r => r.Task)))
		{
			var result = await task;
			
			if (result.GetResultOrError(out var ticker, out var error))
			{
				_tickerModels.UpdateModel(ticker);
				continue;
			}
			
			var failedExchange = requests.First(r => r.Task == task).Exchange;
			_logger.LogMessage($"Exchange '{failedExchange}' has failed: {error.Message}");
		}
	}
}