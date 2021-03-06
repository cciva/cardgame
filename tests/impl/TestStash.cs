using System.Collections.Generic;
using System;
using System.Linq;
using NLog;
using Game.Lib;

namespace Tests
{
    public class TestStash : IStash
    {
        private readonly IDealer _dealer = null;
        private readonly ILogger _logger = null;
        private Queue<Card> _draw = null;
        private Queue<Card> _discarded = null;

        public TestStash(IDealer dealer, ILogger logger = null)
        {
            _dealer = dealer;
            _draw = new Queue<Card>(_dealer.NewDeck().Shuffle().Deal(20));
            // _draw = new Queue<Card>(_dealer.NewDeck().Take(20));
            _discarded = new Queue<Card>();

            if(logger == null)
                _logger = LogManager.GetLogger("stash");
            else
                _logger = logger;
        }

        public IEnumerable<Card> Fetch(byte count = 1)
        {
            if (count == 0)
                throw new InvalidOperationException("you can only draw positive number of cards");

            if (_draw.Count == 0)
            {
                // shuffle discard pile and clear it afterwards
                IEnumerable<Card> shuffled = _discarded.Shuffle();
                
                _discarded.Clear();

                // create new draw pile from shuffled cards
                _draw = new Queue<Card>(shuffled);
            }

            if (count > _draw.Count)
                throw new InvalidOperationException(string.Format("wanted {0} cards but have only {1} in the deck :(", count, _draw.Count));

            yield return _draw.Dequeue();
        }

        public void Put(IEnumerable<Card> cards)
        {
            foreach(Card card in cards)
                _discarded.Enqueue(card);
        }

        public byte NumOwnedCards
        {
            get 
            { 
                return (byte)(_draw.Count + _discarded.Count); 
            }
        }
        
        public bool Empty
        {
            get 
            { 
                return _draw.Count == 0 && 
                       _discarded.Count == 0;
            }
        }

        protected ILogger Logger
        {
            get { return _logger; }
        }

        public IEnumerable<Card> DrawPile
        {
            get { return _draw; }
        }

        public IEnumerable<Card> DiscardPile
        {
            get { return _discarded; }
        }
    }
}