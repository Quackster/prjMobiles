using DotNetty.Transport.Channels;
using log4net;
using Squirtle.Network.Streams;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Squirtle.Game.Players
{
    public class Player
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IChannel _channel;

        /// <summary>
        /// Gets the player channel
        /// </summary>
        public IChannel Channel
        {
            get { return _channel; }
        }

        /// <summary>
        /// Constructor for player
        /// </summary>
        /// <param name="channel">the channel</param>
        public Player(IChannel channel)
        {
            _channel = channel;
        }

        /// <summary>
        /// Send an object to the player's channel
        /// </summary>
        /// <param name="obj">the object to send</param>
        public void Send(object obj)
        {
            if (obj == null)
                return;

            _channel.WriteAndFlushAsync(obj);

            Response response = null;

            if (obj is Response)
                response = (Response)obj;

            _log.Debug(string.Format("Sent: {0} {1}", response.Header, response.Body));
        }
    }
}
