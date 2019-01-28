using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Players
{
    class PlayerData
    {
        private int id;
        private string username;
        private string password;
        private int rank;
        private long join_date;
        private long last_online;
        private string email;
        private string mission;
        private string personal_greeting;
        private string figure;
        private string pool_figure;
        private string credits;
        private string tickets;
        private string sex;
        private string country;
        private string badge;
        private string birthday;
        private bool has_logged_in;

        /// <summary>
        /// Get the user id.
        /// </summary>
        public int Id
        {
            set { id = value; }
            get { return id; }
        }

        /// <summary>
        /// Get the user name.
        /// </summary>
        public string Name
        {
            set { username = value; }
            get { return username; }
        }
    }
}
