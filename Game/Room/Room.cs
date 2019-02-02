using Squirtle.Game.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Room
{
    class Room
    {
        private List<IEntity> _entities;

        public Room()
        {
            _entities = new List<IEntity>();
        }
    }
}
