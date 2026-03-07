namespace CryptoPriceTracker.Domain.Types;

public abstract class BaseTypeValue
{
    protected BaseTypeValue(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
}