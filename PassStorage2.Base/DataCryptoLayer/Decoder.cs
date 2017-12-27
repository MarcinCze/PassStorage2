using PassStorage2.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PassStorage2.Base.DataCryptoLayer
{
    public class Decoder : Interfaces.IDecodeData
    {
        public IEnumerable<Password> Decode(IEnumerable<Password> passwords, string primaryKey, string secondaryKey)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                foreach (var pass in passwords)
                {
                    pass.Title = Rijndael.DecryptRijndael(pass.Title, secondaryKey);
                    pass.Login = Rijndael.DecryptRijndael(pass.Login, secondaryKey);
                    pass.Pass = Rijndael.DecryptRijndael(pass.Pass, secondaryKey);
                }

                return passwords;
            }
            catch (System.Exception e)
            {
                Logger.Instance.Error(e);
                return passwords;
            }
            finally
            {
                watch.Stop();
                Logger.Instance.Debug($"DECODE finished in {watch.ElapsedMilliseconds} ms");
            }
        }

        public Password Decode(Password password, string primaryKey, string secondaryKey)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                password.Title = Rijndael.DecryptRijndael(password.Title, secondaryKey);
                password.Login = Rijndael.DecryptRijndael(password.Login, secondaryKey);
                password.Pass = Rijndael.DecryptRijndael(password.Pass, secondaryKey);

                return password;
            }
            catch (System.Exception e)
            {
                Logger.Instance.Error(e);
                return password;
            }
            finally
            {
                watch.Stop();
                Logger.Instance.Debug($"DECODE finished in {watch.ElapsedMilliseconds} ms");
            }
        }
    }
}
