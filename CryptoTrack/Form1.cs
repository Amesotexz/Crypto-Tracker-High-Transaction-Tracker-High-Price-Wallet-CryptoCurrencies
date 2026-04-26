using CryptoPriceTracker.Domain;
using CryptoPriceTracker.Domain.Types;

namespace CryptoPriceTracker.WinForms;

public partial class Form1 : Form
{
    private readonly System.Windows.Forms.Timer _timer;
    private bool _isSocketRunning = false;

    private readonly IEnumerable<IRestExchangeClient> _exchangeClients;
    private readonly IEnumerable<ISocketExchangeClient> _socketClients;

    public Form1(IEnumerable<IRestExchangeClient> exchangeClients, IEnumerable<ISocketExchangeClient> socketClients)
    {
        InitializeComponent();

        _exchangeClients = exchangeClients;
        _socketClients = socketClients;

        _timer = new System.Windows.Forms.Timer();
        _timer.Interval = 5000;
        _timer.Tick += Timer_Tick;

        comboBoxPairs.Items.Add("BTC-USDT");
        comboBoxPairs.Items.Add("ETH-USDT");
        comboBoxPairs.SelectedIndex = 0;

        comboBoxProtocols.Items.Add(ProtocolTypes.Rest.Value);
        comboBoxProtocols.Items.Add(ProtocolTypes.WebSocket.Value);
        comboBoxProtocols.SelectedIndex = 0;
    }

    private async void Timer_Tick(object sender, EventArgs e)
    {
        if (comboBoxProtocols.SelectedItem?.ToString() == ProtocolTypes.Rest.Value)
            await UpdatePricesByRest();
        if (comboBoxProtocols.SelectedItem?.ToString() == ProtocolTypes.WebSocket.Value)
            UpdatePricesByWebsocket();
    }

    private async Task UpdatePricesByRest()
    {
        try
        {
            var (first, second) = GetPair();
            foreach (var client in _exchangeClients)
            {
                var exchange = client.GetExchangeType();
                var price = await client.GetPriceAsync(first, second);
                SetExchangePrice(exchange, price);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error fetching prices: {ex.Message}");
        }
    }

    private void UpdatePricesByWebsocket()
    {
        if (_isSocketRunning) return;

        _isSocketRunning = true;
        var (first, second) = GetPair();

        foreach (var client in _socketClients)
        {
            client.SubscribeTrades(first, second);
            var exchange = client.GetExchangeType();
            client.NewTrade += price =>
            {
                SetExchangePrice(exchange, price);
                Task.Delay(_timer.Interval).Wait();
            };
        }
    }

    private void buttonStartStop_Click(object sender, EventArgs e)
    {
        if (_timer.Enabled)
        {
            _timer.Stop();
            buttonStartStop.Text = "Start";
            comboBoxPairs.Enabled = true;
            comboBoxProtocols.Enabled = true;
            if (_isSocketRunning)
                UnsubscribeTrades();
        }
        else
        {
            _timer.Start();
            buttonStartStop.Text = "Stop";
            comboBoxPairs.Enabled = false;
            comboBoxProtocols.Enabled = false;
        }
    }

    private void SetExchangePrice(ExchangeType exchange, decimal price)
    {
        switch (exchange)
        {
            case ExchangeType.Binance:
                labelBinance.Text = $"{price}";
                break;
            case ExchangeType.Bybit:
                labelBybit.Text = $"{price}";
                break;
            case ExchangeType.Kucoin:
                labelKucoin.Text = $"{price}";
                break;
            case ExchangeType.Bitget:
                labelBitget.Text = $"{price}";
                break;
        }
    }

    private void UnsubscribeTrades()
    {
        var (first, second) = GetPair();
            
        foreach (var item in _socketClients)
        {
            item.UnsubscribeTrades(first, second);
        }
        _isSocketRunning = false;
    }

    private (string, string) GetPair()
    {
        var pair = comboBoxPairs.SelectedItem?.ToString();
        var validPair = pair!.Split('-');
        
        return (validPair[0], validPair[1]);
    }
}