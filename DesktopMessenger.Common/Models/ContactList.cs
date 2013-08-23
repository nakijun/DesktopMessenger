using System.Collections.ObjectModel;

namespace DesktopMessenger.Common.Models
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
