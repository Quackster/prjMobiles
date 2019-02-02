using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Squirtle.Game.Room.Task
{
    class EntityTask
    {
        private RoomInstance _room;
        private Timer _timer;
        private RoomInstance roomInstance;

        /// <summary>
        /// Constructor for the entity task
        /// </summary>
        /// <param name="roomInstance">the room instance</param>
        public EntityTask(RoomInstance roomInstance)
        {
            this.roomInstance = roomInstance;
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
        public void Run(object state)
        {
            
        }
    }
}
