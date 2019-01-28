using Squirtle.Game.Players;
using Squirtle.Network.Streams;
using System;

namespace Squirtle.Messages
{
    public class INFORETRIEVE : IMessage
    {
        public void Handle(Player player, Request request)
        {
            Console.WriteLine(request.GetArgument(0));
            Console.WriteLine(request.GetArgument(1));
        }
    }
}