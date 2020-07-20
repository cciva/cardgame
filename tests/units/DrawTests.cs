using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics.Tracing;
using Xunit;
using Game.Lib;
using Xunit.Abstractions;

namespace Tests
{
    
    public class DrawTests : IoCBasedTest<GameModule>
    {
        private IPlayer player;
        private GameConfig conf;
        private IGameCoordinator coordinator;

        public DrawTests()
        {
            coordinator = Resolve<IGameCoordinator>();
            player = Resolve<IPlayer>("name", "player1");
            conf = Resolve<GameConfig>();
        }
        
        [Fact]
        public void EmptyDrawPileShouldBecomeReshuffleDiscardPile()
        {
            //GameContext context = new GameContext(conf);
            int initialCount = 20;
            int drawCount = player.Stash.DrawPile.Count();
            int discardCount = player.Stash.DiscardPile.Count();

            Assert.True(player.Stash.NumOwnedCards == initialCount);
            Assert.False(player.Stash.Empty);

            while(drawCount > 0)
            {
                player.Play();
                coordinator.Eval();
                drawCount = player.Stash.DrawPile.Count();
                discardCount = player.Stash.DiscardPile.Count();
            }
            
            Assert.True(drawCount == 0);
            Assert.True(discardCount > drawCount);

            // after player has played piles should be as follows :
            // - draw pile = initial count (20 in this case)
            // - discard pile = 0
            player.Play();

            drawCount = player.Stash.DrawPile.Count();
            discardCount = player.Stash.DiscardPile.Count();
            
            Assert.True(drawCount > 0); // minus one played card
            Assert.True(discardCount == 0);
        }

        ~DrawTests()
        {
            Teardown();
        }
    }
    /**/
}