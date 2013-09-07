using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
            var password = new SecureString();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (!Char.IsControl(key.KeyChar))
                {
                    password.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            using (var facebook = MessengerServiceFactory.CreateInstance("Facebook"))
            {
                facebook.StatusUpdated += Facebook_StatusUpdated;
                facebook.MessageReceived += Facebook_MessageReceived;
                facebook.IsTypingUpdated += Facebook_IsTypingUpdated;

                facebook.Connect(username, password);

                Console.ReadLine();
                facebook.StatusUpdated -= Facebook_StatusUpdated;
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

        static void Facebook_StatusUpdated(object sender, StatusUpdatedEventArgs e)
        {
            Console.WriteLine("{0} ({1})", e.Contact, e.Status);
        }
    }
}
