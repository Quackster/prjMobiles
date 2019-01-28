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
                Response response = Response.Init("ENCRYPTION_OFF");
                player.Send(response);

                response = Response.Init("SECRET_KEY");
                response.AppendNewArgument("1337");
                player.Send(response);
            }
        }
    }
}