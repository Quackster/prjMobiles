using prjMobiles.Game.Pathfinder;
using prjMobiles.Game.Players;
using prjMobiles.Network.Streams;
using System;

namespace prjMobiles.Messages
{
    public class LOOKTO : IMessage
    {
        public void Handle(Player player, Request request)
        {
            int x = int.Parse(request.GetArgument(0));
            int y = int.Parse(request.GetArgument(1));

            if (player.RoomUser.Status.ContainsKey("sit"))
                return;

            player.RoomUser.Position.Rotation = Rotation.CalculateDirection(player.RoomUser.Position.X, player.RoomUser.Position.Y, x, y);
            player.RoomUser.NeedsUpdate = true;
        }
    }
}