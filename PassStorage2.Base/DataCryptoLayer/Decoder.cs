﻿using PassStorage2.Logger.Interfaces;
using PassStorage2.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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
                foreach (var pass in passwords)
                {
                    // 9716 ms
                    //pass.Title = Rijndael.DecryptRijndael(pass.Title, secondaryKey);
                    //pass.Login = Rijndael.DecryptRijndael(pass.Login, secondaryKey);
                    //pass.Pass = Rijndael.DecryptRijndael(pass.Pass, secondaryKey);

                    // 5565 ms
                    Task.WaitAll(new Task[]
                    {
                        Task.Factory.StartNew(() => pass.Title = Rijndael.DecryptRijndael(pass.Title, secondaryKey)),
                        Task.Factory.StartNew(() => pass.Login = Rijndael.DecryptRijndael(pass.Login, secondaryKey)),
                        Task.Factory.StartNew(() => pass.Pass = Rijndael.DecryptRijndael(pass.Pass, secondaryKey)),
                        Task.Factory.StartNew(() => pass.AdditionalInfo = string.IsNullOrEmpty(pass.AdditionalInfo) ? null : Rijndael.DecryptRijndael(pass.AdditionalInfo, secondaryKey))
                    });
                }

                return passwords;
            }
            catch (System.Exception e)
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
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                //password.Title = Rijndael.DecryptRijndael(password.Title, secondaryKey);
                //password.Login = Rijndael.DecryptRijndael(password.Login, secondaryKey);
                //password.Pass = Rijndael.DecryptRijndael(password.Pass, secondaryKey);
                Task.WaitAll(new Task[]
                {
                    Task.Factory.StartNew(() => password.Title = Rijndael.DecryptRijndael(password.Title, secondaryKey)),
                    Task.Factory.StartNew(() => password.Login = Rijndael.DecryptRijndael(password.Login, secondaryKey)),
                    Task.Factory.StartNew(() => password.Pass = Rijndael.DecryptRijndael(password.Pass, secondaryKey)),
                    Task.Factory.StartNew(() => password.AdditionalInfo = Rijndael.DecryptRijndael(password.AdditionalInfo, secondaryKey))
                });

                return password;
            }
            catch (System.Exception e)
            {
                logger.Error(e);
                return password;
            }
            finally
            {
                watch.Stop();
                logger.Debug($"DECODE finished in {watch.ElapsedMilliseconds} ms");
            }
        }
    }
}
