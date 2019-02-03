
using Dapper;
using Squirtle.Game.Entity;
using System;

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

        /// <summary>
        /// Get user details by name
        /// </summary>
        /// <param name="username">the user details</param>
        /// <returns>the details</returns>
        public static EntityData CheckExistingUser(string username, string email)
        {
            using (var connection = Database.Instance().GetConnection())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@username", username);
                queryParameters.Add("@email", email);

                return connection.QueryFirstOrDefault<EntityData>("SELECT * FROM users WHERE username = @username OR email = @email", queryParameters);
            }
        }

        /// <summary>
        /// Get user details by name
        /// </summary>
        /// <param name="username">the user details</param>
        /// <returns>the details</returns>
        public static void NewUser(string username, string password, string email, int age, int pants, int shirt, int head, string sex, string mission)
        {
            using (var connection = Database.Instance().GetConnection())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@username", username);
                queryParameters.Add("@password", password);
                queryParameters.Add("@email", email);
                queryParameters.Add("@age", age);
                queryParameters.Add("@pants", pants);
                queryParameters.Add("@shirt", shirt);
                queryParameters.Add("@head", head);
                queryParameters.Add("@sex", sex);
                queryParameters.Add("@mission", mission);
                queryParameters.Add("@created_at", DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                connection.Execute("INSERT INTO users (username, password, email, age, pants, shirt, head, sex, mission, created_at) VALUES (@username, @password, @email, @age, @pants, @shirt, @head, @sex, @mission, @created_at)", queryParameters);
            }
        }

        /// <summary>
        /// Update details
        /// </summary>
        /// <param name="username">the user details</param>
        /// <returns>the details</returns>
        public static void EditUser(string username, string password, string email, int age, int pants, int shirt, int head, string sex, string mission)
        {
            using (var connection = Database.Instance().GetConnection())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@username", username);
                queryParameters.Add("@password", password);
                queryParameters.Add("@email", email);
                queryParameters.Add("@age", age);
                queryParameters.Add("@pants", pants);
                queryParameters.Add("@shirt", shirt);
                queryParameters.Add("@head", head);
                queryParameters.Add("@sex", sex);
                queryParameters.Add("@mission", mission);

                connection.Execute("UPDATE users SET password = @password, email = @email, age = @age, pants = @pants, shirt = @shirt, head = @head, sex = @sex, mission = @mission WHERE username = @username", queryParameters);
            }
        }
    }
}
