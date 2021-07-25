﻿using PassStorage2.Logger.Interfaces;
using PassStorage2.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PassStorage2.Base.DataCryptoLayer
{
    public class Encoder : Coder, Interfaces.IEncodeData
    {
        public Encoder(ILogger logger) : base(logger)
        { }

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
                logger.Debug($"ENCODE finished in {watch.ElapsedMilliseconds} ms");
                return passwords;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return passwords;
            }
        }

        public Password Encode(Password password, string primaryKey, string secondaryKey)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                Task.WaitAll(new Task[]
                {
                    Task.Factory.StartNew(() => password.Title = Rijndael.EncryptRijndael(password.Title, secondaryKey)),
                    Task.Factory.StartNew(() => password.Login = Rijndael.EncryptRijndael(password.Login, secondaryKey)),
                    Task.Factory.StartNew(() => password.Pass = Rijndael.EncryptRijndael(password.Pass, secondaryKey)),
                    Task.Factory.StartNew(() =>
                    {
                        if (!string.IsNullOrEmpty(password.AdditionalInfo))
                            password.AdditionalInfo = Rijndael.EncryptRijndael(password.AdditionalInfo, secondaryKey);
                    })
                });

                watch.Stop();
                logger.Debug($"ENCODE finished in {watch.ElapsedMilliseconds} ms");
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
