using CryptoPriceTracker.Domain;
using CryptoPriceTracker.Domain.Types;
using Kucoin.Net.Clients;

namespace CryptoPriceTracker.Infrastructure.ExchangeClients.Rest;

public sealed class KucoinClient : IRestExchangeClient
{
    private readonly KucoinRestClient _client = new();

    public async Task<decimal> GetPriceAsync(string firstCoin, string secondCoin)
    {
        var pair = firstCoin + "-" + secondCoin;
        var result = await _client.SpotApi.ExchangeData.GetTickerAsync(pair);
        var lastPrice = result.Data.LastPrice;
        if (lastPrice == null) return 0;

        return result.Success is false ? decimal.Zero : (decimal)lastPrice;
    }
    
    public ExchangeType GetExchangeType()
    {
        return ExchangeType.Kucoin;
    }
}