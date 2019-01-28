using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Squirtle.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Network
{
    internal class NetworkDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.ReadableBytes < 4)
                return;

            int messageLength = int.Parse(Encoding.Default.GetString(input.ReadBytes(4).Array));
            string messageBody = Encoding.Default.GetString(input.ReadBytes(messageLength).Array);
            string messageHeader = null;

            if (messageBody.Contains(" "))
            {
                messageHeader = messageBody.Split(' ')[0];
                messageBody = messageBody.Substring(messageHeader.Length + 1);
            }
            else
            {
                messageHeader = messageBody;
            }

            output.Add(new Request(messageHeader, messageBody));
        }
    }
}