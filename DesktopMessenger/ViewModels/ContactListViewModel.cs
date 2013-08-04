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
        private readonly SettingsViewModel _settingsViewModel;

        public ICommand ShowSettingsCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public ObservableCollection<ContactList> ContactLists
        {
            get { return ServiceManager.ContactLists; }
        }

        public ContactListViewModel()
        {
            _settingsViewModel = new SettingsViewModel();

            ShowSettingsCommand = new ShowSettingsCommand(this);
            ExitCommand = new ExitCommand(this);
        }

        public void ShowSettings()
        {
            var settingsView = new SettingsView {DataContext = _settingsViewModel};
            settingsView.ShowDialog();
        }
    }
}
