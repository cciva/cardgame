using System.Collections.Generic;
using Game.Lib;
using Autofac;

namespace CrazyCards
{
    internal class GameServices
    {
        private static IContainer _container = null;
        private static GameServices _self = null;
        private static readonly object _sync = new object();

        private GameServices() {  }
        static GameServices() 
        {
            Preload();
        }

        private static void Preload()
        {
            var builder = new ContainerBuilder();

            // config
            builder.Register(c => new ConfReader().ReadConf<GameConfig>("game.config"))                
                .AsSelf()
                .SingleInstance();

            // game coordinator
            builder.RegisterType<GameCoordinator>()                
                .As<IGameCoordinator>()
                .SingleInstance();

            // game coordinator
            builder.RegisterType<Dealer>()                
                .As<IDealer>()
                .SingleInstance();


            // stash (draw pile, discard pile...)
            // initialized for each player
            builder.RegisterType<Stash>()                
                .As<IStash>();

            // player
            builder.RegisterType<Player>()
                .As<IPlayer>();

            // game
            builder.RegisterType<Game>()
                .AsSelf();        

            _container = builder.Build();
        }

        internal static GameServices Default
        {
            get 
            {
                lock(_sync)
                {
                    if(_self == null)
                        _self = new GameServices();
                }

                return _self;
            }
        }

        public IPlayer CreatePlayer(string name)
        {
            return _container.Resolve<IPlayer>(new NamedParameter("name", name));
        }

        public IDealer Dealer
        {
            get { return _container.Resolve<IDealer>(); }
        }

        public Game NewGame()
        {
            return _container.Resolve<Game>();
        }

        public GameConfig Conf
        {
            get { return _container.Resolve<GameConfig>(); }
        }

        public IGameCoordinator Coordinator
        {
            get { return _container.Resolve<IGameCoordinator>(); }
        }
    }
}