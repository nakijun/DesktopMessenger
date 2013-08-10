using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace DesktopMessenger.Common
{
    public interface IMessengerService : IDisposable
    {
        event EventHandler<EventArgs> LoggedIn;
        event EventHandler<PresenceEventArgs> PresenceUpdated;
        event EventHandler<MessageEventArgs> MessageReceived;
        event EventHandler<IsTypingEventArgs> IsTypingUpdated;
        string ServiceName { get; }
        void Connect(string username, SecureString password);
    }
}
