using System;
using System.Collections.Generic;
using System.Text;
using Squirtle.Game.Items;
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

            Item item = room.Mapping.LocateItem(position.X, position.Y);

            if (item != null)
            {
                if (item.ClassName == "chair")
                    return true;
                else
                    return false;
            }

            // TODO: Entity checking
            return true;
        }
    }
}
