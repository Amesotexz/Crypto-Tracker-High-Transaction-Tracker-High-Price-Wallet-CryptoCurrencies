public class Coin
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public IEnumerable<CoinDataSnapshot> Snapshots { get; set; }
}

public class CoinDataSnapshot
{
    public DateTime date { get; set; }
    public decimal DolarPrice { get; set; }
    public long DolarMarketCap { get; set; }
    public decimal Volume { get; set; }
}