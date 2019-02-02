using Squirtle.Game.Entity;
using Squirtle.Game.Pathfinder;
using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Room
{
    public class Room
    {
        private RoomData _roomData;
        private List<IEntity> _entities;

        /// <summary>
        /// Get the room data for this room
        /// </summary>
        public RoomData Data
        {
            get { return _roomData; }
        }

        /// <summary>
        /// Constructor for this room
        /// </summary>
        /// <param name="roomData">the room data</param>
        public Room(RoomData roomData)
        {
            _entities = new List<IEntity>();
            _roomData = roomData;
        }

        /// <summary>
        /// Handler for entering room.
        /// </summary>
        /// <param name="entity">the entity to login</param>
        public void enterRoom(IEntity entity)
        {
            entity.RoomUser.RoomId = _roomData.Id;
            entity.RoomUser.Position = new Position(_roomData.StartX, _roomData.StartY, _roomData.StartZ, _roomData.StartRotation);
            _entities.Add(entity);

            if (entity is Player player)
            {
                player.Send(new Response("HEIGHTMAP " + _roomData.Heightmap.Replace("|", "\r")));
                player.Send(new Response("OBJECTS " + _roomData.ModelType + _roomData.Objects));

                var users = Response.Init("USERS");

                foreach (var entityUser in _entities)
                    entityUser.RoomUser.appendUserString(users);

                player.Send(users);

                var newUser = Response.Init("USERS");
                entity.RoomUser.appendUserString(newUser);
                this.Send(newUser);
            }            
        }

        /// <summary>
        /// Send a packet to all users in the room.
        /// </summary>
        /// <param name="newUser"></param>
        private void Send(Response response)
        {
            foreach (var entity in _entities)
            {
                if (entity is Player player)
                    player.Send(response);
            }
        }
    }
}
