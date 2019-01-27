﻿using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using log4net;
using System;
using System.Text;

namespace Squirtle.Network
{
    internal class GameNetworkHandler : ChannelHandlerAdapter
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);

            log.Debug($"Client connected to server: {ctx.Channel.RemoteAddress}");
            ctx.Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes("#HELLO##")));
        }

        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            base.ChannelInactive(ctx);

            log.Debug($"Client disconnected from server: {ctx.Channel.RemoteAddress}");
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            if (msg is string)
            {
                string messageString = (string)msg;
                log.Debug("Message received: " + messageString);
            }

            base.ChannelRead(ctx, msg);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) =>
            log.Error(exception.ToString());
    }
}