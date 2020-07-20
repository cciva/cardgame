using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Lib
{
    public interface IPlayer
    {
        string Uid { get; }
        string Name { get;set; }
        byte Order { get; set; }

        IStash Stash { get; }

        // void EnterGame();
        Hand Play();
        void Collect(IEnumerable<Card> cards);
    }
}
