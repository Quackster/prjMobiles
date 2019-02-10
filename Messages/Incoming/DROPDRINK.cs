using prjMobiles.Game.Players;
using prjMobiles.Network.Streams;
using System;

namespace prjMobiles.Messages
{
    public class DROPDRINK : IMessage
    {
        public void Handle(Player player, Request request)
        {
            if (!player.RoomUser.Status.ContainsKey("carryd") && !player.RoomUser.Status.ContainsKey("drink"))
                return;

            player.RoomUser.RemoveStatus("carryd");
            player.RoomUser.RemoveStatus("drink");
            player.RoomUser.NeedsUpdate = true;
        }
    }
}