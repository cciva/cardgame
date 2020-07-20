using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using NLog;
using Game.Lib;

namespace CrazyCards
{
    public class Game
    {
        // flag to control game runtime
        private bool running = false;
        // simulate some delay to make it like real game
        private static readonly Random _delay = new Random(); 
        private readonly ILogger _logger = null;
        private readonly GameConfig _config = null;
        private readonly IGameCoordinator _coordinator = null;
        private ICollection<IPlayer> _players = null;

        public Game(GameConfig config, IGameCoordinator coordinator, ILogger logger = null)
        {
            _config = config;
            _coordinator = coordinator;
            _players = new Collection<IPlayer>();

            if(logger == null)
                _logger = LogManager.GetLogger("game");
            else
                _logger = logger;
        }

        public void Register(IPlayer player)
        {
            _players.Add(player);
        }

        public void Start()
        {
            Logger.Info("starting game...");
            running = true;
            GameLoop();
        }

        private void GameLoop()
        {
            Stage stage = Stage.Unknown;

            while (running)
            {
                foreach(IPlayer player in _players)
                {
                    Hand hand = Coordinator.PlayTurn(player, out stage);
                    Logger.Info("player '{0}' ({1} cards): {2}", player.Name, player.Stash.NumOwnedCards, hand.Card);

                    // end of the game
                    if(stage == Stage.GameEnd)
                    {
                        Logger.Info("game ended");
                        running = false;
                    }

                    Thread.Sleep(_delay.Next(300, 800));
                    
                    // end of round
                    if(stage == Stage.RoundEnd)
                    {
                        Result result = Coordinator.Eval();

                        if(result.Outcome == Outcome.Draw)
                        {
                            
                            Logger.Info("no winner in this round");
                        }
                        else
                        {
                            Hand winner = result.Winner;
                            Logger.Info("player '{0}' wins this round", winner.Player.Name);
                        }
                    }
                }
            }
        }

        public Stage GameStage
        {
            get { return Stage.Unknown; }
        }

        protected GameConfig Conf
        {
            get { return _config; }
        }

        protected ILogger Logger
        {
            get { return _logger; }
        }

        protected IGameCoordinator Coordinator
        {
            get { return _coordinator; }
        }
    }
}
