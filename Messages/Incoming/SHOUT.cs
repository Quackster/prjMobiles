using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using System;

namespace Squirtle.Messages
{
    public class SHOUT : IMessage
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            var response = new Response("SHOUT");
            response.AppendNewArgument(player.Details.Username);
            response.AppendArgument(request.Body);
            player.RoomUser.Room.Send(response);
        }
    }
}