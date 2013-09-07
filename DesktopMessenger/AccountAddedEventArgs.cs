using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopMessenger.Common;
using DesktopMessenger.Common.Models;

namespace DesktopMessenger
{
    internal class AccountAddedEventArgs : EventArgs
    {
        public Account Account { get; private set; }

        public AccountAddedEventArgs(Account account)
        {
            Account = account;
        }
    }
}
