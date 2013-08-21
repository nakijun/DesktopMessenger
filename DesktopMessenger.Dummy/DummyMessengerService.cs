using System;
using System.Security;
using System.Threading;
using DesktopMessenger.Common;

namespace DesktopMessenger.Dummy
{
    public class DummyMessengerService : IMessengerService
    {
        private readonly Timer _timer;

        public event EventHandler<EventArgs> LoggedIn;
        public event EventHandler<PresenceEventArgs> PresenceUpdated;
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<IsTypingEventArgs> IsTypingUpdated;

        public string ServiceName
        {
            get { return "Dummy"; }
        }

        public DummyMessengerService()
        {
            _timer = new Timer(delegate
            {
                if (MessageReceived != null)
                {
                    MessageReceived(this, new MessageEventArgs("foo", "test message"));
                    MessageReceived(this, new MessageEventArgs("bar", "hello world"));
                }
            }, null, 0, 5000);
        }

        public void Connect(string username, SecureString password)
        {
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
