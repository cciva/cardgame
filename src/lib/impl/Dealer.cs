using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Linq;
using NLog;

namespace Game.Lib
{
    public class Dealer : IDealer
    {
        private Queue<Card> _deck = null;
        //private IList<Card> _deck = null;
        private readonly ILogger _logger = null;
        // default filter from config
        private Func<Card, bool> _filter = null;

        public Dealer(GameConfig conf, ILogger logger = null)
        {
            if(logger == null)
                _logger = LogManager.GetLogger("coordinator");
            else
                _logger = logger;
                
            _deck = new Queue<Card>(conf.NumberOfDealtCards);
            _filter = MakeFilter(conf);
        }

        public IDealer NewDeck(Func<Card, bool> filter = null)
        {
            if(filter == null)
                filter = _filter;

            PurgeDeck();

            string[] suits = Enum.GetNames(typeof(Suit))
                .Where(n => !n.Equals("any", StringComparison.OrdinalIgnoreCase))
                .ToArray();
            string[] ranks = Enum.GetNames(typeof(Rank))
                .Where(n => !n.Equals("any", StringComparison.OrdinalIgnoreCase))
                .ToArray();
            Suit s = Suit.Any;
            Rank r = Rank.Any;
            Card card;
            
            for (int i = 0; i < suits.Length; i++)
            {
                for(int j = 0; j < ranks.Length; j++)
                {
                    s = (Suit)Enum.Parse(typeof(Suit), suits[i]);
                    r = (Rank)Enum.Parse(typeof(Rank), ranks[j]);
                    card = new Card(s, r);

                    if(filter != null && filter(card))
                        continue;
                    
                    _deck.Enqueue(card);
                }
            }

            return this;
        }

        public IDealer Shuffle(IEnumerable<Card> input = null)
        {
            if(input == null || input.Count() == 0)
            {
                PurgeDeck();
                NewDeck();
                _deck = new Queue<Card>(_deck.Shuffle());
            }
            else
                _deck = new Queue<Card>(input.Shuffle());

            return this;
        }

        public IEnumerable<Card> Deal(int num)
        {
            while(num-- > 0)
                yield return _deck.Dequeue();
        }

        public IEnumerable<Card> Deck
        {
            get { return _deck; }
        }

        protected ILogger Logger
        {
            get { return _logger; }
        }

        private Func<Card, bool> MakeFilter(GameConfig conf)
        {
            ICollection<Card> excluded = new Collection<Card>();

            if(conf.ExcludeCards != null && conf.ExcludeCards.Count() > 0)
            {
                foreach(string str in conf.ExcludeCards)
                {
                    Card card = Card.FromString(str);
                    if(card == null)
                        return null;

                    excluded.Add(card);
                }
            }

            return (c) => excluded.Contains(c);
        }

        private void PurgeDeck()
        {
            if(_deck != null && _deck.Count > 0)
            {
                _deck.Clear();
            }
        }
    }
}