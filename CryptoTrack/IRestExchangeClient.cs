using CryptoPriceTracker.Domain.Types;

namespace CryptoPriceTracker.Domain;

public interface IRestExchangeClient
{
    Task<decimal> GetPriceAsync(string firstCoin, string secondCoin);
    ExchangeType GetExchangeType();
}