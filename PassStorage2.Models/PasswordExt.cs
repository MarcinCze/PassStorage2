using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PassStorage2.Models
{
    public class PasswordExt : Password
    {
        public bool IsValid { get; set; }
        public Visibility WarningIconState { get; set; }
    }
}
