using System;

namespace Game.Lib
{
    public class Card : IEquatable<Card>
    {
        public Card(Suit s, Rank r)
        {
            Suit = s;
            Rank = r;
        }

        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        // should be in 'rank:suit' format
        public static Card FromString(string str)
        {
            Rank rank = Rank.Any;
            Suit suit = Suit.Any;

            if(!str.Contains(":"))
                return null;
            
            // extract rank and suit string values
            // by splitting string using delimiter ':'
            string[] elems = str.Split(':');
            string rstr = elems[0].Trim();
            string sstr = elems[1].Trim();

            if(string.IsNullOrEmpty(rstr) || string.IsNullOrEmpty(sstr))
                return null;

            if(!Enum.TryParse(rstr, true, out rank))
                return null;

            if(!Enum.TryParse(sstr, true, out suit))
                return null;

            return new Card(suit, rank);
        }

        public bool Equals(Card other)
        {
            if(this.Rank == Rank.Any || other.Rank == Rank.Any)
                return this.Suit == other.Suit;

            if(this.Suit == Suit.Any || other.Suit == Suit.Any)
                return this.Rank == other.Rank;

            return (this.Suit == other.Suit && this.Rank == other.Rank);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Rank.PrettyPrint(), Suit.PrettyPrint());
        }
    }
}