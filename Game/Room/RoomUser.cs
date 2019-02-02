using Squirtle.Game.Entity;
using Squirtle.Game.Pathfinder;
using Squirtle.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Room
{
    public class RoomUser
    {
        /// <summary>
        /// Get the room id the user is in.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Get the entity
        /// </summary>
        public IEntity Entity { get; set; }

        /// <summary>
        /// Get the position of this room user.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Get the room instance the user is currently in.
        /// </summary>
        public Room Room { get { return RoomManager.Instance().GetRoom(this.RoomId); } }

        /// <summary>
        /// Constructor for room user.
        /// </summary>
        /// <param name="entity">the entity that goes into the room user</param>
        public RoomUser(IEntity entity)
        {
            this.Entity = entity;
        }

        /// <summary>
        /// Append the room user details to the response
        /// </summary>
        /// <param name="response">the response</param>
        public void appendUserString(Response response)
        {
            response.AppendNewArgument(this.Entity.Details.Username);
            response.AppendArgument(string.Format("{0},{1},{2}", this.Entity.Details.Pants, this.Entity.Details.Shirt, this.Entity.Details.Head));
            response.AppendArgument(this.Position.X);
            response.AppendArgument(this.Position.Y);
            response.AppendArgument(this.Position.Z);
            response.AppendArgument(this.Entity.Details.Mission);
        }
    }
}
