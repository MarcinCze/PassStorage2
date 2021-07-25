using PassStorage2.Logger.Interfaces;
using PassStorage2.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace PassStorage2.Base.DataCryptoLayer
{
    public class Decoder : Coder, Interfaces.IDecodeData
    {
        public Decoder(ILogger logger) : base(logger)
        { }

        public IEnumerable<Password> Decode(IEnumerable<Password> passwords, string primaryKey, string secondaryKey)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                Task.WaitAll(
                    passwords
                        .Select(pass => Task.Factory.StartNew(() => Decode(pass, primaryKey, secondaryKey)))
                        .ToArray()
                );

                return passwords;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return passwords;
            }
            finally
            {
                watch.Stop();
                logger.Debug($"DECODE finished in {watch.ElapsedMilliseconds} ms");
            }
        }

        public Password Decode(Password password, string primaryKey, string secondaryKey)
        {
            try
            {
                Task.WaitAll(new Task[]
                {
                    Task.Factory.StartNew(() => password.Title = Rijndael.DecryptRijndael(password.Title, secondaryKey)),
                    Task.Factory.StartNew(() => password.Login = Rijndael.DecryptRijndael(password.Login, secondaryKey)),
                    Task.Factory.StartNew(() => password.Pass = Rijndael.DecryptRijndael(password.Pass, secondaryKey)),
                    Task.Factory.StartNew(() => password.AdditionalInfo = string.IsNullOrEmpty(password.AdditionalInfo) ? null : Rijndael.DecryptRijndael(password.AdditionalInfo, secondaryKey))
                });

                return password;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return password;
            }
        }
    }
}