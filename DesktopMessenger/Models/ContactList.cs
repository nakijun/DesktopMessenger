using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Models
{
    internal class ContactList
    {
        private readonly ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
        }
    }
}
