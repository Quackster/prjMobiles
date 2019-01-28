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
            if (PlayerDao.TryLogin(request.GetArgument(0), request.GetArgument(1)) == null)
            {
                Console.WriteLine("PlayerDao.TryLogin - Invalid");
            }
        }
    }
}