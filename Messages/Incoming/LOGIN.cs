using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using Squirtle.Storage.Access;
using System;

namespace Squirtle.Messages
{
    public class LOGIN : IMessage
    {
        public void Handle(Player player, Request request)
        {
            var playerData = PlayerDao.TryLogin(request.GetArgument(0), request.GetArgument(1));

            if (playerData == null)
            {
                var response = new Response("ERROR");
                response.AppendArgument("login in");
                player.Send(response);
                return;
            }

            player.login(playerData, true);
        }
    }
}