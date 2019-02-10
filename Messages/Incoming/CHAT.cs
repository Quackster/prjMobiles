using prjMobiles.Game.Players;
using prjMobiles.Network.Streams;
using System;

namespace prjMobiles.Messages
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