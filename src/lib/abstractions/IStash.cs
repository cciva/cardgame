using System.Collections.Generic;

namespace Game.Lib
{
    public interface IStash
    {
        IEnumerable<Card> Fetch(byte count = 1);        
        void Put(IEnumerable<Card> cards);

        IEnumerable<Card> DrawPile { get; }
        IEnumerable<Card> DiscardPile { get; }

        byte NumOwnedCards { get; }
        bool Empty { get; }
    }
}