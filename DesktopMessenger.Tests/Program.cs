using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesktopMessenger.Common;
using DesktopMessenger.Facebook;

namespace DesktopMessenger.Tests
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Facebook Login:");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (var facebook = new FacebookMessengerAdapter())
            {
                facebook.PresenceUpdated += Facebook_PresenceUpdated;
                facebook.MessageReceived += Facebook_MessageReceived;
                facebook.IsTypingUpdated += Facebook_IsTypingUpdated;

                facebook.Connect(username, password);

                Console.ReadLine();
                facebook.PresenceUpdated -= Facebook_PresenceUpdated;
                facebook.MessageReceived -= Facebook_MessageReceived;
                facebook.IsTypingUpdated -= Facebook_IsTypingUpdated;
            }
        }

        private static void Facebook_IsTypingUpdated(object sender, IsTypingEventArgs e)
        {
            Console.WriteLine("{0} {1}", e.Contact, e.IsTyping ? "is typing..." : "stopped typing.");
        }

        static void Facebook_MessageReceived(object sender, MessageEventArgs e)
        {
            Console.WriteLine("{0}: {1}", e.Contact, e.Message);
        }

        static void Facebook_PresenceUpdated(object sender, PresenceEventArgs e)
        {
            Console.WriteLine("{0} ({1})", e.Contact, e.PresenceStatus);
        }
    }
}
