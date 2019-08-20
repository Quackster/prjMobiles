using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using log4net;
using prjMobiles.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace prjMobiles.Network
{
    class GameServer
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static GameServer _gameServer;
   
        private MultithreadEventLoopGroup bossGroup;
        private MultithreadEventLoopGroup workerGroup;

        /// <summary>
        /// GameServer constructor
        /// </summary>
        public GameServer()
        {
            this.bossGroup = new MultithreadEventLoopGroup(1);
            this.workerGroup = new MultithreadEventLoopGroup(10);
        }

        /// <summary>
        /// Initialise the game server by given pot
        /// </summary>
        /// <param name="port">the game port</param>
        public void InitialiseServer()
        {
            try
            {
                ServerBootstrap bootstrap = new ServerBootstrap()
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .ChildHandler(new GameChannelInitializer())/*new ActionChannelInitializer<IChannel>(channel =>
                        channel.Pipeline.AddLast("gameEncoder", new NetworkEncoder()),
                        channel.Pipeline.AddLast("ClientHandler", new GameNetworkHandler())
                    ))*/
                    .ChildOption(ChannelOption.TcpNodelay, true)
                    .ChildOption(ChannelOption.SoKeepalive, true)
                    .ChildOption(ChannelOption.SoReuseaddr, true)
                    .ChildOption(ChannelOption.SoRcvbuf, 1024)
                    .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024))
                    .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default);

                bootstrap.BindAsync(IPAddress.Parse(ServerConfig.Instance.GetString("server.ip")), ServerConfig.Instance.GetInt("server.port"));
                log.Info($"Server is now listening on port: {ServerConfig.Instance.GetInt("server.port")}!");
            }
            catch (Exception e)
            {
                log.Error($"Failed to setup network listener... {e}");
            }
        }

        /// <summary>
        /// Invoke the singleton instance
        /// </summary>
        public static GameServer Instance
        {
            get
            {
                return _gameServer;
            }
        }

        /// <summary>
        /// Create new singleton instance
        /// </summary>
        public static void Create()
        {
            _gameServer = new GameServer();
        }
    }
}
