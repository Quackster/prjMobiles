﻿using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Squirtle.Network
{
    class GameServer
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static GameServer gameServer;
   
        private MultithreadEventLoopGroup bossGroup;
        private MultithreadEventLoopGroup workerGroup;

        public static GameServer Instance
        {
            get
            {
                if (gameServer == null)
                    gameServer = new GameServer();

                return gameServer;
            }
        }

        public GameServer()
        {
            this.bossGroup = new MultithreadEventLoopGroup(1);
            this.workerGroup = new MultithreadEventLoopGroup(10);
        }

        public void InitialiseServer(int port)
        {
            try
            {
                ServerBootstrap bootstrap = new ServerBootstrap()
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .ChildHandler(new GameChanelInitializer())/*new ActionChannelInitializer<IChannel>(channel =>
                        channel.Pipeline.AddLast("gameEncoder", new NetworkEncoder()),
                        channel.Pipeline.AddLast("ClientHandler", new GameNetworkHandler())
                    ))*/
                    .ChildOption(ChannelOption.TcpNodelay, true)
                    .ChildOption(ChannelOption.SoKeepalive, true)
                    .ChildOption(ChannelOption.SoReuseaddr, true)
                    .ChildOption(ChannelOption.SoRcvbuf, 1024)
                    .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024))
                    .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default);

                bootstrap.BindAsync(port);
                log.Info($"Server is now listening on port: {port}!");
            }
            catch (Exception e)
            {
                log.Error($"Failed to setup network listener... {e}");
            }
        }
    }
}
