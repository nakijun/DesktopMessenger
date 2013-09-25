using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using DesktopMessenger.Common;
using DesktopMessenger.Common.Models;
using DesktopMessenger.Properties;
using DesktopMessenger.ViewModels;
using DesktopMessenger.Views;

namespace DesktopMessenger
{
    internal static class ServiceManager
    {
        public static ObservableCollection<IMessengerService> Services { get; private set; }

        public static event EventHandler<AccountAddedEventArgs> Added;

        static ServiceManager()
        {
            Services = new ObservableCollection<IMessengerService>();
        }

        public static void Add(Account account)
        {
            Services.Add(account.Service);
            if (Added != null)
                Added(null, new AccountAddedEventArgs(account));
        }

        public static void Remove(IMessengerService service)
        {
            service.Dispose();
            Services.Remove(service);
        }
    }
}
