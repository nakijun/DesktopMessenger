using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DesktopMessenger.Commands;
using DesktopMessenger.Common;
using DesktopMessenger.Common.Models;
using DesktopMessenger.Models;
using DesktopMessenger.Views;
using DesktopMessenger.Properties;

namespace DesktopMessenger.ViewModels
{
    internal class ContactListViewModel : ViewModelBase
    {
        private readonly IDictionary<IMessengerService, ContactList> _contactLists;

        public ObservableCollection<ContactList> ContactLists { get; private set; }

        public ICommand ShowSettingsCommand { get; private set; }

        public ContactListViewModel()
        {
            _contactLists = new Dictionary<IMessengerService, ContactList>();

            ContactLists = new ObservableCollection<ContactList>();

            ShowSettingsCommand = new DelegateCommand(o => new SettingsView {DataContext = new SettingsViewModel()}.ShowDialog());

            ServiceManager.Added += ServiceManager_Added;
        }

        private void ServiceManager_Added(object sender, AccountAddedEventArgs e)
        {
            e.Account.Service.StatusUpdated += Service_StatusUpdated;

            e.Account.Service.Connect(e.Account.Username, e.Account.Password);
        }

        private void Service_StatusUpdated(object sender, StatusUpdatedEventArgs e)
        {
            var service = sender as IMessengerService;
            if (service == null)
                return;

            ContactList contactList;
            if (!_contactLists.TryGetValue(service, out contactList))
            {
                contactList = new ContactList();
                _contactLists.Add(service, contactList);
                App.Current.Dispatcher.Invoke(new Action(() => ContactLists.Add(contactList)));
            }

            switch (e.Status)
            {
                case Status.Online:
                    if (!contactList.Contacts.Any(c => c.Name == e.Contact))
                    {
                        var contact = new Contact(e.Contact) { Name = e.Contact }; //FIXME
                        App.Current.Dispatcher.Invoke(new Action(() => contactList.Contacts.Add(contact)));
                    }
                    break;
                case Status.Offline:
                    if (contactList.Contacts.Any(c => c.Name == e.Contact))
                    {
                        var contact = contactList.Contacts.Single(c => c.Name == e.Contact);
                        App.Current.Dispatcher.Invoke(new Action(() => contactList.Contacts.Remove(contact)));
                    }
                    break;
            }
        }
    }
}
