using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DesktopMessenger.Commands;
using DesktopMessenger.Views;
using DesktopMessenger.Models;
using DesktopMessenger.Properties;

namespace DesktopMessenger.ViewModels
{
    internal class ContactListViewModel
    {
        private readonly ObservableCollection<ContactList> _contactLists = new ObservableCollection<ContactList>();

        public ICommand ShowSettingsCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public ObservableCollection<ContactList> ContactLists
        {
            get { return _contactLists; }
        }

        public ContactListViewModel()
        {
            ShowSettingsCommand = new ShowSettingsCommand(this);
            ExitCommand = new ExitCommand(this);

            var contactList = new ContactList();
            //TODO use real data in the future
            contactList.Contacts.Add(new Contact(Guid.NewGuid().ToString())
                {
                    Name = "foo",
                    Picture = Resources.status_online.ToBitmap()
                });
            contactList.Contacts.Add(new Contact(Guid.NewGuid().ToString())
                {
                    Name = "bar",
                    Picture = Resources.status_offline.ToBitmap()
                });
            ContactLists.Add(contactList);
        }

        public void ShowSettings()
        {
            new SettingsView().ShowDialog();
        }
    }
}
