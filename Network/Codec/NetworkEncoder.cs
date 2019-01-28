using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using log4net;
using Squirtle.Network.Streams;

namespace Squirtle.Network.Codec
{
    internal class NetworkEncoder : MessageToMessageEncoder<object>
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void Encode(IChannelHandlerContext context, object msg, List<object> output)
        {
            try
            {
                if (msg is Response)
                {
                    Response response = (Response)msg;
                    context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes(response.GetMessage())));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}