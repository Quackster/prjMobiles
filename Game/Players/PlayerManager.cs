using System;
using System.Collections.Generic;
using System.Text;

namespace prjMobiles.Game.Players
{
    class PlayerManager
    {
        private static PlayerManager _playerManager;
        private Dictionary<string, Player> _players;

        public Dictionary<string, Player> Players { get { return _players; } }

        /// <summary>
        /// Player manager constructor
        /// </summary>
        public PlayerManager()
        {
            _players = new Dictionary<string, Player>();
        }

        /// <summary>
        /// Invoke the singleton instance
        /// </summary>
        public static PlayerManager Instance()
        {
            if (_playerManager == null)
                _playerManager = new PlayerManager();

            return _playerManager;
        }
    }
}
