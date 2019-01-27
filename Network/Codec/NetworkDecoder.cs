using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Network
{
    internal class NetworkDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            Console.WriteLine("test 123");
            if (input.ReadableBytes < 4)
                return;

            int messageLength = int.Parse(Encoding.Default.GetString(input.ReadBytes(4).Array));
            string messageBody = Encoding.Default.GetString(input.ReadBytes(messageLength).Array);

            output.Add(messageBody);
        }
    }
}