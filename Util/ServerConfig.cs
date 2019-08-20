using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace prjMobiles.Util
{
    class ServerConfig
    {
        private static ServerConfig _serverConfig;
        private Dictionary<string, string> _configValues;
        private string _configFileName = "MobilesConfig.xml";

        /// <summary>
        /// Attempt to read configuration file
        /// </summary>
        public void ReadConfig()
        {
            if (_configValues == null)
                _configValues = new Dictionary<string, string>();

            if (!File.Exists(_configFileName))
            {
                WriteConfig();
            }

            _configValues.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_configFileName);

            _configValues["mysql.hostname"] = xmlDoc.SelectSingleNode("//configuration/mysql/hostname").InnerText;
            _configValues["mysql.username"] = xmlDoc.SelectSingleNode("//configuration/mysql/username").InnerText;
            _configValues["mysql.password"] = xmlDoc.SelectSingleNode("//configuration/mysql/password").InnerText;
            _configValues["mysql.database"] = xmlDoc.SelectSingleNode("//configuration/mysql/database").InnerText;
            _configValues["mysql.port"] = xmlDoc.SelectSingleNode("//configuration/mysql/port").InnerText;

            _configValues["mysql.maxcon"] = xmlDoc.SelectSingleNode("//configuration/mysql/min_connections").InnerText;
            _configValues["mysql.mincon"] = xmlDoc.SelectSingleNode("//configuration/mysql/max_connections").InnerText;

            _configValues["server.ip"] = xmlDoc.SelectSingleNode("//configuration/server/ip").InnerText;
            _configValues["server.port"] = xmlDoc.SelectSingleNode("//configuration/server/port").InnerText;
        }

        /// <summary>
        /// Attempts to write configuration file
        /// </summary>
        private void WriteConfig()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("   ");
            settings.OmitXmlDeclaration = true;

            XmlWriter xmlWriter = XmlWriter.Create(_configFileName, settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("configuration");
            xmlWriter.WriteStartElement("mysql");

            xmlWriter.WriteStartElement("hostname");
            xmlWriter.WriteString("localhost");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("username");
            xmlWriter.WriteString("root");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("password");
            xmlWriter.WriteString("");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("database");
            xmlWriter.WriteString("prjmobiles");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("port");
            xmlWriter.WriteString("3306");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("min_connections");
            xmlWriter.WriteString("5");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("max_connections");
            xmlWriter.WriteString("5");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("server");

            xmlWriter.WriteStartElement("ip");
            xmlWriter.WriteString("127.0.0.1");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("port");
            xmlWriter.WriteString("91");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        /// <summary>
        /// Get string by key
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the string</returns>
        public string GetString(string key)
        {
            return _configValues.GetValueOrDefault(key);
        }

        /// <summary>
        /// Get integer by key
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the integer</returns>
        public int GetInt(string key)
        {
            int number = 0;
            int.TryParse(_configValues.GetValueOrDefault(key), out number);
            return number;
        }

        /// <summary>
        /// Create singleton instance
        /// </summary>
        public static void Create()
        {
            _serverConfig = new ServerConfig();
        }

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        public static ServerConfig Instance
        {
            get
            {
                return _serverConfig;
            }
        }
    }
}
