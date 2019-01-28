using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Squirtle.Network.Streams;

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

                if (message is string)
                {
                    context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes((string)message)));
                }

                if (message is Response)
                {
                    Response response = (Response)message;
                    context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes(response.GetMessage())));
                }
            }
            catch (Exception ex)
            {
                Squirtle.Logger.Error(ex);
            }
        }
    }
}