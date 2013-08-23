using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using DesktopMessenger.Common;
using DesktopMessenger.Common.Models;

namespace DesktopMessenger.ViewModels
{
    internal class ChatViewModel : ViewModelBase
    {
        private readonly IMessengerService _service;
        private string _isTyping;

        public string IsTyping
        {
            get { return _isTyping; }
            private set
            {
                _isTyping = value;
                OnPropertyChanged("IsTyping");
            }
        }

        public Contact Contact { get; private set; } //TODO change to Contacts
        public ObservableCollection<Message> Messages { get; private set; }

        public ChatViewModel(IMessengerService service, Contact contact)
        {
            _service = service;

            Contact = contact;
            Messages = new ObservableCollection<Message>();

            _service.MessageReceived += MessageReceived;
            _service.IsTypingUpdated += IsTypingUpdated;
        }

        private void IsTypingUpdated(object sender, IsTypingEventArgs e)
        {
            if (e.Contact == Contact.Name) //FIXME work with ids
                IsTyping = e.IsTyping ? String.Format("{0} is typing...", e.Contact) : String.Empty;
        }

        private void MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Contact == Contact.Name) //FIXME work with ids
                Application.Current.Dispatcher.Invoke(
                    new Action(() => Messages.Add(new Message {Contact = Contact, Content = e.Message})));
        }
    }
}
