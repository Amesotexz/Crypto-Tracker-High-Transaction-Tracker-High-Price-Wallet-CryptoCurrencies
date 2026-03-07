using CryptoPriceTracker.Domain.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPriceTracker.Domain
{
    public interface ISocketExchangeClient
    {
        public event Action<decimal>? NewTrade;
        Task SubscribeTrades(string firstCoin, string secondCoin);
        Task UnsubscribeTrades(string firstCoin, string secondCoin);
        ExchangeType GetExchangeType();
    }
}
