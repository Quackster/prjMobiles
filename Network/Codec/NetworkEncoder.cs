using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Squirtle.Network.Codec
{
    internal class NetworkEncoder : MessageToMessageEncoder<IByteBuffer>
    {
        protected override void Encode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            
        }
    }
}