using Squirtle.Game.Players;
using Squirtle.Network.Streams;

namespace Squirtle.Messages
{
    public interface IMessage
    {
        void Handle(Player player, Request request);
    }
}