using prjMobiles.Game.Players;
using prjMobiles.Network.Streams;
using System;

namespace prjMobiles.Messages
{
    public class STATUSOK : IMessage
    {
        public void Handle(Player player, Request request)
        {
            var response = Response.Init("OK");
            player.Send(response);
        }
    }
}