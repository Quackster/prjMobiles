using Squirtle.Game.Entity;
using Squirtle.Game.Pathfinder;
using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using Squirtle.Storage.Access;
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
        public void EnterRoom(IEntity entity)
        {
            if (entity.RoomUser.RoomId > 0)
                entity.RoomUser.Room.LeaveRoom(entity);

            var roomModel = RoomDao.GetModel(_roomData.ModelType);

            if (roomModel == null)
                return;

            entity.RoomUser.RoomId = _roomData.Id;
            entity.RoomUser.Position = new Position(roomModel.StartX, roomModel.StartY, roomModel.StartZ, roomModel.StartRotation);

            if (entity is Player player)
            {
                player.Send(new Response("HEIGHTMAP " + roomModel.Heightmap.Replace("|", "\r")));
                player.Send(new Response("OBJECTS " + _roomData.ModelType + roomModel.Objects));

                if (_entities.Count > 0)
                {
                    var users = Response.Init("USERS");
                    var statuses = Response.Init("STATUS");

                    foreach (var entityUser in _entities)
                    {
                        entityUser.RoomUser.AppendUserString(users);
                        entityUser.RoomUser.AppendStatusString(users);
                    }

                    player.Send(users);
                    player.Send(statuses);
                }

                _entities.Add(entity);

                var newUser = Response.Init("USERS");
                entity.RoomUser.AppendUserString(newUser);
                this.Send(newUser);

                var newStatus = Response.Init("STATUS");
                entity.RoomUser.AppendStatusString(newStatus);
                this.Send(newStatus);
            }
        }

        /// <summary>
        /// Handler for leaving room
        /// </summary>
        /// <param name="entity">leave room handler</param>
        public void LeaveRoom(IEntity entity)
        {
            _entities.Remove(entity);

            var response = Response.Init("LOGOUT");
            response.AppendNewArgument(entity.Details.Username);
            this.Send(response);
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
