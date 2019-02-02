using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squirtle.Game.Room;

namespace Squirtle.Game.Pathfinder
{
    static class Pathfinder
    {
        private static bool NoDiag = false;

        public static List<Position> FindPath(RoomInstance room, Position start, Position end)
        {
            List<Position> Path = new List<Position>();

            PathfinderNode Nodes = FindPathReversed(room, end, start);

            if (Nodes != null) // make sure we do have a path first
            {
                while (Nodes.Next != null)
                {
                    Path.Add(Nodes.Next.Position);
                    Nodes = Nodes.Next;
                }
            }

            // I need to change 'IsValidStep' to not count the position the user is on (the user who wants to walk) or the emulator will error..

            return Path;
        }

        private static PathfinderNode FindPathReversed(RoomInstance room, Position start, Position end)
        {
            MinHeap<PathfinderNode> OpenList = new MinHeap<PathfinderNode>(256);

            PathfinderNode[,] Map = new PathfinderNode[room.Model.MapSizeX, room.Model.MapSizeY];
            PathfinderNode Node;
            Position Tmp;
            int Cost;
            int Diff;

            PathfinderNode current = new PathfinderNode(start);
            current.Cost = 0;

            PathfinderNode Finish = new PathfinderNode(end);
            Map[current.Position.X, current.Position.Y] = current;
            OpenList.Add(current);

            while (OpenList.Count > 0)
            {
                current = OpenList.ExtractFirst();
                current.InClosed = true;

                for (int i = 0; NoDiag ? i < NoDiagMovePoints.Length : i < MovePoints.Length; i++)
                {
                    Tmp = current.Position + (NoDiag ? NoDiagMovePoints[i] : MovePoints[i]);
                    bool IsFinalMove = (Tmp.X == end.X && Tmp.Y == end.Y); // are we at the final position?

                    if (room.Mapping.IsValidStep(new Position(current.Position.X, current.Position.Y), Tmp, IsFinalMove)) // need to set the from positions
                    {
                        if (Map[Tmp.X, Tmp.Y] == null)
                        {
                            Node = new PathfinderNode(Tmp);
                            Map[Tmp.X, Tmp.Y] = Node;
                        }
                        else
                        {
                            Node = Map[Tmp.X, Tmp.Y];
                        }

                        if (!Node.InClosed)
                        {
                            Diff = 0;

                            if (current.Position.X != Node.Position.X)
                            {
                                Diff += 2;
                            }

                            if (current.Position.Y != Node.Position.Y)
                            {
                                Diff += 2;
                            }

                            Cost = current.Cost + Diff + Node.Position.GetDistanceSquared(end);

                            if (Cost < Node.Cost)
                            {
                                Node.Cost = Cost;
                                Node.Next = current;
                            }

                            if (!Node.InOpen)
                            {
                                if (Node.Equals(Finish))
                                {
                                    Node.Next = current;
                                    return Node;
                                }

                                Node.InOpen = true;
                                OpenList.Add(Node);
                            }
                        }
                    }
                }
            }

            return null;
        }

        private static Position[] MovePoints = new Position[]
        {
            new Position(-1, -1),
            new Position(0, -1),
            new Position(1, -1),
            new Position(1, 0),
            new Position(1, 1),
            new Position(0, 1),
            new Position(-1, 1),
            new Position(-1, 0)
        };

        private static Position[] NoDiagMovePoints = new Position[]
        {
            new Position(0, -1),
            new Position(1, 0),
            new Position(0, 1),
            new Position(-1, 0)
        };
    }
}
