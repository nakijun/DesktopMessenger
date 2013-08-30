using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DesktopMessenger.Commands;
using DesktopMessenger.Common.Models;
using DesktopMessenger.Views;
using DesktopMessenger.Properties;

namespace DesktopMessenger.ViewModels
{
    internal class ContactListViewModel
    {
        public ICommand ShowSettingsCommand { get; private set; }
        public ICommand SetStatusCommand { get; private set; }  

        public String Status { get; private set; }

        public ObservableCollection<ContactList> ContactLists
        {
            get { return ServiceManager.ContactLists; }
        }

        public ContactListViewModel()
        {
            ShowSettingsCommand = new DelegateCommand(o => new SettingsView {DataContext = new SettingsViewModel()}.ShowDialog());
        }
    }
}
