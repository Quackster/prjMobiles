using Squirtle.Game.Pathfinder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Room.Model
{
    public class RoomModel
    {
        private int model_id;
        private int start_x;
        private int start_y;
        private int start_z;
        private int start_rotation;
        private string heightmap;
        private string objects;

        public int ModelType { get { return model_id; } }
        public int StartX { get { return start_x; }  }
        public int StartY { get { return start_y; } }
        public int StartZ { get { return start_z; } }
        public int StartRotation { get { return start_rotation; } }
        public string Heightmap { get { return heightmap; } }
        public string Objects { get { return objects; }}

        public int[,] TileHeights;
        public bool[,] TileStates;

        public int MapSizeX { get; internal set; }
        public int MapSizeY { get; internal set; }

        /// <summary>
        /// Parse heightmap data
        /// </summary>
        public void ParseMap()
        {
            string[] lines = this.Heightmap.Split('|');

            this.MapSizeY = lines.Length;
            this.MapSizeX = lines[0].Length;

            this.TileStates = new Boolean[this.MapSizeX, this.MapSizeY];
            this.TileHeights = new int[this.MapSizeX, this.MapSizeY];

            for (int y = 0; y < this.MapSizeY; y++)
            {
                string line = lines[y];

                for (int x = 0; x < this.MapSizeX; x++)
                {
                    char tile = line.ToCharArray()[x];

                    if (Char.IsDigit(tile))
                    {
                        this.TileStates[x, y] = true;
                        this.TileHeights[x, y] = (tile - '0');
                    }
                    else
                    {
                        this.TileStates[x, y] = false;
                        this.TileHeights[x, y] = 0;
                    }

                    if (x == this.StartX && y == this.StartY)
                    {
                        this.TileStates[x, y] = true;
                        this.TileHeights[x, y] = StartZ;
                    }
                }
            }
        }

        /// <summary>
        /// Get if the position is valid for this heightmap.
        /// </summary>
        /// <param name="Position">the position</param>
        /// <returns>true, if successful</returns>
        public bool IsValidPosition(Position Position)
        {
            return (Position.X >= 0 && Position.Y >= 0 && Position.X < this.MapSizeX && Position.Y < this.MapSizeY);
        }
    }
}
