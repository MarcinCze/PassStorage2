namespace PassStorage2.Base.DataCryptoLayer.Interfaces
{
    public interface IEntryProtection
    {
        bool IsAllowed(string primary, string secondary, string primaryExpected, string secondaryExpected);
    }
}
