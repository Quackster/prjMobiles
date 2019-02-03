using System;
using System.Collections.Generic;
using System.Text;
using Squirtle.Game.Entity;
using Squirtle.Game.Items;
using Squirtle.Game.Pathfinder;
using Squirtle.Game.Room.Model;

namespace Squirtle.Game.Room
{
    public class RoomMapping
    {
        private RoomInstance _room;
        private List<Item> _items;

        /// <summary>
        /// Constructor for room mapping
        /// </summary>
        /// <param name="roomInstance">the room instance</param>
        public RoomMapping(RoomInstance roomInstance)
        {
            this._room = roomInstance;
            this._items = roomInstance.Model.ParseItems();
        }

        /// <summary>
        /// Method to check if the next position is a valid step.
        /// </summary>
        /// <param name="position">the position from</param>
        /// <param name="tmp">the position to</param>
        /// <param name="isFinalMove">if the next position is final</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool IsValidStep(IEntity entity, Position position, Position tmp, bool isFinalMove)
        {
            if (!RoomTile.IsValidTile(_room, position, entity))
                return false;

            if (!RoomTile.IsValidTile(_room, tmp, entity))
                return false;

            // Block stairwell in model 1
            if (_room.Model.ModelType == 1)
            {
                if ((position.X == 3 && position.Y == 14) && (tmp.X == 2 && tmp.Y == 15))
                    return false;

                if ((position.X == 2 && position.Y == 15) && (tmp.X == 3 && tmp.Y == 14))
                    return false;
            }

            // Block stairwell in model 1
            if (_room.Model.ModelType == 1)
            {
                if ((position.X == 13 && position.Y == 2) && (tmp.X == 14 && tmp.Y == 1))
                    return false;

                if ((position.X == 14 && position.Y == 1) && (tmp.X == 13 && tmp.Y == 2))
                    return false;
            }

            int oldHeight = _room.Model.TileHeights[position.X, position.Y];
            int newHeight = _room.Model.TileHeights[tmp.X, tmp.Y];

            if (Math.Abs(oldHeight - newHeight) > 1)
                return false; // Can't go higher than 1 square

            if (!isFinalMove)
            {
                Item item = this.LocateItem(tmp.X, tmp.Y);

                if (item != null && item.ClassName == "chair")
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Find an item by given coordinates.
        /// </summary>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <returns>the item instance</returns>
        public Item LocateItem(int x, int y)
        {
            foreach (Item item in _items)
            {
                if (item.Position.X == x && item.Position.Y == y)
                    return item;

                foreach (var tile in item.GetAffectedTiles())
                {
                    if (tile.X == x && tile.Y == y)
                    {
                        return item;
                    }
                }
            }

            return null;
        }
    }
}
