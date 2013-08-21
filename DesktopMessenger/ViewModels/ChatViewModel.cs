using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using DesktopMessenger.Common;
using DesktopMessenger.Models;

namespace DesktopMessenger.ViewModels
{
    internal class ChatViewModel : ViewModelBase
    {
        private readonly Contact _contact;
        private readonly IMessengerService _service;

        public ObservableCollection<Message> Messages { get; private set; }

        public ChatViewModel(IMessengerService service, Contact contact)
        {
            _contact = contact;
            _service = service;

            Messages = new ObservableCollection<Message>();

            _service.MessageReceived += MessageReceived;
        }

        private void MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Contact == _contact.Name) //FIXME work with ids
            {
                Application.Current.Dispatcher.Invoke(
                    new Action(() => Messages.Add(new Message {Contact = _contact, Content = e.Message})));
            }
        }
    }
}
