using System.Collections.ObjectModel;
using DesktopMessenger.Common.Models;

namespace DesktopMessenger.Models
{
    public class ContactList
    {
        public ObservableCollection<Contact> Contacts { get; private set; }

        public ContactList()
        {
            Contacts = new ObservableCollection<Contact>();
        }
    }
}
