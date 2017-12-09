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

                    //pass.Title = AES.Decrypt(pass.Title, primaryKey);
                    //pass.Login = AES.Decrypt(pass.Login, primaryKey);
                    //pass.Pass = AES.Decrypt(pass.Pass, primaryKey);

                    //pass.Title = PlayfairCipher.Decipher(pass.Title, secondaryKey);
                    //pass.Login = PlayfairCipher.Decipher(pass.Login, secondaryKey);
                    //pass.Pass = PlayfairCipher.Decipher(pass.Pass, secondaryKey);

                    //pass.Title = VigenereCipher.Decipher(pass.Title, primaryKey);
                    //pass.Login = VigenereCipher.Decipher(pass.Login, primaryKey);
                    //pass.Pass = VigenereCipher.Decipher(pass.Pass, primaryKey);
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
    }
}
