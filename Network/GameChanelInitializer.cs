using DotNetty.Transport.Channels;
using Squirtle.Network.Codec;
using System;

namespace Squirtle.Network
{
    internal class GameChanelInitializer : ChannelInitializer<IChannel>
    {
        protected override void InitChannel(IChannel channel)
        {
            IChannelPipeline pipeline = channel.Pipeline;
            pipeline.AddLast("gameEncoder", new NetworkEncoder());
            pipeline.AddLast("gameDecoder", new NetworkDecoder());
            pipeline.AddLast("clientHandler", new GameNetworkHandler());
        }
    }
}