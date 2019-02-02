using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Room
{
    class RoomModel
    {
        private int model_type;
        private int start_x;
        private int start_y;
        private int start_z;
        private int start_rotation;
        private string heightmap;
        private string objects;

        public int ModelType { get { return model_type; } set { model_type = value; } }
        public int StartX { get { return start_x; } set { start_x = value; } }
        public int StartY { get { return start_y; } set { start_y = value; } }
        public int StartZ { get { return start_z; } set { start_y = value; } }
        public int StartRotation { get { return start_rotation; } set { start_rotation = value; } }
        public string Heightmap { get { return heightmap; } set { heightmap = value; } }
        public string Objects { get { return objects; } set { objects = value; } }
    }
}
