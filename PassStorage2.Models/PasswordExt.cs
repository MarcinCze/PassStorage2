using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage2.Models
{
    /// <summary>
    /// Password class extension. This class is not being saved to File/WS/DB
    /// </summary>
    public class PasswordExt : Password
    {
        public bool IsExpired { get; set; }

        /// <summary>
        /// Copy Password to PasswordExt
        /// </summary>
        /// <param name="pass">Original password</param>
        public void Copy(Password pass)
        {
            Copy(pass, 0);
        }

        /// <summary>
        /// Copy Password to PasswordExt
        /// </summary>
        /// <param name="pass">Original password</param>
        /// <param name="expirationDays">Expiration days for setting IsValid</param>
        public void Copy(Password pass, int expirationDays)
        {
            Id = pass.Id;
            Login = pass.Login;
            Title = pass.Title;
            Pass = pass.Pass;
            PassChangeTime = pass.PassChangeTime;
            SaveTime = pass.SaveTime;
            ViewCount = pass.ViewCount;
            IsExpired = (DateTime.Now - pass.PassChangeTime).TotalDays >= expirationDays;
        }

        /// <summary>
        /// Copy list of Passwords into list of PasswordsExt
        /// </summary>
        /// <param name="passwords"></param>
        /// <returns></returns>
        public static IEnumerable<PasswordExt> CopyList(IEnumerable<Password> passwords)
        {
            var passExtList = new List<PasswordExt>();
            foreach (Password pass in passwords)
            {
                var p = new PasswordExt();
                p.Copy(pass);
                passExtList.Add(p);
            }
            return passExtList;
        }
    }
}
