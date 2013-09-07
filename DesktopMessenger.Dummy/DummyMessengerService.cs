using System;
using System.Security;
using System.Threading;
using DesktopMessenger.Common;

namespace DesktopMessenger.Dummy
{
    public class DummyMessengerService : IMessengerService
    {
        private readonly Thread _thread;
        private bool _running;

        public event EventHandler<EventArgs> LoggedIn;
        public event EventHandler<StatusUpdatedEventArgs> StatusUpdated;
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<IsTypingEventArgs> IsTypingUpdated;

        public string ServiceName { get { return "Dummy"; } }
        public Status Status { get; set; }

        public DummyMessengerService()
        {
            Status = Status.Offline;
            _thread = new Thread(DummyThread) {IsBackground = true};
        }

        private void DummyThread()
        {
            Status = Status.Online;

            if (StatusUpdated != null)
            {
                StatusUpdated(this, new StatusUpdatedEventArgs("foo", Status.Online));
                StatusUpdated(this, new StatusUpdatedEventArgs("bar", Status.Online));
            }

            var typing = false;

            while (_running)
            {
                if (IsTypingUpdated != null)
                {
                    typing = !typing;
                    IsTypingUpdated(this, new IsTypingEventArgs("foo", typing));
                }
                if (MessageReceived != null)
                {
                    MessageReceived(this, new MessageEventArgs("foo", "test message " + DateTime.Now));
                    MessageReceived(this, new MessageEventArgs("bar", "hello world " + DateTime.Now));
                }

                Thread.Sleep(5000);
            }
        }

        public void Connect(string username, SecureString password)
        {
            if (LoggedIn != null)
                LoggedIn(this, EventArgs.Empty);

            _running = true;
            _thread.Start();
        }

        public void Dispose()
        {
            Status = Status.Offline;

            if (_running)
            {
                _running = false;
                _thread.Join();
            }
        }
    }
}
