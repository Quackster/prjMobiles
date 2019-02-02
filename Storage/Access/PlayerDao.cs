
using Dapper;
using Squirtle.Game.Entity;

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
        public static EntityData TryLogin(string username, string password)
        {
            using (var connection = Database.Instance().GetConnection())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@username", username);
                queryParameters.Add("@password", password);

                return connection.QueryFirstOrDefault<EntityData>("SELECT * FROM users WHERE username = @username AND password = @password", queryParameters);
            }
        }
    }
}
