public class CoinReport
{
    public required Coin Coin { get; set; }
    public decimal PercentChanged { get; set; }
    public decimal NewDolarPrice { get; set; }
    public decimal OldDolarPrice { get; set; }
    public DateTime NewDolarPriceDate { get; set; }
    public DateTime OldDolarPriceDate { get; set; }
}