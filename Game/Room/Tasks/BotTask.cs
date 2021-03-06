﻿using log4net;
using prjMobiles.Game.Bots;
using prjMobiles.Game.Entity;
using prjMobiles.Game.Pathfinder;
using prjMobiles.Game.Players;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace prjMobiles.Game.Room.Tasks
{
    public class BotTask
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(BotTask));
        private static Random _random;

        private Timer _timer;
        private Bot _bot;
        private RoomInstance _room;
        private Player _currentCustomer;

        private bool FridgeDrinkGrab;
        private bool GiveDrinkPlayer;

        private List<Position> _walkingPositions;
        private long _walkingTimer;
        private string _drinkRequest;

        private string[] _allowedDrinks = new string[]
        {
            "disco mix",
            "whisky mix",
            "whisky",
            "whiskey mix",
            "whiskey",
            "beer",
            "vodka"
        };

        /// <summary>
        /// Constructor for the entity task
        /// </summary>
        /// <param name="roomInstance">the room instance</param>
        public BotTask(RoomInstance roomInstance)
        {
            _room = roomInstance;
            _random = new Random();
        }

        /// <summary>
        /// Method for creating waitress
        /// </summary>
        public void CreateWaitress()
        {
            _walkingPositions = new List<Position>();
            _walkingPositions.Add(new Position(14, 2));
            _walkingPositions.Add(new Position(13, 2));
            _walkingPositions.Add(new Position(12, 2));
            _walkingPositions.Add(new Position(11, 2));
            _walkingPositions.Add(new Position(10, 2));
            _walkingPositions.Add(new Position(9, 2));
            _walkingPositions.Add(new Position(8, 2));

            _walkingPositions.Add(new Position(10, 1));
            _walkingPositions.Add(new Position(11, 1));
            _walkingPositions.Add(new Position(12, 1));
            _walkingPositions.Add(new Position(13, 1));
            _walkingPositions.Add(new Position(14, 1));

            var details = new EntityData();
            details.Username = "Maarit";
            details.Head = 9;
            details.Shirt = 9;
            details.Pants = 9;
            details.Sex = 'M';
            details.Mission = "Ask me for a drink or say hi!";

            Position startPosition = _walkingPositions[_random.Next(0, this._walkingPositions.Count)];

            _bot = new Bot(details);
            _room.EnterRoom(_bot, new Position(startPosition.X, startPosition.Y, 0, _random.Next(0, 8)));

            _bot.RoomUser.AddStatus("stand", "");
            _bot.RoomUser.NeedsUpdate = true;
        }

        /// <summary>
        /// Handle disposting waitress
        /// </summary>
        public void DisposeWaitress()
        {
            if (_bot == null)
                return;

            _room.LeaveRoom(_bot);
        }

        /// <summary>
        /// Create the task for the room to handle bot
        /// </summary>
        public void CreateTask()
        {
            if (this._timer != null)
                return;

            _timer = new Timer(new TimerCallback(Run), null, 0, 1000);
        }

        /// <summary>
        /// Stops the task for the room to handle bot
        /// </summary>
        public void StopTask()
        {
            if (this._timer == null)
                return;

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timer = null;
        }

        /// <summary>
        /// Running task loop
        /// </summary>
        /// <param name="state">the state</param>
        private void Run(object state)
        {
            try
            {
                if (_currentCustomer != null)
                {
                    if ((_currentCustomer.RoomUser.RoomId != _room.Data.Id) || !IsCustomerWaiting(_currentCustomer) || _currentCustomer.RoomUser.IsDrinking)
                    {
                        FridgeDrinkGrab = false;
                        GiveDrinkPlayer = false;
                        _currentCustomer = null;
                    }
                }

                if (_currentCustomer == null)
                {
                    var customer = this.FindCustomer();

                    if (customer != null)
                    {
                        if (IsFacingCustomer(customer))
                        {
                            int direction = Rotation.CalculateDirection(_bot.RoomUser.Position.X, _bot.RoomUser.Position.Y, customer.RoomUser.Position.X, customer.RoomUser.Position.Y);

                            if (direction != _bot.RoomUser.Position.Rotation)
                            {
                                _bot.RoomUser.Position.Rotation = direction;
                                _bot.RoomUser.NeedsUpdate = true;
                                _currentCustomer = customer;
                            }
                        }
                        else
                        {
                            if (_bot.RoomUser.Position.X != customer.RoomUser.Position.X || _bot.RoomUser.Position.Y != 2)
                                _bot.RoomUser.Move(customer.RoomUser.Position.X, 2);
                        }
                    }
                    else
                    {
                        if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > _walkingTimer)
                        {
                            _walkingTimer = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + _random.Next(3, 10);

                            Position targetPosition = _walkingPositions[_random.Next(0, this._walkingPositions.Count)];

                            if (targetPosition != null)
                                _bot.RoomUser.Move(targetPosition.X, targetPosition.Y);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        /// <summary>
        /// Handler for when the bot stops walking
        /// </summary>
        public void StoppedWalking()
        {
            if (_currentCustomer == null)
            {
                _bot.RoomUser.AddStatus("stand", "");
                _bot.RoomUser.NeedsUpdate = true;
                return;
            }

            if (FridgeDrinkGrab)
            {
                if (_bot.RoomUser.Position.X == 8 && _bot.RoomUser.Position.Y == 2)
                    Task.Delay(500).ContinueWith(t => PerformFridgeGrab());
            }

            if (GiveDrinkPlayer)
                Task.Delay(1000).ContinueWith(t => PerformGiveDrink());
        }

        /// <summary>
        /// Handle bot commands
        /// </summary>
        /// <param name="from">the character it said from</param>
        /// <param name="command">the actual command</param>
        public void HandleCommand(Player from, string command)
        {
            if (!IsFacingCustomer(from))
                return;

            if ((_currentCustomer != from) && _currentCustomer != null)
                return;

            if (FridgeDrinkGrab || GiveDrinkPlayer)
                return;


            foreach (string drink in _allowedDrinks)
            {
                if (command.ToLower().Contains(drink.ToLower()))
                {
                    _drinkRequest = drink;
                    this.StartServingCustomer();
                    break;
                }
            }
        }

        /// <summary>
        /// Initiates serving customer once asked for a drink
        /// </summary>
        private void StartServingCustomer()
        {
            int walkX = 8;
            int walkY = 2;

            Task.Delay(500).ContinueWith(t => _bot.RoomUser.Talk(string.Format("{0} tulossa.", _drinkRequest.ToLower())));

            FridgeDrinkGrab = true;
            _bot.RoomUser.Move(walkX, 2);

            if (_bot.RoomUser.Position.X == walkX && _bot.RoomUser.Position.Y == walkY)
                Task.Delay(500).ContinueWith(t => PerformFridgeGrab());
            else
                _bot.RoomUser.Move(walkX, 2);

        }

        /// <summary>
        /// Peform action to grab from fridge
        /// </summary>
        public void PerformFridgeGrab()
        {
            if (!FridgeDrinkGrab || _currentCustomer == null)
                return;

            _bot.RoomUser.RemoveStatus("stand");
            _bot.RoomUser.AddStatus("taked", "");
            _bot.RoomUser.Position.Rotation = 0;
            _bot.RoomUser.NeedsUpdate = true;

            GiveDrinkPlayer = true;
            FridgeDrinkGrab = false;

            Task.Delay(1000).ContinueWith(t => PerformGiveDrink());
        }

        /// <summary>
        /// Perform give drink action to the other user
        /// </summary>
        public void PerformGiveDrink()
        {
            if (!GiveDrinkPlayer || _currentCustomer == null)
                return;

            if (!IsFacingCustomer(_currentCustomer))
            {
                _bot.RoomUser.Move(_currentCustomer.RoomUser.Position.X, 2);
                return;
            }
            else
            {
                int direction = Rotation.CalculateDirection(_bot.RoomUser.Position.X, _bot.RoomUser.Position.Y, _currentCustomer.RoomUser.Position.X, _currentCustomer.RoomUser.Position.Y);

                _bot.RoomUser.Position.Rotation = direction;
                _bot.RoomUser.RemoveStatus("stand");
                _bot.RoomUser.RemoveStatus("taked");
                _bot.RoomUser.AddStatus("gived", "");
                _bot.RoomUser.NeedsUpdate = true;

                Task.Delay(1000).ContinueWith(t => RemoveGiveDrink());

            }
        }

        /// <summary>
        /// Perform remove drink and add drink to the other hand, the end of the transaction
        /// </summary>
        private void RemoveGiveDrink()
        {
            _bot.RoomUser.RemoveStatus("gived");
            _bot.RoomUser.AddStatus("stand", "");
            _bot.RoomUser.NeedsUpdate = true;

            _currentCustomer.RoomUser.AddStatus("carryd", _drinkRequest);
            _currentCustomer.RoomUser.NeedsUpdate = true;

            GiveDrinkPlayer = false;
        }

        /// <summary>
        /// Locates a customer, if found one
        /// </summary>
        /// <returns>the player, if found</returns>
        public Player FindCustomer()
        {
            foreach (IEntity entity in _room.Entities.ToArray())
            {
                if (entity is Player player)
                {
                    if (entity.RoomUser.IsDrinking)
                        continue;

                    if (IsCustomerWaiting(player))
                        return player;
                }
            }

            return null;
        }

        /// <summary>
        /// Get if Maarit is facing the customer
        /// </summary>
        /// <param name="player">the player to check for</param>
        /// <returns>true, if successful</returns>
        public bool IsFacingCustomer(Player player)
        {
            if (IsCustomerWaiting(player))
            {
                if ((_bot.RoomUser.Position.X == player.RoomUser.Position.X) && _bot.RoomUser.Position.Y == 2)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get if the customer is waiting
        /// </summary>
        /// <param name="player">the packer to check</param>
        /// <returns>true, if successful</returns>
        public bool IsCustomerWaiting(Player player)
        {
            return (player.RoomUser.Position.X >= 8 && player.RoomUser.Position.X <= 14 && (player.RoomUser.Position.Y == 4));
        }
    }
}
