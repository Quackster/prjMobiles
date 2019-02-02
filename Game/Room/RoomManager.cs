using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Room
{
    class RoomManager
    {
        private Room[] _rooms;

        public RoomManager()
        {
            _rooms = new Room[2];
            _rooms[0] = new Room();
            _rooms[1] = new Room();
        }
    }
}
