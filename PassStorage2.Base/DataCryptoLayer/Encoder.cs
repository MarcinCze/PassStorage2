using PassStorage2.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PassStorage2.Base.DataCryptoLayer
{
    public class Encoder : Interfaces.IEncodeData
    {
        public IEnumerable<Password> Encode(IEnumerable<Password> passwords, string primaryKey, string secondaryKey)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                foreach (var pass in passwords)
                {
                    pass.Title = Rijndael.EncryptRijndael(pass.Title, secondaryKey);
                    pass.Login = Rijndael.EncryptRijndael(pass.Login, secondaryKey);
                    pass.Pass = Rijndael.EncryptRijndael(pass.Pass, secondaryKey);
                }
                watch.Stop();
                Logger.Instance.Debug($"ENCODE finished in {watch.ElapsedMilliseconds} ms");
                return passwords;
            }
            catch (System.Exception e)
            {
                Logger.Instance.Error(e);
                return passwords;
            }
        }

        public Password Encode(Password password, string primaryKey, string secondaryKey)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                password.Title = Rijndael.EncryptRijndael(password.Title, secondaryKey);
                password.Login = Rijndael.EncryptRijndael(password.Login, secondaryKey);
                password.Pass = Rijndael.EncryptRijndael(password.Pass, secondaryKey);

                watch.Stop();
                Logger.Instance.Debug($"ENCODE finished in {watch.ElapsedMilliseconds} ms");
                return password;
            }
            catch (System.Exception e)
            {
                Logger.Instance.Error(e);
                return password;
            }
        }
    }
}
