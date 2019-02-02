using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using System;

namespace Squirtle.Messages
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