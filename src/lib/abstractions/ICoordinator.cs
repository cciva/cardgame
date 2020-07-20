using System;
using System.Collections.Generic;

namespace Game.Lib
{
    public interface IGameCoordinator
    {
        Hand PlayTurn(IPlayer player, out Stage stage);
        Result Eval();
    }
}