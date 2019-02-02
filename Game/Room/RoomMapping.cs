using System;
using System.Collections.Generic;
using System.Text;
using Squirtle.Game.Pathfinder;

namespace Squirtle.Game.Room
{
    public class RoomMapping
    {
        private RoomInstance _room;

        /// <summary>
        /// Constructor for room mapping
        /// </summary>
        /// <param name="roomInstance">the room instance</param>
        public RoomMapping(RoomInstance roomInstance)
        {
            this._room = roomInstance;
        }

        /// <summary>
        /// Method to check if the next position is a valid step.
        /// </summary>
        /// <param name="position">the position from</param>
        /// <param name="tmp">the position to</param>
        /// <param name="isFinalMove">if the next position is final</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool IsValidStep(Position position, Position tmp, bool isFinalMove)
        {
            if (_room.Model.IsValidPosition(position))
                return false;

            if (_room.Model.IsValidPosition(tmp))
                return false;

            return true;
        }
    }
}
