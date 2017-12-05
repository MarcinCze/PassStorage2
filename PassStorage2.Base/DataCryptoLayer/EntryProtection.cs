using System;

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
            //TODO Make validation
            IsAllowed = true;
        }

        public void Dispose()
        {
            
        }
    }
}
