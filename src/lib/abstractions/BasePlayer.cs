using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Game.Lib
{
    public abstract class BasePlayer : IPlayer
    {
        IStash _stash = null;

        public BasePlayer(string name, IStash stash)
        {
            Name = name;
            Uid = Guid.NewGuid().ToString();
            Stash = stash;
        }
        
        // public void EnterGame()
        // {
        //     throw new System.NotImplementedException();
        // }

        public Hand Play()
        {
            Hand hand = null;
            Card card = null;

            if(Stash.Empty)
                hand = LoseGame(null);
            else
            {
                card = Stash.Fetch().FirstOrDefault();
                if(card == null)
                    hand = LoseGame(null);
                else
                    hand = new Hand(this, card, Outcome.Unknown);
            }
            
            return hand;
        }
        
        public void Collect(IEnumerable<Card> cards)
        {
            Stash.Put(cards);
        }

        public string Uid { get; private set; }
        public string Name { get; set; }
        public byte Order { get; set; }

        public IStash Stash 
        { 
            get { return _stash; }
            private set { _stash = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        private Hand LoseGame(Card card)
        {
            return new Hand(this, null, Outcome.GameLoss);
        }

        private Hand LoseRound(Card card)
        {
            return new Hand(this, null, Outcome.RoundLoss);
        }
    }
}