using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Squirtle.Network.Streams;

namespace Squirtle.Network.Codec
{
    internal class NetworkEncoder : MessageToMessageEncoder<Response>
    {
        protected override void Encode(IChannelHandlerContext context, Response response, List<object> output)
        {
            try
            {
                context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes(response.GetMessage())));
            }
            catch (Exception ex)
            {
                Squirtle.Logger.Error(ex);
            }
        }
    }
}