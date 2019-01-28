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
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static MessageHandler _messageHandler;
        private Dictionary<string, IMessage> _messages;
  
        public MessageHandler()
        {
            _messages = new Dictionary<string, IMessage>();
            _messages.Add("VERSIONCHECK", new VERSIONCHECK());
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
                _log.Debug(string.Format("Message received: {0} {1}", request.Header, request.Body));

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
