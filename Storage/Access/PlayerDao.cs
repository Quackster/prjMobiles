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
        public static List<PlayerData> GetAllMyTable()
        {
            using (var connection = Database.Instance().GetConnection())
            {
                return connection.Query<PlayerData>("SELECT * FROM users").AsList();
            }
        }
    }
}
