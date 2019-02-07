using log4net;
using Squirtle.Game.Entity;
using Squirtle.Network.Streams;
using System;
using System.Threading;

namespace Squirtle.Game.Room.Tasks
{
    public class DrinkTask
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(DrinkTask));
        private static Random _random;

        private Timer _timer;
        private RoomInstance _room;

        /// <summary>
        /// Constructor for the drink task
        /// </summary>
        /// <param name="roomInstance">the room instance</param>
        public DrinkTask(RoomInstance roomInstance)
        {
            _room = roomInstance;
        }

        /// <summary>
        /// Create the task for the room
        /// </summary>
        public void CreateTask()
        {
            if (this._timer != null)
                return;

            _timer = new Timer(new TimerCallback(Run), null, 0, 1000);
        }

        /// <summary>
        /// Stops the task for the room
        /// </summary>
        public void StopTask()
        {
            if (this._timer == null)
                return;

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timer = null;
        }

        /// <summary>
        /// Running task loop
        /// </summary>
        /// <param name="state">the state</param>
        private void Run(object state)
        {
            try
            {
                foreach (IEntity entity in _room.Entities.ToArray())
                {
                    if (entity.RoomUser.RoomId != _room.Data.Id)
                        continue;

                    if (entity.RoomUser.Status.ContainsKey("drink"))
                        this.ProcessUserDrink(entity);

                    if (entity.RoomUser.Status.ContainsKey("carryd"))
                        this.ProcessUserCarry(entity);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        /// <summary>
        /// Process drinking user
        /// </summary>
        /// <param name="entity">the entity</param>
        private void ProcessUserDrink(IEntity entity)
        {
            var statusData = entity.RoomUser.Status["drink"];

            long dateAdded = statusData.Item2;
            string statusValue = statusData.Item1;

            if (!(DateTimeOffset.UtcNow.ToUnixTimeSeconds() > dateAdded))
                return;

            entity.RoomUser.RemoveStatus("drink");
            entity.RoomUser.AddStatus("carryd", entity.RoomUser.LastDrink);


            var response = Response.Init("STATUS");
            entity.RoomUser.AppendStatusString(response);
            _room.Send(response);
        }

        /// <summary>
        /// Process drinking user
        /// </summary>
        /// <param name="entity">the entity</param>
        private void ProcessUserCarry(IEntity entity)
        {
            var statusData = entity.RoomUser.Status["carryd"];

            long dateAdded = statusData.Item2;
            string statusValue = statusData.Item1;

            if (!(DateTimeOffset.UtcNow.ToUnixTimeSeconds() > dateAdded))
                return;

            entity.RoomUser.RemoveStatus("carryd");
            entity.RoomUser.AddStatus("drink", "", 1);

            var response = Response.Init("STATUS");
            entity.RoomUser.AppendStatusString(response);
            _room.Send(response);
        }
    }
}
