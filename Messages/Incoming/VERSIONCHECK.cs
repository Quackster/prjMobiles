using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using System;

namespace Squirtle.Messages
{
    public class VERSIONCHECK : IMessage
    {
        public void Handle(Player player, Request request)
        {
            if (request.GetArgument(0) == "client002" || request.GetArgument(0) == "client003")
            {
                player.Send(new Response("ENCRYPTION_OFF"));
                player.Send(new Response("SECRET_KEY\r1337"));
            }
        }
    }
}