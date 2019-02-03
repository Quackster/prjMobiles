using log4net;
using Squirtle.Game.Entity;
using Squirtle.Game.Pathfinder;
using Squirtle.Network.Streams;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Squirtle.Game.Room.Tasks
{
    public class EntityTask
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(EntityTask));

        private Timer _timer;
        private RoomInstance _room;

        private long _nextDrinkTime;
        private int _requiresUpdate = 2;

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

            _timer = new Timer(new TimerCallback(Run), null, 0, 500);
        }

        /// <summary>
        /// Stops the task for the room to handle walking
        /// </summary>
        public void StopTask()
        {
            if (this._timer == null)
                return;

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timer = null;
        }


        /// <summary>
        /// Run method called every 500ms
        /// </summary>
        /// <param name="state">whatever this means??</param>
        private void Run(object state)
        {
            try
            {
                bool isDrinkTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() > _nextDrinkTime;

                if (isDrinkTime)
                    _nextDrinkTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 12;

                var entityUpdates = new List<IEntity>();

                List<IEntity> copy;

                lock (_room.Entities)
                    copy = new List<IEntity>(_room.Entities);

                foreach (IEntity entity in copy)
                {
                    if (entity.RoomUser.RoomId != _room.Data.Id)
                        continue;

                    ProcessUser(entity);

                    if (entity.RoomUser.NeedsUpdate || (isDrinkTime && entity.RoomUser.Status.ContainsKey("carryd") || _requiresUpdate == 2))
                    {
                        entity.RoomUser.NeedsUpdate = false;
                        entityUpdates.Add(entity);
                    }
                }

                if (entityUpdates.Count > 0)
                {
                    var response = Response.Init("STATUS");

                    foreach (var entityUser in entityUpdates)
                        entityUser.RoomUser.AppendStatusString(response, isDrinkTime && _requiresUpdate == 0);

                    _room.Send(response);
                }

                if (isDrinkTime)
                {
                    _requiresUpdate = 2;
                }
                else
                {
                    if (_requiresUpdate > 0)
                        _requiresUpdate--;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
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
                    entity.RoomUser.Position.Z = _room.Model.TileHeights[entity.RoomUser.Position.X, entity.RoomUser.Position.Y];
                }

                if (entity.RoomUser.PathList.Count > 0)
                {
                    Position next = entity.RoomUser.PathList[0];
                    entity.RoomUser.PathList.Remove(next);

                    int rotation = Rotation.CalculateDirection(position.X, position.Y, next.X, next.Y);
                    double height = _room.Model.TileHeights[next.X, next.Y];

                    if (entity.RoomUser.Status.ContainsKey("mv"))
                        entity.RoomUser.Status.Remove("mv");

                    if (entity.RoomUser.Status.ContainsKey("sit"))
                        entity.RoomUser.Status.Remove("sit");

                    if (entity.RoomUser.Status.ContainsKey("stand"))
                        entity.RoomUser.Status.Remove("stand");

                    if (entity.RoomUser.Status.ContainsKey("taked"))
                        entity.RoomUser.Status.Remove("taked");

                    if (entity.RoomUser.Status.ContainsKey("gived"))
                        entity.RoomUser.Status.Remove("gived");

                    entity.RoomUser.Position.Rotation = rotation;
                    entity.RoomUser.Status.Add("mv", string.Format("{0},{1},{2}", next.X, next.Y, height));
                    entity.RoomUser.NextPosition = next;

                }
                else
                {
                    entity.RoomUser.StopWalking();
                }

                entity.RoomUser.NeedsUpdate = true;
            }
        }
    }
}
