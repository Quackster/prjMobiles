using prjMobiles.Game.Entity;
using prjMobiles.Game.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace prjMobiles.Game.Bots
{
    class Bot : IEntity
    {
        private EntityData _entityDetails;
        private RoomUser _roomUser;

        /// <summary>
        /// Gets the player data.
        /// </summary>
        public override EntityData Details
        {
            get { return _entityDetails; }
        }

        /// <summary>
        /// Gets the room user.
        /// </summary>
        public override RoomUser RoomUser
        {
            get { return _roomUser; }
        }

        /// <summary>
        /// Constructor for entity
        /// </summary>
        /// <param name="entityDetails">the details</param>
        public Bot(EntityData entityDetails)
        {
            _entityDetails = entityDetails;
            _roomUser = new RoomUser(this);
        }
    }
}
