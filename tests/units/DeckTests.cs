using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;
using Game.Lib;

namespace Tests
{
    public class DeckTests : IoCBasedTest<GameModule>
    {
        private IDealer dealer;

        public DeckTests()
        {
            dealer = Resolve<IDealer>();
        }
        
        [Fact]
        public void DeckShouldContain40Cards()
        {
            IEnumerable<Card> deck = dealer.NewDeck().Deck;
            Assert.True(deck != null && deck.Count() == 40);
        }

        [Fact]
        public void DeckShouldBeShuffled()
        {
            // non-shuffled deck
            IEnumerable<Card> nonShuffled = dealer.NewDeck().Deck;
            // shuffled deck
            IEnumerable<Card> shuffled = nonShuffled.Shuffle();

            // check if sequences are equal
            Assert.False(nonShuffled.SequenceEqual(shuffled));
            // check for duplicate values
            Assert.False(shuffled.GroupBy(x => x).Any(x=> x.Count() > 1));
        }

        ~DeckTests()
        {
            dealer = null;
            Teardown();
        }
    }
}

