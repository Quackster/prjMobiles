using prjMobiles.Game.Items;
using prjMobiles.Game.Pathfinder;
using System;
using System.Collections.Generic;
using System.Text;

namespace prjMobiles.Game.Room.Model
{
    public class RoomModel
    {
        private readonly int model_id;
        private readonly int start_x;
        private readonly int start_y;
        private readonly int start_z;
        private readonly int start_rotation;
        private readonly string heightmap;
        private readonly string objects;

        public RoomModel(int model_id, int start_x, int start_y, int start_z, int start_rotation, string heightmap, string objects)
        {
            this.model_id = model_id;
            this.start_x = start_x;
            this.start_y = start_y;
            this.start_z = start_z;
            this.start_rotation = start_rotation;
            this.heightmap = heightmap;
            this.objects = objects;
        }

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
        /// Parse database items
        /// </summary>
        /// <returns>list of item instances</returns>
        public List<Item> ParseItems()
        {
            var itemList = new List<Item>();

            if (this.Objects.Length > 0)
            {
                foreach (var furniLine in this.Objects.Split('\r'))
                {
                    string[] data = furniLine.Split(' ');
                    itemList.Add(new Item(data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]), int.Parse(data[5])));
                }
            }

            return itemList;
        }

        /// <summary>
        /// Get if the position is valid for this heightmap.
        /// </summary>
        /// <param name="Position">the position</param>
        /// <returns>true, if successful</returns>
        public bool IsValidPosition(Position Position)
        {
            if (Position.X >= 0 && Position.Y >= 0 && Position.X < this.MapSizeX && Position.Y < this.MapSizeY)
            {
                return TileStates[Position.X, Position.Y];
            }

            return false;
        }
    }
}
