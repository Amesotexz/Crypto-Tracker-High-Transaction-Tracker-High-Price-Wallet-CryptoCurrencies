using CryptoPriceTracker.Domain;
using CryptoPriceTracker.Infrastructure.ExchangeClients.Rest;
using CryptoPriceTracker.Infrastructure.ExchangeClients.Socket;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoPriceTracker.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        var serviceProvider = new ServiceCollection()
        .AddSingleton<IRestExchangeClient, BinanceClient>()
        .AddSingleton<IRestExchangeClient, BybitClient>()
        .AddSingleton<IRestExchangeClient, KucoinClient>()
        .AddSingleton<IRestExchangeClient, BitgetClient>()
        .AddSingleton<ISocketExchangeClient, BinanceWebSocketClient>()
        .AddSingleton<ISocketExchangeClient, BitgetWebSocketClient>()
        .AddSingleton<ISocketExchangeClient, BybitWebSocketClient>()
        .AddSingleton<ISocketExchangeClient, KucoinWebSocketClient>()
        .AddSingleton<Form1>()
        .BuildServiceProvider();

        Application.Run(serviceProvider.GetService<Form1>());
    }
}