using System;
using System.Security.Cryptography;
using System.Text;

namespace PassStorage2.Base
{
    public static class RandomPassword
    {
        /// <summary>
        /// Generating random string
        /// </summary>
        /// <param name="length">Length of the string</param>
        /// <returns></returns>
        public static string Generate(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var res = new StringBuilder();
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }
    }
}
