using prjMobiles.Game.Players;
using prjMobiles.Network.Streams;
using System;

namespace prjMobiles.Messages
{
    public class Move : IMessage
    {
        public void Handle(Player player, Request request)
        {
            int X = int.Parse(request.GetArgument(0));
            int Y = int.Parse(request.GetArgument(1));

            player.RoomUser.Move(X, Y);
        }
    }
}