using System;
using DotNetty.Transport.Channels;
using log4net;
using Squirtle.Game.Entity;
using Squirtle.Game.Room;
using Squirtle.Network.Streams;

namespace Squirtle.Game.Players
{
    public class Player : IEntity
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Player));

        private IChannel _channel;
        private EntityData _playerDetails;
        private RoomUser _roomUser;

        /// <summary>
        /// Gets the player channel.
        /// </summary>
        public IChannel Channel
        {
            get { return _channel; }
        }

        /// <summary>
        /// Get the ip address of the player connected.
        /// </summary>
        public string IpAddress
        {
            get { return _channel.RemoteAddress.ToString().Split(':')[3].Replace("]", ""); }
        }

        /// <summary>
        /// Gets the player data.
        /// </summary>
        public override EntityData Details
        {
            get { return _playerDetails; }
        }

        /// <summary>
        /// Gets the room user.
        /// </summary>
        public override RoomUser RoomUser
        {
            get { return _roomUser; }
        }

        /// <summary>
        /// Constructor for player.
        /// </summary>
        /// <param name="channel">the channel</param>
        public Player(IChannel channel)
        {
            _channel = channel;
        }

        /// <summary>
        /// Login handler for the user
        /// </summary>
        /// <param name="playerDetails">the player data</param>
        /// <param name="enterRoom">whether or not they enter room</param>
        public void login(EntityData playerDetails, bool enterRoom)
        {
            _playerDetails = playerDetails;
            _roomUser = new RoomUser(this);

            if (!enterRoom)
            {
                var response = Response.Init("USEROBJECT");
                response.AppendNewArgument(playerDetails.Username);
                response.AppendArgument(playerDetails.Password);
                response.AppendArgument(playerDetails.Email);
                response.AppendArgument(string.Format("{0},{1},{2}", playerDetails.Pants, playerDetails.Shirt, playerDetails.Head));
                response.AppendArgument("noidea");
                response.AppendArgument("x");
                response.AppendArgument(playerDetails.Sex.Equals("M") ? "Male" : "Female");
                response.AppendArgument("yes");
                response.AppendArgument(playerDetails.Age);
                response.AppendArgument(playerDetails.Mission);
                this.Send(response);
            }
            else
            {
                var room = RoomManager.Instance().GetRoom(1);

                if (room != null)
                    room.EnterRoom(this);
            }
        }

        /// <summary>
        /// Disconnection handler
        /// </summary>
        public void Disconnect()
        {
            if (_roomUser == null)
                return;

            var room = _roomUser.Room;

            if (room != null)
                room.LeaveRoom(this);
        }

        /// <summary>
        /// Send an object to the player's channel.
        /// </summary>
        /// <param name="obj">the object to send</param>
        public void Send(object obj)
        {
            if (obj == null)
                return;

            _channel.WriteAndFlushAsync(obj);

            if (obj is Response response)
                _log.Debug(string.Format("Sent: {0}{1}", response.Header, response.Body));
        }
    }
}
