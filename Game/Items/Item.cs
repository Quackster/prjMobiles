using prjMobiles.Game.Pathfinder;
using System;
using System.Collections.Generic;
using System.Text;

namespace prjMobiles.Game.Items
{
    public class Item
    {

        public string ClassName { get; set; }
        public Position Position { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }

        /// <summary>
        /// Constructor for item class
        /// </summary>
        /// <param name="className">the class name of the furni</param>
        /// <param name="x">the x coord</param>
        /// <param name="y">the y coord</param>
        /// <param name="z">the furni height</param>
        /// <param name="rotation">the furni rotation</param>
        public Item(string className, int x, int y, int z, int rotation)
        {
            ClassName = className;
            Position = new Position(x, y, z, rotation);

            if (className != "table")
            {
                Length = 1;
                Width = 1;
            }
            else
            {
                Length = 2;
                Width = 2;
            }
        }

        public List<Position> GetAffectedTiles()
        {
            int length = Length;
            int width = Width;

            var points = new List<Position>();

            if (Length != Width)
            {
                if (Position.Rotation == 0 || Position.Rotation == 4)
                {
                    int l = Length;
                    length = width;
                    width = l;
                }
            }

            for (int newX = Position.X; newX < Position.X + width; newX++)
            {
                for (int newY = Position.Y; newY < Position.Y + length; newY++)
                {
                    Position pos = new Position(newX, newY);
                    points.Add(pos);
                }
            }

            return points;
        }
    }
}
