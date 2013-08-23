using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DesktopMessenger.Common.Models;

namespace DesktopMessenger
{
    internal static class AccountManager
    {
        public static ObservableCollection<Account> Accounts { get; private set; }

        static AccountManager()
        {
            Accounts = new ObservableCollection<Account>();

            //TODO load accounts from settings
        }

        public static void Add(Account account)
        {
            Accounts.Add(account);
        }
        public static void Remove(Account account)
        {
            Accounts.Remove(account);
        }
    }
}
