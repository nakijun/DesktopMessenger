using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Common
{
    public class MessageEventArgs : EventArgs
    {
        public string Contact { get; private set; } //TODO use object references instead of strings
        public string Message { get; private set; }

        public MessageEventArgs(string contact, string message)
        {
            Contact = contact;
            Message = message;
        }
    }
}
