using Dapper;
using Squirtle.Game.Room;
using System.Collections.Generic;

namespace Squirtle.Storage.Access
{
    class RoomDao
    {
        /// <summary>
        /// Get the list of rooms in the database
        /// </summary>
        /// <returns>the list of rooms</returns>
        public static List<Room> GetRooms()
        {
           var rooms = new List<Room>();

            using (var connection = Database.Instance().GetConnection())
            {
                var roomDataList = connection.Query<RoomData>("SELECT * FROM rooms").AsList();

                foreach (var roomData in roomDataList)
                    rooms.Add(new Room(roomData));
            }

            return rooms;
        }
    }
}
