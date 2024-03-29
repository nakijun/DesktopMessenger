﻿using System;
using System.Diagnostics;
using System.Security;
using System.Security.Authentication;
using DesktopMessenger.Common;
using agsXMPP;
using agsXMPP.Net;
using agsXMPP.Xml.Dom;
using agsXMPP.protocol.client;
using agsXMPP.protocol.extensions.chatstates;

namespace DesktopMessenger.Facebook
{
    public class FacebookMessengerService : IMessengerService
    {
        #region Fields
        private readonly XmppClientConnection _xmppClientConnection;
        private Status _status;
        #endregion

        #region Events

        public event EventHandler<EventArgs> LoggedIn;
        public event EventHandler<StatusUpdatedEventArgs> StatusUpdated;
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<IsTypingEventArgs> IsTypingUpdated;
        #endregion

        #region Properties
        public string ServiceName { get { return "Facebook"; } }

        public Status Status
        {
            get { return _status; }
            set
            {
                switch (value)
                {
                    case Status.Online:
                        _xmppClientConnection.Send(new Presence(ShowType.chat, "Online") { Type = PresenceType.available });
                        _status = value;
                        break;
                }
            }
        }
        #endregion

        #region Constructor
        public FacebookMessengerService()
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
        public void Connect(string username, SecureString password)
        {
            _xmppClientConnection.Open(username, password.GetString());
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
            if (!_xmppClientConnection.Authenticated)
                throw new AuthenticationException();
            
            Status = Status.Online;
            if (LoggedIn != null)
                LoggedIn(this, EventArgs.Empty);
        }

        private void XmppClientConnection_OnAuthError(object sender, Element e)
        {
            throw new AuthenticationException();
        }

        private void XmppClientConnection_OnPresence(object sender, Presence pres)
        {
            if (StatusUpdated != null)
                StatusUpdated(this, new StatusUpdatedEventArgs(pres.From.User.Substring(1), FacebookConverter.ToPresenceStatus(pres.Type)));
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
