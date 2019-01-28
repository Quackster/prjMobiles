using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Players
{
    public class Player
    {
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
            _channel.WriteAndFlushAsync(obj);
        }
    }
}
