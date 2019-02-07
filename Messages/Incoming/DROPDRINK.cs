using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using System;

namespace Squirtle.Messages
{
    public class DROPDRINK : IMessage
    {
        public void Handle(Player player, Request request)
        {
            if (!player.RoomUser.Status.ContainsKey("carryd"))
                return;

            player.RoomUser.RemoveStatus("carryd");
            player.RoomUser.NeedsUpdate = true;
        }
    }
}