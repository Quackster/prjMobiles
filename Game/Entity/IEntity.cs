using prjMobiles.Game.Players;
using prjMobiles.Game.Room;

namespace prjMobiles.Game.Entity
{
    public abstract class IEntity
    {
        public abstract EntityData Details { get; }
        public abstract RoomUser RoomUser { get; }
    }
}
