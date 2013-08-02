using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DesktopMessenger.Common
{
    public static class SecureStringHelper
    {
        public static string GetString(this SecureString secureString)
        {
            var bstr = Marshal.SecureStringToBSTR(secureString);
            
            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }
    }
}
