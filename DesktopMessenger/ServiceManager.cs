using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DesktopMessenger.Common;
using DesktopMessenger.Models;
using DesktopMessenger.Properties;
using DesktopMessenger.ViewModels;
using DesktopMessenger.Views;

namespace DesktopMessenger
{
    internal static class ServiceManager
    {
        private static readonly IDictionary<string, ChatView> _chats = new Dictionary<string, ChatView>();

        public static ObservableCollection<IMessengerService> Services { get; private set; }

        static ServiceManager()
        {
            Services = new ObservableCollection<IMessengerService>();
        }

        public static void Connect(IMessengerService service, Account account)
        {
            service.Connect(account.Username, account.Password);

            //HACK here just for testing
            service.MessageReceived += Service_MessageReceived;

            Services.Add(service);
        }

        public static void Disconnect(IMessengerService service)
        {
            service.MessageReceived -= Service_MessageReceived;

            service.Dispose();

            Services.Remove(service);
        }

        private static void Service_MessageReceived(object sender, MessageEventArgs e)
        {
            var service = sender as IMessengerService;
            if (service == null)
                return;

            ChatView chatView;
            if (!_chats.TryGetValue(e.Contact, out chatView))
            {
                chatView = new ChatView
                    {
                        DataContext =
                            new ChatViewModel(service,
                                              new Contact(Guid.NewGuid().ToString()) {Name = e.Contact})
                    };
                chatView.Show();
            }
        }
    }
}
