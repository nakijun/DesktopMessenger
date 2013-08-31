using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DesktopMessenger.Commands;
using DesktopMessenger.Views;

namespace DesktopMessenger.ViewModels
{
    internal class QuickbarViewModel : ViewModelBase
    {
        private ContactListView _contactListView;

        public ContactListView ContactListView
        {
            get { return _contactListView; }
            private set { _contactListView = value; OnPropertyChanged("ContactListView"); }
        }

        public ObservableCollection<ChatView> Chats { get { return ServiceManager.Chats; } }

        public ICommand ShowSettingsCommand { get; private set; }
        public ICommand ShutdownCommand { get; private set; }

        public QuickbarViewModel()
        {
            ContactListView = new ContactListView {DataContext = new ContactListViewModel()};

            ShowSettingsCommand = new DelegateCommand(o => new SettingsView {DataContext = new SettingsViewModel()}.ShowDialog());
            ShutdownCommand = new DelegateCommand(o => App.Current.Shutdown());
        }
    }
}
