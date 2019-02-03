using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using System;

namespace Squirtle.Messages
{
    public class CHAT: IMessage
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            if (player.RoomUser.Room.Model.ModelType == 0)
            {
                if (player.RoomUser.Room.BotTask.HandleCommand(player, request.Body))
                {
                    return;
                }
            }

            var response = new Response("CHAT");
            response.AppendNewArgument(player.Details.Username);
            response.AppendArgument(request.Body);
            player.RoomUser.Room.Send(response);
        }
    }
}