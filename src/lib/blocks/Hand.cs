using System;

namespace Game.Lib
{
    public class Hand : Tuple<IPlayer, Card, Outcome>
    {
        public Hand(IPlayer player, 
                    Card card, 
                    Outcome outcome = Outcome.Unknown)
            : base(player, card, outcome)
        {

        }

        public IPlayer Player
        {
            get { return this.Item1; }
        }

        public Card Card
        {
            get { return this.Item2; }
        }

        public Outcome Outcome
        {
            get { return this.Item3; }
        }
    }
}
