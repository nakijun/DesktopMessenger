using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security;
using System.Windows.Controls;
using System.Windows.Input;
using DesktopMessenger.Commands;
using DesktopMessenger.Common;
using DesktopMessenger.Common.Models;
using DesktopMessenger.Views;

namespace DesktopMessenger.ViewModels
{
    internal class SettingsViewModel : ViewModelBase
    {
        private UserControl _currentView;

        public ObservableCollection<Account> Accounts
        {
            get { return AccountManager.Accounts; }
        }

        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged("CurrentView"); }
        }

        public SettingsViewModel()
        {
            CurrentView = new AccountSettingsView { DataContext = new AccountSettingsViewModel() };
        }
    }
}
