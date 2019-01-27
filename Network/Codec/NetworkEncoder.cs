using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Squirtle.Network.Codec
{
    internal class NetworkEncoder : MessageToMessageEncoder<object>
    {
        protected override void Encode(IChannelHandlerContext context, object message, List<object> output)
        {
            try
            {
                if (message is byte[])
                {
                    byte[] messageData = (byte[])message;
                    context.WriteAndFlushAsync(Unpooled.CopiedBuffer(messageData));
                }
            }
            catch (Exception ex)
            {
                Squirtle.Logger.Error(ex);
            }
        }
    }
}