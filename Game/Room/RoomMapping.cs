﻿using System;
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
            if (!_room.Model.IsValidPosition(position))
                return false;

            if (!_room.Model.IsValidPosition(tmp))
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

            return true;
        }
    }
}
