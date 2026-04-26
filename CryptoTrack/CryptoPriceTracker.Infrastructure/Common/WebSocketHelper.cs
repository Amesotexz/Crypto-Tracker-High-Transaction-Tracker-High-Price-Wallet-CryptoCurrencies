using CryptoExchange.Net.Objects.Sockets;

namespace CryptoPriceTracker.Infrastructure.Common;

public static class WebSocketHelper
{
    public static async Task Unsubscribe(string key, Dictionary<string, UpdateSubscription> socketList)
    {
        var isSuc = socketList.TryGetValue(key, out var websocket);
        if(isSuc == false) return; 
        
        await websocket!.CloseAsync();
        socketList.Remove(key);
    }
}