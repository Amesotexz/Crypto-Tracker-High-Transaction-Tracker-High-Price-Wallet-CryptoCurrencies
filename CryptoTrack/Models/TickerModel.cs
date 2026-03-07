using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CryptoTracker.WinFormsUI.Models;

internal sealed class TickerModel : INotifyPropertyChanged
{
	private decimal _price;
	private decimal _low24H;
	private decimal _high24H;
	private string _changePercentage24H = "0%";
	
	public required string Exchange { get; init; }
	
	public decimal Price
	{
		get => _price; set { _price = value; OnPropertyChanged(); }
	}
	
	[DisplayName("Low24h")]
	public decimal Low24H
	{
		get => _low24H; set { _low24H = value; OnPropertyChanged(); }
	}
	
	[DisplayName("High24h")]
	public decimal High24H
	{
		get => _high24H; set { _high24H = value; OnPropertyChanged(); }
	}
	
	[DisplayName("Change24h")]
	public string ChangePercentage24H
	{
		get => _changePercentage24H; set { _changePercentage24H = value; OnPropertyChanged(); }
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;

	private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}