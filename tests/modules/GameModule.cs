using System.Collections.Generic;
using Game.Lib;
using Autofac;
using Autofac.Core;

namespace Tests
{
    public class GameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
            builder.RegisterType<TestStash>()                
                .As<IStash>();

            // player
            builder.RegisterType<TestPlayer>()                
                .As<IPlayer>();

            // game
            // builder.RegisterType<Game>()
            //     .AsSelf();
        }
    }
}