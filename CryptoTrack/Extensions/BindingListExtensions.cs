using System;
using System.ComponentModel;
using System.Linq;
using CryptoExchange.Net;
using CryptoTracker.Core.Entities;
using CryptoTracker.WinFormsUI.Models;

namespace CryptoTracker.WinFormsUI.Extensions;

internal static class BindingListExtensions
{
	internal static void UpdateModel(this BindingList<TickerModel> models, TickerData ticker)
	{
		var model = models.First(m => m.Exchange.Equals(ticker.Exchange, StringComparison.Ordinal));
		var positiveChange = ticker.ChangePercentage24H > 0 ? "+" : "";
		
		model.Price = ticker.LastPrice.Normalize();
		model.Low24H = ticker.LowPrice24H.Normalize();
		model.High24H = ticker.HighPrice24H.Normalize();
		model.ChangePercentage24H = $"{positiveChange}{ticker.ChangePercentage24H.Normalize()}%";
	}
}