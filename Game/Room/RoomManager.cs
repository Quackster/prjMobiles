using Squirtle.Game.Room.Model;
using Squirtle.Storage.Access;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Room
{
    class RoomManager
    {
        private static RoomManager _roomManager;

        private List<RoomInstance> _rooms;
        private Dictionary<int, RoomModel> _roomModels;

        /// <summary>
        /// Room manager constructor
        /// </summary>
        public RoomManager()
        {
            _rooms = RoomDao.GetRooms();
            _roomModels = RoomDao.GetModels();

            foreach (var kvp in _roomModels)
                kvp.Value.ParseMap();
        }

        /// <summary>
        /// Get the room by room id
        /// </summary>
        /// <param name="roomId">the room id</param>
        /// <returns>the room instance</returns>
        public RoomInstance GetRoom(int roomId)
        {
            foreach (var room in _rooms)
            {
                if (room.Data.Id == roomId)
                    return room;
            }

            return null;
        }

        /// <summary>
        /// Get the room model by id
        /// </summary>
        /// <param name="typeId">the type id</param>
        /// <returns>the room model instance</returns>
        public RoomModel GetModel(int typeId)
        {
            if (_roomModels.ContainsKey(typeId))
                return _roomModels[typeId];

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
