using System;
using PassStorage2.Models;

namespace PassStorage2.Base.DataCryptoLayer
{
    public class EntryProtection : IDisposable
    {
        protected string Primary { get; set; }
        protected string Secondary { get; set; }
        public bool IsAllowed { get; protected set; }

        public EntryProtection(string primary, string secondary)
        {
            if (string.IsNullOrEmpty(primary) || string.IsNullOrEmpty(secondary))
                throw new Exception("Passwords are incorrect!");

            Primary = primary;
            Secondary = secondary;
            IsAllowed = false;
        }

        public void Validate()
        {
            Logger.Instance.Debug($"############## FIRST: {Primary}");
            Logger.Instance.Debug($"############## FIRST: {Secondary}");
            Logger.Instance.Debug($"############## FirstHash: {SHA.GenerateSHA256String(Primary)}");
            Logger.Instance.Debug($"############## SecondaryHash: {SHA.GenerateSHA256String(Secondary)}");

            IsAllowed = SHA.Equals(Constants.Fhash, Primary) && SHA.Equals(Constants.Shash, Secondary);
            Logger.Instance.Debug($"IsAllowed = {IsAllowed}");
        }

        public void Dispose()
        { }
    }
}
