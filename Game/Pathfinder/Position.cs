namespace Squirtle.Game.Pathfinder
{
    public class Position
    {
        public int X;
        public int Y;
        public int Z;
        public int BodyRotation;
        public int HeadRotation;

        /// <summary>
        /// Handler for getting and setting main rotation
        /// </summary>
        public int Rotation
        {
            set
            {
                BodyRotation = value;
                HeadRotation = value;
            } 
            get
            {
                return BodyRotation;
            }
        }

        /// <summary>
        /// Constructor for position class
        /// </summary>
        /// <param name="x">the x coord</param>
        /// <param name="y">the y coord</param>
        /// <param name="z">the z coord</param>
        /// <param name="rotation">the rotation</param>
        public Position(int x, int y, int z, int rotation)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.BodyRotation = rotation;
            this.HeadRotation = rotation;
        }
    }
}