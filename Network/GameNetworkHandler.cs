using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Text;

namespace Squirtle.Network
{
    internal class GameNetworkHandler : ChannelHandlerAdapter
    {
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);

            Squirtle.Logger.Debug($"Client connected to server: {ctx.Channel.RemoteAddress}");
            ctx.Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes("#HELLO##")));
        }

        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            base.ChannelInactive(ctx);

            Squirtle.Logger.Debug($"Client disconnected from server: {ctx.Channel.RemoteAddress}");
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            base.ChannelRead(ctx, msg);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) =>
            Squirtle.Logger.Error(exception.ToString());
    }
}