using Squirtle.Game.Bots;
using Squirtle.Game.Entity;
using Squirtle.Game.Items;
using Squirtle.Game.Pathfinder;
using Squirtle.Game.Room.Model;
using Squirtle.Network.Streams;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Room
{
    public class RoomUser
    {
        /// <summary>
        /// Get the room id the user is in.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Get the entity
        /// </summary>
        public IEntity Entity { get; set; }

        /// <summary>
        /// Get the position of this room user.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Get the goal position of this room user.
        /// </summary>
        public Position Goal { get; set; }

        /// <summary>
        /// Get the room instance the user is currently in.
        /// </summary>
        public RoomInstance Room { get { return RoomManager.Instance().GetRoom(this.RoomId); } }

        /// <summary>
        /// Get the status handling, the value is the value string and the time it was added.
        /// </summary>
        public ConcurrentDictionary<String, Tuple<string, long>> Status { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public bool NeedsUpdate { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public bool IsWalking { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public Position NextPosition { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public List<Position> PathList { get; set; }

        /// <summary>
        /// Constructor for room user.
        /// </summary>
        /// <param name="entity">the entity that goes into the room user</param>
        public RoomUser(IEntity entity)
        {
            this.Entity = entity;
            this.Reset();
        }
        
        /// <summary>
        /// Reset details when moving between rooms
        /// </summary>
        public void Reset()
        {
            this.PathList = new List<Position>();

            if (this.Status == null)
                this.Status = new ConcurrentDictionary<String, Tuple<string, long>>();
            else
            {
                foreach (var key in Status.Keys)
                {
                    if (key != "carryd" && key != "dance")
                        RemoveStatus(key);
                }
            }

            this.IsWalking = false;
            this.NextPosition = null;
            this.RoomId = 0;
        }

        /// <summary>
        /// Request move handler
        /// </summary>
        /// <param name="x">x coord goal</param>
        /// <param name="y">y coord goal</param>
        public void Move(int x, int y)
        {
            if (this.Room == null)
                return;

            if (this.NextPosition != null)
            {
                var oldPosition = this.NextPosition.Copy();
                this.Position.X = oldPosition.X;
                this.Position.Y = oldPosition.Y;
                this.Position.Z = this.Room.Model.TileHeights[oldPosition.X, oldPosition.Y];
                this.NeedsUpdate = true;
            }

            this.Goal = new Position(x, y);

            if (!RoomTile.IsValidTile(this.Room, this.Goal, this.Entity))
                return;

            var pathList = Pathfinder.Pathfinder.FindPath(this.Entity, this.Room, this.Position, this.Goal);

            if (pathList == null)
                return;

            if (pathList.Count > 0)
            {
                this.PathList = pathList;
                this.IsWalking = true;
            }
        }

        /// <summary>
        /// Stopped walking handler
        /// </summary>
        public void StopWalking()
        {
            if (!this.IsWalking)
                return;

            this.IsWalking = false;
            this.PathList.Clear();
            this.NeedsUpdate = true;
            this.NextPosition = null;
            this.RemoveStatus("mv");

            if (this.Entity is Bot)
                this.Room.BotTask.StoppedWalking();

            Item item = this.Room.Mapping.LocateItem(Position.X, Position.Y);

            if (item != null)
            {
                this.AddStatus("sit", "1");
                this.Position.Rotation = item.Position.Rotation;
                this.NeedsUpdate = true;
            }

            if (Room.Model.ModelType == 1)
            {
                if (Position.X == 0 && Position.Y == 7)
                {
                    RoomManager.Instance().GetRoom(1).EnterRoom(this.Entity, new Position(15, 18, 0, 7, 6));
                    return;
                }
            }


            if (Room.Model.ModelType == 0)
            {
                if (Position.X == 16 && Position.Y == 18)
                {
                    RoomManager.Instance().GetRoom(2).EnterRoom(this.Entity, new Position(1, 7, 0, 3, 2));
                    return;
                }
            }

            Console.WriteLine("Coords: " + Position.X + " / " + Position.Y);

        }

        /// <summary>
        /// Append the room user details to the response
        /// </summary>
        /// <param name="response">the response</param>
        public void AppendUserString(Response response)
        {
            response.AppendNewArgument(this.Entity.Details.Username);
            response.AppendArgument(string.Format("{0},{1},{2}", this.Entity.Details.Pants, this.Entity.Details.Shirt, this.Entity.Details.Head));
            response.AppendArgument(this.Position.X);
            response.AppendArgument(this.Position.Y);
            response.AppendArgument(this.Position.Z);
            response.AppendArgument(this.Entity.Details.Mission);
        }

        /// <summary>
        /// Append the status string to the response
        /// </summary>
        /// <param name="response">the response</param>
        public void AppendStatusString(Response response, bool isDrinking = false)
        {
            response.AppendNewArgument(this.Entity.Details.Username);
            response.AppendArgument(string.Format("{0},{1},{2},{3},{4}/", this.Position.X, this.Position.Y, this.Position.Z, this.Position.HeadRotation, this.Position.BodyRotation));

            foreach (var kvp in Status)
            {
                if (isDrinking && kvp.Key == "carryd")
                {
                    response.Append("drink");
                }
                else
                {
                    response.Append(kvp.Key);

                    string statusValue = kvp.Value.Item1;

                    if (statusValue.Length > 0)
                    {
                        response.Append(" ");
                        response.Append(statusValue);
                    }

                }
                response.Append("/");
            }

            response.Append((char)13);
        }

        /// <summary>
        /// Adds a status with a key and value, along with the int64 time of when the status was added.
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        public void AddStatus(string key, string value)
        {
            this.RemoveStatus(key);
            Status.TryAdd(key, Tuple.Create(value, DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        }

        /// <summary>
        /// Removes a status by its given key
        /// </summary>
        /// <param name="key">the key to check for</param>
        public void RemoveStatus(string key)
        {
            if (Status.ContainsKey(key))
                this.Status.Remove(key, out _);
        }
    }
}
