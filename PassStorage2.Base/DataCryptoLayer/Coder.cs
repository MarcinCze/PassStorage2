using PassStorage2.Logger.Interfaces;

namespace PassStorage2.Base.DataCryptoLayer
{
    public abstract class Coder
    {
        protected ILogger logger;

        public Coder(ILogger logger)
        {
            this.logger = logger;
        }
    }
}
