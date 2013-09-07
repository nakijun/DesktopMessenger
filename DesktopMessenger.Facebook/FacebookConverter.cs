using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopMessenger.Common;
using agsXMPP.protocol.client;

namespace DesktopMessenger.Facebook
{
    internal static class FacebookConverter
    {
        public static Status ToPresenceStatus(PresenceType presence)
        {
            switch (presence)
            {
                case PresenceType.available:
                    return Status.Online;
                case PresenceType.unavailable:
                    return Status.Offline;
                default:
                    throw new NotSupportedException("Conversion not defined for this presence type.");
            }
        }
    }
}
