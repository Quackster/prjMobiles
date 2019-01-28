using log4net;
using log4net.Config;
using Squirtle.Game.Players;
using Squirtle.Messages;
using Squirtle.Network;
using System;
using System.IO;
using System.Reflection;

namespace Squirtle
{
    class Squirtle
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

            Console.Title = "prjSquirtle - Habbo Hotel Emulation";

            log.Info("Booting prjSquirtle - Written by Quackster");
            log.Info("Habbo Hotel 2001 emulation of V1");

            PlayerManager.Instance();
            MessageHandler.Instance();

            GameServer.Instance().InitialiseServer(37120);
            Console.Read();
        }
    }
}
