using Bybit.Net.Clients;
using CryptoPriceTracker.Domain;
using CryptoPriceTracker.Domain.Types;

namespace CryptoPriceTracker.Infrastructure.ExchangeClients.Rest;
 
public sealed class BybitClient : IRestExchangeClient
{
    private readonly BybitRestClient _client = new();
    
    public async Task<decimal> GetPriceAsync(string firstCoin, string secondCoin)
    {
        var pair = firstCoin + secondCoin;
        var result = await _client.V5Api.ExchangeData.GetSpotTickersAsync(pair);
        var lastPrice = result.Data.List.First().LastPrice;
        
        return result.Success is false ? decimal.Zero : lastPrice;
    }
    
    public ExchangeType GetExchangeType()
    {
        return ExchangeType.Bybit;
    }
}