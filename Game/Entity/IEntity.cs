using Squirtle.Game.Players;
using Squirtle.Game.Room;

namespace Squirtle.Game.Entity
{
    public abstract class IEntity
    {
        public abstract EntityData Details { get; }
        public abstract RoomUser RoomUser { get; }
    }
}
