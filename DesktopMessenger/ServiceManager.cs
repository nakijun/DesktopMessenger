using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DesktopMessenger.Common;
using DesktopMessenger.Models;

namespace DesktopMessenger
{
    internal static class ServiceManager
    {
        public static ObservableCollection<IMessengerService> Services { get; private set; }

        static ServiceManager()
        {
            Services = new ObservableCollection<IMessengerService>();
        }

        public static void Connect(IMessengerService service, Account account)
        {
            //TODO initialize service
            service.Connect(account.Username, account.Password);

            Services.Add(service);
        }
        public static void Disconnect(IMessengerService service)
        {
            service.Dispose();

            Services.Remove(service);
        }
    }
}
