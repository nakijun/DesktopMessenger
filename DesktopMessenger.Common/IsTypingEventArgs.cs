using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Common
{
    public class IsTypingEventArgs : EventArgs
    {
        public string Contact { get; private set; }
        public bool IsTyping { get; private set; }

        public IsTypingEventArgs(string contact, bool isTyping)
        {
            Contact = contact;
            IsTyping = isTyping;
        }
    }
}
