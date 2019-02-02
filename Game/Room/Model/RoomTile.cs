using System;
using System.Collections.Generic;
using System.Text;
using Squirtle.Game.Pathfinder;

namespace Squirtle.Game.Room.Model
{
    class RoomTile
    {
        /// <summary>
        /// Method to see if a tile is valid or not to be walked on
        /// </summary>
        /// <param name="room">the room instance</param>
        /// <param name="position">the tile coordinate</param>
        /// <returns></returns>
        internal static bool IsValidTile(RoomInstance room, Position position)
        {
            if (!room.Model.IsValidPosition(position))
                return false;

            // TODO: Entity checking
            return true;
        }
    }
}
