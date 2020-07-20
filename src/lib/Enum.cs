using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Game.Lib
{
    public enum Stage
    {
        Unknown,
        NextPlayer,
        RoundEnd,
        GameEnd,
    }

    public enum Rank : byte
    {
        [Description("A")]
        Ace = 1,
        [Description("2")]
        Two,
        [Description("3")]
        Three,
        [Description("4")]
        Four,
        [Description("5")]
        Five,
        [Description("6")]
        Six,
        [Description("7")]
        Seven,
        [Description("8")]
        Eight,
        [Description("9")]
        Nine,
        [Description("10")]
        Ten,
        [Description("J")]
        Jack,
        [Description("Q")]
        Queen,
        [Description("K")]
        King,
        [Description("")]
        Any
    }

    public enum Suit
    {
        [Description("\u2663")]
        Clubs,
        [Description("\u2666")]
        Diamonds,
        [Description("\u2660")]
        Spades,
        [Description("\u2665")]
        Hearts,
        [Description("")]
        Any
    }

    public enum Outcome
    {
        Unknown,
        GameWin,
        RoundWin,
        RoundLoss,
        GameLoss,
        Draw
    }
}
