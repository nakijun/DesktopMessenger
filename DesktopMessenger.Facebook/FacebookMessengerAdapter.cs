using System;
using System.Diagnostics;
using System.Security.Authentication;
using DesktopMessenger.Common;
using agsXMPP;
using agsXMPP.Net;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.client;
using agsXMPP.protocol.extensions.chatstates;

namespace DesktopMessenger.Facebook
{
    public class FacebookMessengerAdapter : IMessengerAdapter
    {
        #region Fields
        private readonly XmppClientConnection _xmppClientConnection;
        private PresenceStatus _presenceStatus;
        #endregion

        #region Events
        public event EventHandler<PresenceEventArgs> PresenceUpdated;
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<IsTypingEventArgs> IsTypingUpdated;
        #endregion

        #region Properties
        public string ServiceName { get { return "Facebook"; } }

        public PresenceStatus Presence
        {
            get { return _presenceStatus; }
            set
            {
                switch (value)
                {
                    case PresenceStatus.Online:
                        _xmppClientConnection.Send(new Presence(ShowType.chat, "Online") { Type = PresenceType.available });
                        _presenceStatus = value;
                        break;
                }
            }
        }
        #endregion

        #region Constructor
        public FacebookMessengerAdapter()
        {
            _xmppClientConnection = new XmppClientConnection("chat.facebook.com");
            _xmppClientConnection.OnAuthError += XmppClientConnection_OnAuthError;
            _xmppClientConnection.OnLogin += XmppClientConnection_OnLogin;
            _xmppClientConnection.OnPresence += XmppClientConnection_OnPresence;
            _xmppClientConnection.OnMessage += XmppClientConnection_OnMessage;

            //_xmppClientConnection.AutoRoster = true;
            _xmppClientConnection.UseSSL = false;
            _xmppClientConnection.UseStartTLS = true;
        }
        #endregion

        #region Methods
        public void Connect(string username, string password)
        {
            _xmppClientConnection.Open(username, password);
        }

        public void Dispose()
        {
            _xmppClientConnection.OnAuthError -= XmppClientConnection_OnAuthError;
            _xmppClientConnection.OnLogin -= XmppClientConnection_OnLogin;
            _xmppClientConnection.OnPresence -= XmppClientConnection_OnPresence;
            _xmppClientConnection.OnMessage -= XmppClientConnection_OnMessage;

            _xmppClientConnection.Close();
        }
        #endregion

        #region Event Handlers
        private void XmppClientConnection_OnLogin(object sender)
        {
            if (_xmppClientConnection.Authenticated)
                Presence = PresenceStatus.Online;
            else
                throw new AuthenticationException();
        }

        private void XmppClientConnection_OnAuthError(object sender, Element e)
        {
            throw new NotImplementedException();
        }

        private void XmppClientConnection_OnPresence(object sender, Presence pres)
        {
            if (PresenceUpdated != null)
                PresenceUpdated(this, new PresenceEventArgs(pres.From.User.Substring(1), FacebookConverter.ToPresenceStatus(pres.Type)));
        }

        private void XmppClientConnection_OnMessage(object sender, Message msg)
        {
            switch (msg.Chatstate)
            {
                case Chatstate.active:
                    if (String.IsNullOrEmpty(msg.Body))
                    {
                        if (IsTypingUpdated != null)
                            IsTypingUpdated(this, new IsTypingEventArgs(msg.From.User.Substring(1), false));
                    }
                    else
                    {
                        if (IsTypingUpdated != null)
                            IsTypingUpdated(this, new IsTypingEventArgs(msg.From.User.Substring(1), false));
                        if (MessageReceived != null)
                            MessageReceived(this, new MessageEventArgs(msg.From.User.Substring(1), msg.Body));
                    }
                    break;
                case Chatstate.composing:
                    if (IsTypingUpdated != null)
                        IsTypingUpdated(this, new IsTypingEventArgs(msg.From.User.Substring(1), true));
                    break;
                default:
                    throw new NotSupportedException("ChatState not supported: " + msg.Chatstate.ToString());
            }
        }
        #endregion
    }
}
