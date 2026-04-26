namespace CryptoPriceTracker.Domain.Types;

public sealed class ProtocolTypes : BaseTypeValue
{
    private ProtocolTypes(string protocol) : base(protocol) { }

    public static ProtocolTypes WebSocket = new ProtocolTypes("WebSocket");
    public static ProtocolTypes Rest = new ProtocolTypes("Rest");
}