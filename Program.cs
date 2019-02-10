using log4net;
using log4net.Config;
using prjMobiles.Game.Players;
using prjMobiles.Game.Room;
using prjMobiles.Messages;
using prjMobiles.Network;
using prjMobiles.Storage;
using prjMobiles.Storage.Access;
using System;
using System.IO;
using System.Reflection;

namespace prjMobiles
{
    class prjMobiles
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get the logger instance.
        /// </summary>
        public static ILog Logger
        {
            get { return log; }
        }

        /// <summary>
        /// Main entry point for program
        /// </summary>
        /// <param name="args">the arguments specified when calling this program</param>
        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            Console.Title = "prjMobiles - Mobiles Disco Emulation";

            log.Info("Booting prjprjMobiles - Written by Quackster");
            log.Info("Emulation of Mobiles Disco created in 1999");

            Exception exception = null;
            log.Info("Attempting to connect to MySQL database");

            if (!Database.Instance().HasConnection(ref exception))
            {
                log.Fatal("Connection to database failed, could not start server");
                log.Error(exception.ToString());

                Console.Read();
                return;
            }
            else
            {
                log.Info("Connection to MySQL was successful!");
            }

            PlayerManager.Instance();

            RoomManager.Instance();
            RoomManager.Instance().LoadModels();
            RoomManager.Instance().LoadRooms();

            MessageHandler.Instance();

            GameServer.Instance().InitialiseServer(91);
            Console.Read();
        }
    }
}
