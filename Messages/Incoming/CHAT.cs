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

            string talkMessage = request.Body;

            if (player.RoomUser.Room.Model.ModelType == 0)
                player.RoomUser.Room.BotTask.HandleCommand(player, request.Body);


            player.RoomUser.Talk(talkMessage);
        }
    }
}