using CryptoExchange.Net.Objects.Sockets;
using CryptoPriceTracker.Domain;
using CryptoPriceTracker.Domain.Types;
using CryptoPriceTracker.Infrastructure.Common;
using Kucoin.Net.Clients;

namespace CryptoPriceTracker.Infrastructure.ExchangeClients.Socket;

public class KucoinWebSocketClient : ISocketExchangeClient
{
    private readonly KucoinSocketClient _socketClient;
    private readonly Dictionary<string, UpdateSubscription> _tradesSocket;

    public KucoinWebSocketClient()
    {
        _socketClient = new KucoinSocketClient();
        _tradesSocket = new Dictionary<string, UpdateSubscription>();
    }

    public event Action<decimal>? NewTrade;

    public ExchangeType GetExchangeType()
    {
        return ExchangeType.Kucoin;
    }

    public async Task SubscribeTrades(string firstCoin, string secondCoin)
    {
        var pair = firstCoin + "-" + secondCoin;
        var tickerSubscriptionResult = await _socketClient.SpotApi.SubscribeToTickerUpdatesAsync(pair, (update) =>
        {
            var lastPrice = update.Data.LastPrice ?? decimal.Zero;
            NewTrade?.Invoke(lastPrice);
        });
        if (tickerSubscriptionResult.Success)
            _tradesSocket.Add(pair, tickerSubscriptionResult.Data);
    }

    public async Task UnsubscribeTrades(string firstCoin, string secondCoin)
    {
        var key = firstCoin + "-" + secondCoin;
        await WebSocketHelper.Unsubscribe(key, _tradesSocket);
    }
}