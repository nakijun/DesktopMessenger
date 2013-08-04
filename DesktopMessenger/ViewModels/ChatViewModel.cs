using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using DesktopMessenger.Common;
using DesktopMessenger.Models;

namespace DesktopMessenger.ViewModels
{
    internal class ChatViewModel : INotifyPropertyChanged
    {
        private readonly Contact _contact;
        private readonly IMessengerService _service;
        
        public event PropertyChangedEventHandler PropertyChanged;

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
            if (e.Contact == _contact.Name) //FIXME
            {
                Application.Current.Dispatcher.Invoke(
                    new Action(() => Messages.Add(new Message {Contact = _contact, Content = e.Message})));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
