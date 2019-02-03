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

        private Timer _timer;
        private Bot _bot;
        private RoomInstance _room;

        /// <summary>
        /// Constructor for the entity task
        /// </summary>
        /// <param name="roomInstance">the room instance</param>
        public BotTask(RoomInstance roomInstance)
        {
            this._room = roomInstance;
        }

        /// <summary>
        /// Method for creating waitress
        /// </summary>
        public void CreateWaitress()
        {
            var details = new EntityData();
            details.Username = "Maarit";
            details.Head = 9;
            details.Shirt = 9;
            details.Pants = 9;
            details.Sex = 'M';
            details.Mission = "Ask me for a drink or say hi!";

            _bot = new Bot(details);
            _room.EnterRoom(_bot, new Position(13, 2, 0, 4));
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

            _timer = new Timer(new TimerCallback(Run), null, 0, 500);
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
            
        }

    }
}
