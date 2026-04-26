using Binance.Net.Clients;
using CryptoPriceTracker.Domain;
using CryptoPriceTracker.Domain.Types;

namespace CryptoPriceTracker.Infrastructure.ExchangeClients.Rest;

public sealed class BinanceClient : IRestExchangeClient
{
    private readonly BinanceRestClient _client = new();
    
    public async Task<decimal> GetPriceAsync(string firstCoin, string secondCoin)
    {
        var pair = firstCoin + secondCoin;
        var result = await _client.SpotApi.ExchangeData.GetTickerAsync(pair);
        var lastPrice = result.Data.LastPrice;
        
        return result.Success is false ? decimal.Zero : lastPrice;
    }
    
    public ExchangeType GetExchangeType()
    {
        return ExchangeType.Binance;
    }
}