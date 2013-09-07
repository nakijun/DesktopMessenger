using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopMessenger.Common
{
    public class StatusUpdatedEventArgs : EventArgs
    {
        public string Contact { get; private set; }
        public Status Status { get; private set; }

        public StatusUpdatedEventArgs(string contact, Status status)
        {
            Contact = contact;
            Status = status;
        }
    }
}
