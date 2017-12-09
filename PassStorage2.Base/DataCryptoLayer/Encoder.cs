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
                    //pass.Title = VigenereCipher.Encipher(pass.Title, primaryKey);
                    //pass.Login = VigenereCipher.Encipher(pass.Login, primaryKey);
                    //pass.Pass = VigenereCipher.Encipher(pass.Pass, primaryKey);

                    //pass.Title = PlayfairCipher.Encipher(pass.Title, secondaryKey);
                    //pass.Login = PlayfairCipher.Encipher(pass.Login, secondaryKey);
                    //pass.Pass = PlayfairCipher.Encipher(pass.Pass, secondaryKey);

                    //pass.Title = AES.Encrypt(pass.Title, primaryKey);
                    //pass.Login = AES.Encrypt(pass.Login, primaryKey);
                    //pass.Pass = AES.Encrypt(pass.Pass, primaryKey);

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
    }
}
