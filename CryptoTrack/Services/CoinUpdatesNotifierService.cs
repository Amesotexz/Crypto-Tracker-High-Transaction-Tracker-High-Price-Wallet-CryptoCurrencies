class CoinUpdatesNotifierService
{
    private CoinService _coinService;
    private EmailService _emailService;
    private ILogger<CoinUpdatesNotifierService> _logger;

    public CoinUpdatesNotifierService(
        CoinService coinService,
        EmailService emailService,
        ILogger<CoinUpdatesNotifierService> logger
        )
    {
        _coinService = coinService;
        _emailService = emailService;
        _logger = logger;

        var btc = new Coin()
        {
            Name = "Bitcoin",
            Code = "bitcoin"
        };

        users = new List<User> {
            new User
            {
                Name = "Leonardo",
                Email = "kiszewski1999@gmail.com",
                FavoriteCoins = new List<Coin>{ btc }
            }
        };
    }

    public async Task CheckUpdates()
    {
        var reports = await _coinService.AnalyzeCoinsDaily();

        foreach (var report in reports)
        {
            _logger.LogInformation($"Report from: {report.Coin.Code}");

            var interestedUsers = users.
                Where(u => u.FavoriteCoins.Any(coin => coin.Code == report.Coin.Code));

            foreach (var user in interestedUsers)
            {
                var mail = new SendEmailParams
                {
                    RecipientName = user.Name,
                    RecipientEmail = user.Email,
                    Subject = $"Updates on {report.Coin.Name}",
                    Body = $"Old Price: {report.OldDolarPrice} in {report.OldDolarPriceDate}\n" + $"New Price: {report.NewDolarPrice} in {report.NewDolarPriceDate}\n",
                };

                _logger.LogInformation($"Sending report: {report.Coin} to: {user.Name}");

                await _emailService.SendEmail(mail);
            }
        }
    }

    //TODO: Get from database
    private IEnumerable<User> users;
}