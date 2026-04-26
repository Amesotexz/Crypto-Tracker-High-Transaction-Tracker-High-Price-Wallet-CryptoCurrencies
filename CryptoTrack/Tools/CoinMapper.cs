public static class CoinMapper
{
    public static Coin GeckoCoinToCoin(this GeckoCoinDto coinDto)
    {
        CoinDataSnapshot snapshot = new CoinDataSnapshot
        {
            date = DateTime.Now,
            DolarMarketCap = coinDto.MarketCap,
            DolarPrice = coinDto.CurrentPrice,
            Volume = coinDto.TotalVolume,
        };

        return new Coin
        {
            Code = coinDto.Id,
            Name = coinDto.Name,
            Snapshots = new List<CoinDataSnapshot> { snapshot }
        };
    }
}