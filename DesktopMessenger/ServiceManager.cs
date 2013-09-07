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
        private static readonly IDictionary<string, ChatView> _chats;

        public static ObservableCollection<IMessengerService> Services { get; private set; }
        public static ObservableCollection<ChatView> Chats { get; private set; }

        public static event EventHandler<AccountAddedEventArgs> Added;
        public static event EventHandler<EventArgs> Removed;

        static ServiceManager()
        {
            _chats = new Dictionary<string, ChatView>();

            Services = new ObservableCollection<IMessengerService>();
            Chats = new ObservableCollection<ChatView>();
        }

        public static void Add(Account account)
        {
            account.Service.MessageReceived += Service_MessageReceived; //HACK remove later
            Services.Add(account.Service);
            if (Added != null)
                Added(null, new AccountAddedEventArgs(account));
        }

        public static void Remove(IMessengerService service)
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
                App.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        var contact = new Contact(Guid.NewGuid().ToString()) { Name = e.Contact };
                        var chatViewModel = new ChatViewModel(service, contact);
                        chatViewModel.Messages.Add(new Message { Contact = contact, Content = e.Message });
                        chatView = new ChatView { DataContext = chatViewModel };
                        _chats.Add(e.Contact, chatView);
                        Chats.Add(chatView);
                    }));
            }
            //chatView.Show();
        }
    }
}
