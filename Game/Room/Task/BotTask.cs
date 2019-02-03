using log4net;
using Squirtle.Game.Bots;
using Squirtle.Game.Entity;
using Squirtle.Game.Pathfinder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Squirtle.Game.Room.Task
{
    class BotTask
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(EntityTask));
        private static Random _random;

        private Timer _timer;
        private Bot _bot;
        private RoomInstance _room;
    
        private List<Position> _walkingPositions;
        private long _walkingTimer;

        /// <summary>
        /// Constructor for the entity task
        /// </summary>
        /// <param name="roomInstance">the room instance</param>
        public BotTask(RoomInstance roomInstance)
        {
            _room = roomInstance;
            _random = new Random();
        }

        /// <summary>
        /// Method for creating waitress
        /// </summary>
        public void CreateWaitress()
        {
            _walkingPositions = new List<Position>();
            _walkingPositions.Add(new Position(14, 2));
            _walkingPositions.Add(new Position(13, 2));
            _walkingPositions.Add(new Position(12, 2));
            _walkingPositions.Add(new Position(11, 2));
            _walkingPositions.Add(new Position(10, 2));
            _walkingPositions.Add(new Position(9, 2));
            _walkingPositions.Add(new Position(8, 2));
            _walkingPositions.Add(new Position(10, 1));
            _walkingPositions.Add(new Position(11, 1));
            _walkingPositions.Add(new Position(12, 1));
            _walkingPositions.Add(new Position(13, 1));
            _walkingPositions.Add(new Position(14, 1));

            var details = new EntityData();
            details.Username = "Maarit";
            details.Head = 9;
            details.Shirt = 9;
            details.Pants = 9;
            details.Sex = 'M';
            details.Mission = "Ask me for a drink or say hi!";

            Position startPosition = _walkingPositions[_random.Next(0, this._walkingPositions.Count)];

            _bot = new Bot(details);
            _room.EnterRoom(_bot, new Position(startPosition.X, startPosition.Y, 0, _random.Next(0, 8)));

            _bot.RoomUser.Status.Add("stand", "");
            _bot.RoomUser.NeedsUpdate = true;
        }

        /// <summary>
        /// Handle disposting waitress
        /// </summary>
        public void DisposeWaitress()
        {
            if (_bot == null)
                return;

            _room.LeaveRoom(_bot);
        }

        /// <summary>
        /// Create the task for the room to handle bot
        /// </summary>
        public void CreateTask()
        {
            if (this._timer != null)
                return;

            _timer = new Timer(new TimerCallback(Run), null, 0, 1000);
        }

        /// <summary>
        /// Stops the task for the room to handle bot
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
            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > _walkingTimer)
            {
                _walkingTimer = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + _random.Next(3, 8);

                Position targetPosition = _walkingPositions[_random.Next(0, this._walkingPositions.Count)];

                if (targetPosition != null)
                    _bot.RoomUser.Move(targetPosition.X, targetPosition.Y);
            }
        }

    }
}
