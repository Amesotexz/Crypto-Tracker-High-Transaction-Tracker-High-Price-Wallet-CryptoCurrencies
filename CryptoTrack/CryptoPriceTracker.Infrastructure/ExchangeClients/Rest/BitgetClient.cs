using CryptoPriceTracker.Domain.Types;
using Bitget.Net.Clients;
using CryptoPriceTracker.Domain;

namespace CryptoPriceTracker.Infrastructure.ExchangeClients.Rest;

public sealed class BitgetClient : IRestExchangeClient
{
    private readonly BitgetRestClient _client = new();
    
    public async Task<decimal> GetPriceAsync(string firstCoin, string secondCoin)
    {
        var pair = firstCoin + secondCoin + "_SPBL";
        var result = await _client.SpotApi.ExchangeData.GetTickerAsync(pair);
        var lastPrice = result.Data.ClosePrice;
        
        return result.Success is false ? decimal.Zero : lastPrice;
    }
    
    public ExchangeType GetExchangeType()
    {
        return ExchangeType.Bitget;
    }
}