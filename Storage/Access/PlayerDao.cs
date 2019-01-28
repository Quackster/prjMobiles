using Squirtle.Game.Players;
using System;
using System.Collections.Generic;
using System.Text;

using Dapper;
using Dapper.Contrib.Extensions;

namespace Squirtle.Storage.Access
{
    class PlayerDao
    {
        /// <summary>
        /// Get if the login was successful
        /// </summary>
        /// <param name="username">the username requested</param>
        /// <param name="password">the password request</param>
        /// <returns>the login data, null if login was invalid</returns>
        public static PlayerData TryLogin(string username, string password)
        {
            using (var connection = Database.Instance().GetConnection())
            {
                return connection.QueryFirstOrDefault<PlayerData>("SELECT * FROM users WHERE username = @username", new { username });
            }
        }
    }
}
