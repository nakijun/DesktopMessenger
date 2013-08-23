using System.Security;

namespace DesktopMessenger.Common.Models
{
    public class Account
    {
        public IMessengerService Service { get; set; }
        public string Username { get; set; }
        public SecureString Password { get; set; }
    }
}
