using System.Net.Http.Headers;
using System.Text.Json;
using DotNetEnv;
using MongoDB.Driver;

class CoinRepository
{
    private HttpClient _client;
    private IMongoClient _mongoDB;

    public CoinRepository(HttpClient client, IMongoClient mongoDB)
    {
        _client = client;
        _mongoDB = mongoDB;
    }

    public async Task<IEnumerable<GeckoCoinDto>> GetRemoteCoinRecords()
    {
        try
        {
            var apiKey = Env.GetString("GECKO_API_KEY");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd"),
                Headers =
    {
        { "accept", "application/json" },
        { "x-cg-demo-api-key", apiKey },
    },
            };
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var coins = JsonSerializer.Deserialize<IEnumerable<GeckoCoinDto>>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return coins ?? Enumerable.Empty<GeckoCoinDto>();
            }
        }
        catch (Exception)
        {
            return Enumerable.Empty<GeckoCoinDto>();
        }
    }
    public async Task AddOrUpdateCoinRecords(IEnumerable<Coin> coins)
    {
        try
        {
            var currentLocalCoins = await GetLocalCoinRecords();

            if (!currentLocalCoins.Any())
            {
                await _mongoDB.GetDatabase("CoinsDB").GetCollection<Coin>("coins").InsertManyAsync(coins);

                return;
            }

            var coinsCollection = _mongoDB.GetDatabase("CoinsDB").GetCollection<Coin>("coins");

            foreach (var c in coins)
            {
                var filter = Builders<Coin>.Filter.Eq(e => e.Code, c.Code);
                var coin = await coinsCollection.Find(filter).FirstOrDefaultAsync();

                var newSnapshots = new List<CoinDataSnapshot>();

                newSnapshots.AddRange(coin?.Snapshots ?? []);
                newSnapshots.AddRange(c.Snapshots);

                var update = Builders<Coin>.Update.Set(r => r.Snapshots, newSnapshots);
                await coinsCollection.UpdateOneAsync(filter, update);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Coins Count {coins.Count()}");
            Console.WriteLine($"Error {ex}");
        }
    }
    public async Task<IEnumerable<Coin>> GetLocalCoinRecords()
    {
        var filter = Builders<Coin>.Filter.Empty;

        var result = await _mongoDB.GetDatabase("CoinsDB").GetCollection<Coin>("coins").Find(filter).ToListAsync();

        return result;
    }
}