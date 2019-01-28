using Squirtle.Game.Players;
using Squirtle.Network.Streams;

namespace Squirtle.Messages
{
    public class VERSIONCHECK : IMessage
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new Response("ENCRYPTION_OFF"));
            player.Send(new Response("SECRET_KEY\r1337"));
        }
    }
}