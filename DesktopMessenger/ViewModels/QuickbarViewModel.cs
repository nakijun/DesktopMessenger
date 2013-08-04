using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DesktopMessenger.Commands;
using DesktopMessenger.Views;

namespace DesktopMessenger.ViewModels
{
    internal class QuickbarViewModel
    {
        private readonly ContactListViewModel _contactListViewModel;

        public ICommand ShowContactListCommand { get; private set; }

        public QuickbarViewModel()
        {
            _contactListViewModel = new ContactListViewModel();

            ShowContactListCommand = new ShowContactListCommand(this);
        }

        public void ShowContactList()
        {
            var contactListView = new ContactListView { DataContext = _contactListViewModel };
            contactListView.Show();
        }
    }
}
