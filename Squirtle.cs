using log4net;
using log4net.Config;
using Squirtle.Network;
using System;
using System.IO;
using System.Reflection;

namespace Squirtle
{
    class Squirtle
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog Logger
        {
            get { return log; }
        }

        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            log.Info("Booting prjSquirtle - Written by Quackster");
            log.Info("Habbo Hotel 2001 emulation of V1");

            GameServer.Instance.InitialiseServer(37120);

            Console.Read();
        }
    }
}
