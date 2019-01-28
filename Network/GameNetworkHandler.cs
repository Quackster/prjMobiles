using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using log4net;
using Squirtle.Network.Streams;
using System;
using System.Text;

namespace Squirtle.Network
{
    internal class GameNetworkHandler : ChannelHandlerAdapter
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Handle client connections.
        /// </summary>
        /// <param name="ctx">the channel context</param>
        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            base.ChannelActive(ctx);

            log.Debug($"Client connected to server: {ctx.Channel.RemoteAddress}");
            ctx.Channel.WriteAndFlushAsync("#HELLO##");
        }

        /// <summary>
        /// Handle client disconnects.
        /// </summary>
        /// <param name="ctx">the channel context</param>
        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            base.ChannelInactive(ctx);

            log.Debug($"Client disconnected from server: {ctx.Channel.RemoteAddress}");
        }

        /// <summary>
        /// Handle incoming channel messages from the decoder
        /// </summary>
        /// <param name="ctx">the channel context</param>
        /// <param name="msg">the incoming message</param>
        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            if (msg is Request)
            {
                Request request = (Request)msg;
                log.Debug("Message received: " + request.Header + (request.Body.Length > 0 ? " / " + request.Body : ""));

                if (request.Header == "VERSIONCHECK")
                {
                    ctx.Channel.WriteAndFlushAsync("#ENCRYPTION_OFF##");
                    ctx.Channel.WriteAndFlushAsync("#SECRET_KEY\r1337##");
                }
            }

            base.ChannelRead(ctx, msg);
        }

        /// <summary>
        /// Handle channel read complete.
        /// </summary>
        /// <param name="context">the channel context</param>
        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        /// <summary>
        /// Handle exceptions thrown by the network api.
        /// </summary>
        /// <param name="context">the channel context</param>
        /// <param name="exception">the exception</param>
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) =>
            log.Error(exception.ToString());
    }
}