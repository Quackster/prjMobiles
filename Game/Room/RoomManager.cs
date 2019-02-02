using Squirtle.Storage.Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Room
{
    class RoomManager
    {
        private static RoomManager _roomManager;
        private List<Room> _rooms;

        /// <summary>
        /// Room manager constructor
        /// </summary>
        public RoomManager()
        {
            _rooms = RoomDao.GetRooms();
        }

        /// <summary>
        /// Get the room by room id
        /// </summary>
        /// <param name="roomId">the room id</param>
        /// <returns>the room instance</returns>
        public Room GetRoom(int roomId)
        {
            foreach (var room in _rooms)
            {
                if (room.Data.Id == roomId)
                    return room;
            }

            return null;
        }

        /// <summary>
        /// Invoke the singleton instance
        /// </summary>
        public static RoomManager Instance()
        {
            if (_roomManager == null)
                _roomManager = new RoomManager();

            return _roomManager;
        }
    }
}
