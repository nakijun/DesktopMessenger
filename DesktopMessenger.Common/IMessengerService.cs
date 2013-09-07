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
        event EventHandler<StatusUpdatedEventArgs> StatusUpdated;
        event EventHandler<MessageEventArgs> MessageReceived;
        event EventHandler<IsTypingEventArgs> IsTypingUpdated;
        string ServiceName { get; }
        Status Status { get; set; }
        void Connect(string username, SecureString password);
    }
}
