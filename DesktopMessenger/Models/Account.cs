using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using DesktopMessenger.Common;

namespace DesktopMessenger.Models
{
    internal class Account
    {
        public IMessengerService Service { get; set; }
        public string Username { get; set; }
        public SecureString Password { get; set; }
    }
}
