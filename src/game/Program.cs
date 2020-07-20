using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using Game.Lib;

namespace CrazyCards
{
    class Program
    {
        static void Main(string[] args)
        {
            IPlayer player1 = GameServices.Default.CreatePlayer("p1");
            IPlayer player2 = GameServices.Default.CreatePlayer("p2");
            
            Game game = GameServices.Default.NewGame();
            game.Register(player1);
            game.Register(player2);

            game.Start();
        }
    }
}
