namespace PassStorage2.Base.DataCryptoLayer
{
    public class EntryProtection : Interfaces.IEntryProtection
    {
        public bool IsAllowed(string primary, string secondary, string primaryExpected, string secondaryExpected) => 
            SHA.Equals(primaryExpected, primary) && SHA.Equals(secondaryExpected, secondary);
    }
}