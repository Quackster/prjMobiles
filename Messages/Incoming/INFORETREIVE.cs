using prjMobiles.Game.Players;
using prjMobiles.Network.Streams;
using prjMobiles.Storage.Access;
using System;

namespace prjMobiles.Messages
{
    public class INFORETRIEVE : IMessage
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

            player.login(playerData, false);
        }
    }
}