using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Common
{
    public class PresenceEventArgs : EventArgs
    {
        public string Contact { get; private set; }
        public PresenceStatus PresenceStatus { get; private set; }

        public PresenceEventArgs(string contact, PresenceStatus presenceStatus)
        {
            Contact = contact;
            PresenceStatus = presenceStatus;
        }
    }
}
