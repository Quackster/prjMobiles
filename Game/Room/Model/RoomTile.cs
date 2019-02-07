using System;
using System.Collections.Generic;
using System.Text;
using Squirtle.Game.Entity;
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
        internal static bool IsValidTile(RoomInstance room, Position position, IEntity user)
        {
            if (!room.Model.IsValidPosition(position))
                return false;

            foreach (IEntity entity in room.Entities.ToArray())
            {
                if (entity.Details.Username == user.Details.Username)
                    continue;

                if (entity.RoomUser.Position.X == user.RoomUser.Position.X && entity.RoomUser.Position.Y == user.RoomUser.Position.Y)
                    continue;

                if (entity.RoomUser.Position.X == position.X && entity.RoomUser.Position.Y == position.Y)
                {
                    return false;
                }
            }

            Item item = room.Mapping.LocateItem(position.X, position.Y);

            if (item != null)
            {
                if (item.ClassName == "chair")
                    return true;
                else
                    return false;
            }

            return true;
        }
    }
}
