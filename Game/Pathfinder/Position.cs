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

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
            this.BodyRotation = 0;
            this.HeadRotation = 0;
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

        public Position(int x, int y, int z, int rotation, int bodyRotation) : this(x, y, z, rotation)
        {
            BodyRotation = bodyRotation;
        }

        // methods
        public int GetDistanceSquared(Position Point)
        {
            int dx = this.X - Point.X;
            int dy = this.Y - Point.Y;
            return (dx * dx) + (dy * dy);
        }

        public override bool Equals(object obj)
        {
            if (obj is Position)
            {
                Position v2d = (Position)obj;
                return v2d.X == this.X && v2d.Y == this.Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (X + " " + Y).GetHashCode();
        }

        public override string ToString()
        {
            return X + ", " + Y;
        }

        // operators

        /*public static bool operator ==(Position One, Position Two)
        {
            if (One is Position && Two is Position)
            {
                return One.Equals(Two);
            }

            return false;
        }

        public static bool operator !=(Position One, Position Two)
        {
            if (One is Position && Two is Position)
            {
                return !One.Equals(Two);
            }

            return false;
        }*/

        public static Position operator +(Position One, Position Two)
        {
            return new Position(One.X + Two.X, One.Y + Two.Y);
        }

        public static Position operator -(Position One, Position Two)
        {
            return new Position(One.X - Two.X, One.Y - Two.Y);
        }

        public Position Copy() => new Position(X, Y, Z, HeadRotation, BodyRotation);

        public static Position Zero = new Position(0, 0);
    }
}