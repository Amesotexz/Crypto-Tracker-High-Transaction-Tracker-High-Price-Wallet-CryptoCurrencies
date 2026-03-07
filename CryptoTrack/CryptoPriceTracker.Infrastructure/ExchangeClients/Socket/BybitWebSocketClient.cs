using Bitget.Net.Clients;
using Bybit.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoPriceTracker.Domain;
using CryptoPriceTracker.Domain.Types;
using CryptoPriceTracker.Infrastructure.Common;

namespace CryptoPriceTracker.Infrastructure.ExchangeClients.Socket;

public class BybitWebSocketClient : ISocketExchangeClient
{
    private readonly BybitSocketClient _socketClient;
    private readonly Dictionary<string, UpdateSubscription> _tradesSocket;

    public BybitWebSocketClient()
    {
        _socketClient = new BybitSocketClient();
        _tradesSocket = new Dictionary<string, UpdateSubscription>();
    }

    public event Action<decimal>? NewTrade;

    public ExchangeType GetExchangeType()
    {
        return ExchangeType.Bybit;
    }

    public async Task SubscribeTrades(string firstCoin, string secondCoin)
    {
        var pair = firstCoin + secondCoin;
        var tickerSubscriptionResult = await _socketClient.V5SpotApi.SubscribeToTickerUpdatesAsync(pair, (update) =>
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