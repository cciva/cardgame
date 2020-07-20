using System;
using System.Collections.Generic;

namespace Game.Lib
{
    public interface IDealer
    {
        IDealer NewDeck(Func<Card, bool> filter = null);
        IDealer Shuffle(IEnumerable<Card> input = null);
        IEnumerable<Card> Deal(int num);

        IEnumerable<Card> Deck
        {
            get;
        }
    }
}