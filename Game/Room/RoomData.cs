using System;
using System.Collections.Generic;
using System.Text;

namespace prjMobiles.Game.Room
{
    public class RoomData
    {
        private int id;
        private string name;
        private int model_type;
        /*private int start_x;
        private int start_y;
        private int start_z;
        private int start_rotation;
        private string heightmap;
        private string objects;*/

        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int ModelType { get { return model_type; } set { model_type = value; } }
        /*public int StartX { get { return start_x; } set { start_x = value; } }
        public int StartY { get { return start_y; } set { start_y = value; } }
        public int StartZ { get { return start_z; } set { start_y = value; } }
        public int StartRotation { get { return start_rotation; } set { start_rotation = value; } }
        public string Heightmap { get { return heightmap; } set { heightmap = value; } }
        public string Objects { get { return objects; } set { objects = value; } }*/
    }
}
/*using System;
using System.Collections.Generic;
using System.Text;

namespace prjMobiles.Game.Room
{
    public class RoomData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelType { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int StartZ { get; set; }
        public int StartRotation { get; set; }
        public string Heightmap { get; set; }
        public string Objects { get; set; }
    }
}*/
