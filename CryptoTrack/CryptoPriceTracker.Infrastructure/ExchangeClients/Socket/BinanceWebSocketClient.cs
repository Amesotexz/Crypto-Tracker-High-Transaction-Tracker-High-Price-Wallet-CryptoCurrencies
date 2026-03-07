using Binance.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoPriceTracker.Domain;
using CryptoPriceTracker.Domain.Types;
using CryptoPriceTracker.Infrastructure.Common;

namespace CryptoPriceTracker.Infrastructure.ExchangeClients.Socket;

public class BinanceWebSocketClient : ISocketExchangeClient
{
    private readonly BinanceSocketClient _socketClient;
    private readonly Dictionary<string, UpdateSubscription> _tradesSocket;

    public BinanceWebSocketClient()
    {
        _socketClient = new BinanceSocketClient();
        _tradesSocket = new Dictionary<string, UpdateSubscription>();
    }

    public event Action<decimal>? NewTrade;

    public ExchangeType GetExchangeType()
    {
        return ExchangeType.Binance;
    }

    public async Task SubscribeTrades(string firstCoin, string secondCoin)
    {
        var pair = firstCoin + secondCoin;
        var tickerSubscriptionResult = await _socketClient.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(pair,
            (update) =>
            {
                var lastPrice = update.Data.LastPrice;
                NewTrade?.Invoke(lastPrice);
            });
        if (tickerSubscriptionResult.Success)
            _tradesSocket.Add(pair, tickerSubscriptionResult.Data);
    }

    public async Task UnsubscribeTrades(string firstCoin, string secondCoin)
    {
        await WebSocketHelper.Unsubscribe(firstCoin + secondCoin, _tradesSocket);
    }
}