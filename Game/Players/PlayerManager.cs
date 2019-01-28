using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Game.Players
{
    class PlayerManager
    {
        private static PlayerManager _playerManager;

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
