class User
{
    public Guid Id;
    public required string Email;
    public required string Name;
    public required IEnumerable<Coin> FavoriteCoins = Enumerable.Empty<Coin>();
}