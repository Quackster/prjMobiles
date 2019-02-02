using Squirtle.Game.Entity;
using Squirtle.Game.Pathfinder;
using Squirtle.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Squirtle.Game.Room.Task
{
    public class EntityTask
    {
        private Timer _timer;
        private RoomInstance _room;

        /// <summary>
        /// Constructor for the entity task
        /// </summary>
        /// <param name="roomInstance">the room instance</param>
        public EntityTask(RoomInstance roomInstance)
        {
            this._room = roomInstance;
        }

        /// <summary>
        /// Create the task for the room to handle walking
        /// </summary>
        public void CreateTask()
        {
            if (this._timer != null)
                return;

            this._timer = new Timer(new TimerCallback(Run), null, 500, 500);
        }

        /// <summary>
        /// Stops the task for the room to handle walking
        /// </summary>
        public void StopTask()
        {
            if (this._timer == null)
                return;

            this._timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Run method called every 500ms
        /// </summary>
        /// <param name="state">whatever this means??</param>
        private void Run(object state)
        {
            var entityUpdates = new List<IEntity>();

            foreach (IEntity entity in this._room.Entities)
            {
                this.ProcessUser(entity);

                if (entity.RoomUser.NeedsUpdate)
                {
                    entity.RoomUser.NeedsUpdate = false;
                    entityUpdates.Add(entity);
                }
            }

            if (entityUpdates.Count > 0)
            {
                var response = Response.Init("STATUS");

                foreach (var entityUser in entityUpdates)
                    entityUser.RoomUser.AppendStatusString(response);

                this._room.Send(response);
            }
        }

        /// <summary>
        /// Process user inside room
        /// </summary>
        /// <param name="entity">the entity to process</param>
        private void ProcessUser(IEntity entity)
        {
            Position position = entity.RoomUser.Position;
            Position goal = entity.RoomUser.Goal;

            if (entity.RoomUser.IsWalking)
            {
                if (entity.RoomUser.NextPosition != null)
                {
                    entity.RoomUser.Position.X = entity.RoomUser.NextPosition.X;
                    entity.RoomUser.Position.Y = entity.RoomUser.NextPosition.Y;
                    // TODO: Height
                }

                if (entity.RoomUser.PathList.Count > 0)
                {
                    Position next = entity.RoomUser.PathList[0];
                    entity.RoomUser.PathList.Remove(next);

                    int rotation = Rotation.CalculateDirection(position.X, position.Y, next.X, next.Y);
                    double height = 0;

                    if (entity.RoomUser.Status.ContainsKey("mv"))
                        entity.RoomUser.Status.Remove("mv");

                    entity.RoomUser.Position.Rotation = rotation;
                    entity.RoomUser.Status.Add("mv", string.Format("{0},{1},{2}", next.X, next.Y, height));
                    entity.RoomUser.NextPosition = next;

                } else
                {
                    entity.RoomUser.StopWalking();
                }

                entity.RoomUser.NeedsUpdate = true;
            }
        }
    }

    internal class Rotation
    {
        internal static int CalculateDirection(int x, int y, int to_x, int to_y)
        {
            if (x == to_x)
            {
                if (y < to_y)
                    return 4;
                else
                    return 0;
            }
            else if (x > to_x)
            {
                if (y == to_y)
                    return 6;
                else if (y < to_y)
                    return 5;
                else
                    return 7;
            }
            else
            {
                if (y == to_y)
                    return 2;
                else if (y < to_y)
                    return 3;
                else
                    return 1;
            }
        }
    }
}
