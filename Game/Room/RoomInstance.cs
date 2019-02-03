using Squirtle.Game.Entity;
using Squirtle.Game.Pathfinder;
using Squirtle.Game.Players;
using Squirtle.Game.Room.Model;
using Squirtle.Game.Room.Task;
using Squirtle.Network.Streams;
using System;
using System.Collections.Generic;

namespace Squirtle.Game.Room
{
    public class RoomInstance
    {
        private RoomData _roomData;
        private RoomMapping _roomMapping;
        private EntityTask _entityTask;

        private bool _isActive;
        private List<IEntity> _entities;

        /// <summary>
        /// Get the room data for this room
        /// </summary>
        public RoomMapping Mapping
        {
            get { return _roomMapping; }
        }

        /// <summary>
        /// Get the room data for this room
        /// </summary>
        public RoomData Data
        {
            get { return _roomData; }
        }

        /// <summary>
        /// Get the room model data for this room
        /// </summary>
        public RoomModel Model
        {
            get { return RoomManager.Instance().GetModel(_roomData.ModelType); }
        }

        /// <summary>
        /// Get the list of players
        /// </summary>
        public List<Player> Players
        {
            get
            {
                var players = new List<Player>();

                foreach (var entity in _entities)
                {
                    if (entity is Player player)
                        players.Add(player);
                }

                return players;
            }
        }

        /// <summary>
        /// Get the list of entities in the room
        /// </summary>
        public List<IEntity> Entities { get { return _entities; } }

        /// <summary>
        /// Constructor for this room
        /// </summary>
        /// <param name="roomData">the room data</param>
        public RoomInstance(RoomData roomData)
        {
            _entities = new List<IEntity>();
            _roomData = roomData;
            _roomMapping = new RoomMapping(this);
            _entityTask = new EntityTask(this);
        }

        /// <summary>
        /// Handler for entering room.
        /// </summary>
        /// <param name="entity">the entity to login</param>
        public void EnterRoom(IEntity entity)
        {
            if (entity.RoomUser.RoomId > 0)
                entity.RoomUser.Room.LeaveRoom(entity);

            var roomModel = this.Model;

            if (roomModel == null)
                return;

            Response response = null;

            entity.RoomUser.RoomId = _roomData.Id;
            entity.RoomUser.Position = new Position(roomModel.StartX, roomModel.StartY, roomModel.StartZ, roomModel.StartRotation);

            if (entity is Player player)
            {
                player.Send(Response.Init("HEIGHTMAP" + (char)13 + roomModel.Heightmap.Replace("|", "\r")));
                player.Send(Response.Init("OBJECTS " + _roomData.ModelType + (char)13 + roomModel.Objects));

                if (_entities.Count > 0)
                {
                    var users = Response.Init("USERS");
                    var statuses = Response.Init("STATUS");

                    foreach (var entityUser in _entities)
                    {
                        entityUser.RoomUser.AppendUserString(users);
                        entityUser.RoomUser.AppendStatusString(statuses);
                    }

                    player.Send(users);
                    player.Send(statuses);
                }

                _entities.Add(entity);

                response = Response.Init("USERS");
                entity.RoomUser.AppendUserString(response);
                this.Send(response);

                response = Response.Init("STATUS");
                entity.RoomUser.AppendStatusString(response);
                this.Send(response);

                if (!_isActive)
                    StartRoom();

                /*spot1
spot1 is the spotlight on the left-hand side of the room.

spot1 move 11 8 2 5000 - Makes the spotlight point at x(11) y(8) z(2) with 5000 millisecond transition time from old position. First move starts near 7 7 0.
spot1 follow User - Points the spotlight at User in the room
spot1 off - Burns out the spotlight
spot1 on - Switches the spotlight back on
spot2
See spot1 above. spot2 is the spotlight on the right-hand side of the room.

mirrorball1
mirrorball1 is the disco ball hanging from the ceiling in the middle of the room.

mirrorball1 on - Starts rotating and reflecting light onto walls
mirrorball1 off - Disengages from above action
ambient1
ambient1 is the color overlay for the entire room.

ambient1 fade 255 255 255 9000 - Sets the amount of color to remove from red, green and blue color channels with 9000 millisecond transition time from old setting. Setting this for the first time disregards transition time.*/

                /*player.Send(new Response("SHOWPROGRAM\rspot2 move " + roomModel.StartX + " " + roomModel.StartY + " " + roomModel.StartZ + " 5000 "));
                player.Send(new Response("SHOWPROGRAM\rmirrorball1 on"));
                player.Send(new Response("SHOWPROGRAM\rambient1 fade " + random.Next(100, 130) + " " + +random.Next(30, 100) + " " + random.Next(0, 50) + " 1000"));*/
            }
        }

        /// <summary>
        /// Method called for when the room first starts
        /// </summary>
        private void StartRoom()
        {
            this._entityTask.CreateTask();
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

            this._isActive = this.Players.Count > 0;

            if (!this._isActive)
                this._entityTask.StopTask();
        }

        /// <summary>
        /// Send a packet to all users in the room.
        /// </summary>
        /// <param name="newUser"></param>
        public void Send(Response response)
        {
            foreach (var player in Players)
            {
                player.Send(response);
            }
        }
    }
}
