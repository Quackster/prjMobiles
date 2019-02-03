using log4net;

using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Messages
{
    class MessageHandler
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MessageHandler));

        private static MessageHandler _messageHandler;
        private Dictionary<string, IMessage> _messages;
  
        public MessageHandler()
        {
            _messages = new Dictionary<string, IMessage>();
            _messages.Add("LOGIN", new LOGIN());
            _messages.Add("STATUSOK", new STATUSOK());
            _messages.Add("Move", new Move());
            _messages.Add("INFORETRIEVE", new INFORETRIEVE());
            _messages.Add("UPDATE", new UPDATE());
            _messages.Add("REGISTER", new REGISTER());
            _messages.Add("SHOUT", new SHOUT());
            _messages.Add("CHAT", new CHAT());

            //[2019-02-03 13:21:32,984] DEBUG  Squirtle.Messages.MessageHandler - Received: UPDATE 123 you@domain.com 2,1,14 noidea x Male no 45 Alex the best
        }

        /// <summary>
        /// Process incoming packet request
        /// </summary>
        /// <param name="player">the player to process the packet for</param>
        /// <param name="request">the request sent by client</param>
        public void ProcessRequest(Player player, Request request)
        {
            try
            {
                _log.Debug(string.Format("Received: {0} {1}", request.Header, request.Body));

                if (_messages.ContainsKey(request.Header))
                    _messages[request.Header].Handle(player, request);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        /// <summary>
        /// Invoke the singleton instance
        /// </summary>
        public static MessageHandler Instance()
        {
            if (_messageHandler == null)
                _messageHandler = new MessageHandler();

            return _messageHandler;
        }
    }
}
