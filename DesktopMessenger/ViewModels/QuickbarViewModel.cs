using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DesktopMessenger.Commands;
using DesktopMessenger.Views;

namespace DesktopMessenger.ViewModels
{
    internal class QuickbarViewModel
    {
        public ContactListView ContactListView { get; private set; }
        public ObservableCollection<ChatView> Chats { get { return ServiceManager.Chats; } }

        public QuickbarViewModel()
        {
            ContactListView = new ContactListView {DataContext = new ContactListViewModel()};

            //ShowContactListCommand = new ShowContactListCommand(this);
        }
    }
}
