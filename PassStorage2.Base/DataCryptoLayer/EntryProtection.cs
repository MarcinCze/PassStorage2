using System;

namespace PassStorage2.Base.DataCryptoLayer
{
    public class EntryProtection : IDisposable
    {
        protected string Primary { get; set; }
        protected string PrimaryExpected { get; set; }
        protected string Secondary { get; set; }
        protected string SecondaryExpected { get; set; }

        public EntryProtection(string primary, string secondary, string primaryExpected, string secondaryExpected)
        {
            if (string.IsNullOrEmpty(primary) || string.IsNullOrEmpty(secondary))
                throw new Exception("Passwords are incorrect!");

            Primary = primary;
            PrimaryExpected = primaryExpected;
            Secondary = secondary;
            SecondaryExpected = secondaryExpected;
        }

        public bool IsAllowed => SHA.Equals(PrimaryExpected, Primary) && SHA.Equals(SecondaryExpected, Secondary);

        public void Dispose()
        { }
    }
}