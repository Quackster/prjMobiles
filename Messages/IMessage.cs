using prjMobiles.Game.Players;
using prjMobiles.Network.Streams;

namespace prjMobiles.Messages
{
    public interface IMessage
    {
        void Handle(Player player, Request request);
    }
}