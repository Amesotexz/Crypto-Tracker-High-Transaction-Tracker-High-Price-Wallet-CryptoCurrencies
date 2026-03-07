using Bitget.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoPriceTracker.Domain;
using CryptoPriceTracker.Domain.Types;
using CryptoPriceTracker.Infrastructure.Common;

namespace CryptoPriceTracker.Infrastructure.ExchangeClients.Socket;

public class BitgetWebSocketClient : ISocketExchangeClient
{
    private readonly BitgetSocketClient _socketClient;
    private readonly Dictionary<string, UpdateSubscription> _tradesSocket;

    public BitgetWebSocketClient()
    {
        _socketClient = new BitgetSocketClient();
        _tradesSocket = new Dictionary<string, UpdateSubscription>();
    }

    public event Action<decimal>? NewTrade;

    public ExchangeType GetExchangeType()
    {
        return ExchangeType.Bitget;
    }

    public async Task SubscribeTrades(string firstCoin, string secondCoin)
    {
        var pair = firstCoin + secondCoin;
        var tickerSubscriptionResult = await _socketClient.SpotApi.SubscribeToTickerUpdatesAsync(pair, (update) =>
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