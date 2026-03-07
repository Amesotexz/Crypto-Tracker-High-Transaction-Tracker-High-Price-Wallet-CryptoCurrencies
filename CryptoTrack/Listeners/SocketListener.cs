using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using CryptoTracker.Core.Entities;
using CryptoTracker.Core.Enums;
using CryptoTracker.Core.Services;
using CryptoTracker.WinFormsUI.Common;
using CryptoTracker.WinFormsUI.Extensions;
using CryptoTracker.WinFormsUI.Models;

namespace CryptoTracker.WinFormsUI.Listeners;

internal class SocketListener
{
	private readonly List<UpdateSubscription> _subscriptions;
	private readonly BindingList<TickerModel> _tickerModels;
	private readonly ITickerService[] _services;
	private readonly ILogger _logger;

	private TradingPair _pair;
	
	public SocketListener(ITickerService[] services, BindingList<TickerModel> tickerModels, ILogger logger)
	{
		_services = services;
		_tickerModels = tickerModels;
		_logger = logger;
		_subscriptions = new(_services.Length);
	}
	
	public async Task StartAsync(TradingPair pair)
	{
		// We will just rely on the saved subscriptions to indicate if the client is already running
		if (_subscriptions.Count != 0) return;
		
		_pair = pair;
		_logger.LogMessage("Starting Web-Sockets...");
		
		var requests = _services
			.Select(s => (s.Exchange, s.SubscribeToTickerUpdatesAsync(_pair, UpdateHandler, CancellationToken.None)))
			.ToArray();
		
		foreach (var (exchange, task) in requests)
		{
			var result = await task;
			
			if (!result.GetResultOrError(out var sub, out var error))
			{
				_logger.LogMessage($"Web-Socket '{exchange}' has failed to start: {error.Message}.");
				continue;
			}
			
			// subscribe to events
			
			_subscriptions.Add(sub);
			_logger.LogMessage($"Web-Socket '{exchange}' is successfully started");
		}
	}
	
	public async Task StopAsync()
	{
		if (_subscriptions.Count == 0) return;
		
		var tasks = _subscriptions.Select(s => s.CloseAsync()).ToArray();
		await Task.WhenAll(tasks);
		
		_subscriptions.Clear();
		_logger.LogMessage("Web-Socket client is stopped");
	}
	
	public async Task RestartAsync(TradingPair pair)
	{
		await StopAsync();
		await StartAsync(pair);
	}
	
	private void UpdateHandler(TickerData ticker) => _tickerModels.UpdateModel(ticker);
}
