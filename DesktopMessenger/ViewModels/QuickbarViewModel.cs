using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DesktopMessenger.Commands;
using DesktopMessenger.Common;
using DesktopMessenger.Common.Models;
using DesktopMessenger.Views;

namespace DesktopMessenger.ViewModels
{
    internal class QuickbarViewModel : ViewModelBase
    {
        private ContactListView _contactListView;
        private readonly IDictionary<string, ChatView> _chats;

        public ContactListView ContactListView
        {
            get { return _contactListView; }
            private set { _contactListView = value; OnPropertyChanged("ContactListView"); }
        }

        public ObservableCollection<ChatView> Chats { get; private set; }

        public ICommand ShowSettingsCommand { get; private set; }
        public ICommand ShutdownCommand { get; private set; }

        public QuickbarViewModel()
        {
            _chats = new Dictionary<string, ChatView>();

            ContactListView = new ContactListView {DataContext = new ContactListViewModel()};
            Chats = new ObservableCollection<ChatView>();

            ShowSettingsCommand = new DelegateCommand(o => new SettingsView {DataContext = new SettingsViewModel()}.ShowDialog());
            ShutdownCommand = new DelegateCommand(o => App.Current.Shutdown());

            ServiceManager.Added += ServiceManager_Added;
        }

        private void ServiceManager_Added(object sender, AccountAddedEventArgs e)
        {
            e.Account.Service.MessageReceived += Service_MessageReceived;
        }

        private void Service_MessageReceived(object sender, MessageEventArgs e)
        {
            var service = sender as IMessengerService;
            if (service == null)
                return;

            ChatView chatView;
            if (!_chats.TryGetValue(e.Contact, out chatView))
            {
                App.Current.Dispatcher.Invoke(new Action(() =>
                {
                    var contact = new Contact(Guid.NewGuid().ToString()) { Name = e.Contact };
                    var chatViewModel = new ChatViewModel(service, contact);
                    chatViewModel.Messages.Add(new Message { Contact = contact, Content = e.Message });
                    chatView = new ChatView { DataContext = chatViewModel };
                    _chats.Add(e.Contact, chatView);
                    Chats.Add(chatView);
                }));
            }
        }
    }
}
