using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Common
{
    public interface IMessengerAdapter
    {
        event EventHandler<PresenceEventArgs> PresenceUpdated;
        event EventHandler<MessageEventArgs>  MessageReceived;
        event EventHandler<IsTypingEventArgs> IsTypingUpdated;

        void Connect(string username, string password);
    }
}
