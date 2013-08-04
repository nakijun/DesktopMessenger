using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using DesktopMessenger.Common;
using DesktopMessenger.Models;
using DesktopMessenger.Properties;
using DesktopMessenger.ViewModels;
using DesktopMessenger.Views;

namespace DesktopMessenger
{
    internal static class ServiceManager
    {
        private static readonly IDictionary<string, ChatView> _chats;
        private static readonly IDictionary<IMessengerService, ContactList> _contactLists;

        public static ObservableCollection<IMessengerService> Services { get; private set; }
        public static ObservableCollection<ContactList> ContactLists { get; private set; }

        static ServiceManager()
        {
            _chats = new Dictionary<string, ChatView>();
            _contactLists = new Dictionary<IMessengerService, ContactList>();

            ContactLists = new ObservableCollection<ContactList>();
            Services = new ObservableCollection<IMessengerService>();
        }

        public static void Connect(Account account)
        {
            account.Service.Connect(account.Username, account.Password);

            //HACK here just for testing
            account.Service.MessageReceived += Service_MessageReceived;
            account.Service.PresenceUpdated += Service_PresenceUpdated;

            Services.Add(account.Service);
        }

        private static void Service_PresenceUpdated(object sender, PresenceEventArgs e)
        {
            var service = sender as IMessengerService;
            if (service == null)
                return;

            ContactList contactList;
            if (!_contactLists.TryGetValue(service, out contactList))
            {
                contactList = new ContactList();
                _contactLists.Add(service, contactList);
                Application.Current.Dispatcher.Invoke(new Action(() => ContactLists.Add(contactList)));
            }

            Contact contact;
            if (contactList.Contacts.Any(c => c.Name == e.Contact))
            {
                contact = contactList.Contacts.Single(c => c.Name == e.Contact);
            }
            else
            {
                contact = new Contact(e.Contact) {Name = e.Contact}; //FIXME
                Application.Current.Dispatcher.Invoke(new Action(() => contactList.Contacts.Add(contact)));
            }
        }

        public static void Disconnect(IMessengerService service)
        {
            service.MessageReceived -= Service_MessageReceived;
            service.PresenceUpdated -= Service_PresenceUpdated;

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
                Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        var contact = new Contact(Guid.NewGuid().ToString()) {Name = e.Contact};
                        var chatViewModel = new ChatViewModel(service, contact);
                        chatViewModel.Messages.Add(new Message { Contact = contact, Content = e.Message });
                        chatView = new ChatView {DataContext = chatViewModel};
                    }));
                _chats.Add(e.Contact, chatView);
            }
            chatView.Show();
        }
    }
}
