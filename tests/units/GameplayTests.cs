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
    
    public class GameplayTests : IoCBasedTest<GameModule>
    {
        private IPlayer player1;
        private IPlayer player2;
        private GameConfig conf;
        private IGameCoordinator coordinator;

        public GameplayTests()
        {
            coordinator = Resolve<IGameCoordinator>();
            player1 = Resolve<IPlayer>("name", "player1");
            player2 = Resolve<IPlayer>("name", "player2");
            conf = Resolve<GameConfig>();
        }
        
        [Fact]
        public void EmptyDrawPileShouldBecomeReshuffleDiscardPile()
        {
            Stage stage = Stage.Unknown;

            Hand h1 = coordinator.PlayTurn(player1, out stage);
            Hand h2 = coordinator.PlayTurn(player2, out stage);

            if(stage == Stage.RoundEnd)
            {
                Result result = coordinator.Eval();
                if(h1.Card.Rank > h2.Card.Rank)
                {
                    Assert.True(result.Outcome == Outcome.RoundWin);
                    Assert.True(result.Winner != null);
                    Assert.True(result.Winner.Card.Rank == h1.Card.Rank);
                    Assert.True(result.Winner.Player.Equals(player1));
                }
                else if(h1.Card.Rank < h2.Card.Rank)
                {
                    Assert.True(result.Outcome == Outcome.RoundWin);
                    Assert.True(result.Winner != null);
                    Assert.True(result.Winner.Card.Rank == h2.Card.Rank);
                    Assert.True(result.Winner.Player.Equals(player2));
                }
                else
                {
                    Assert.True(result.Outcome == Outcome.Draw);
                    Assert.True(result.Winner == null);
                }
            }
        }

        ~GameplayTests()
        {
            Teardown();
        }
    }
    /**/
}