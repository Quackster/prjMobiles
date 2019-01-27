using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;

namespace Squirtle.Network
{
    internal class GameNetworkHandler : ChannelHandlerAdapter
    {
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);

            Squirtle.Logger.Debug($"Client connected to client: {ctx.Channel.RemoteAddress}");
        }

        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            base.ChannelInactive(ctx);

            Squirtle.Logger.Debug($"Client disconnected from client: {ctx.Channel.RemoteAddress}");
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            IByteBuffer message = msg as IByteBuffer;
            base.ChannelRead(ctx, msg);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) =>
            Squirtle.Logger.Error(exception.ToString());
    }
}