using DesktopMessenger.Common;
using DesktopMessenger.Models;

namespace DesktopMessenger.ViewModels
{
    internal class ChatViewModel
    {
        private readonly Chat _chat;
        private readonly Contact _contact;
        private readonly IMessengerService _service;

        public ChatViewModel(IMessengerService service, Contact contact)
        {
            _chat = new Chat();
            _contact = contact;
            _service = service;

            _service.MessageReceived += MessageReceived;
        }

        private void MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Contact == _contact.Id)
            {
                _chat.Messages.Add(new Message { Contact = _contact, Content = e.Message });
            }
        }
    }
}
