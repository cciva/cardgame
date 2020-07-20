using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Linq;
using NLog;

namespace Game.Lib
{
    public class GameCoordinator : IGameCoordinator
    {
        private readonly ILogger _logger = null;
        private readonly GameConfig _conf = null;
        private readonly Stack<Hand> _history = null;
        private Stage _stage = Stage.Unknown;
        private int _cycle = 0;
        
        public GameCoordinator(GameConfig conf, ILogger logger = null)
        {
            _conf = conf;
            _history = new Stack<Hand>();

            if(logger == null)
                _logger = LogManager.GetLogger("coordinator");
            else
                _logger = logger;
        }

        public Hand PlayTurn(IPlayer player, out Stage stage)
        {
            stage = Stage.Unknown;
            Hand hand = player.Play();
            stage = this.RecordHistory(hand);
            
            return hand;
        }
        
        public Result Eval()
        {
            Hand winner = null;

            if(HandleWin(out winner))
                return new Result(winner);

            if(HandleDraw())
                return new Result(Outcome.Draw);

            return new Result(Outcome.Unknown);
        }

        public IEnumerable<Hand> LastRound
        {
            get
            {
                if(History.Count() == 0)
                    return Enumerable.Empty<Hand>();

                // take last N hands where N is number of players
                // that will give us a round 
                return History.Reverse()
                    .Skip(Math.Max(0, History.Count() - Conf.NumberOfPlayers));
            }
        }

        public IEnumerable<Hand> History
        {
            get { return _history.Reverse(); }
        }

        public Stage Stage 
        { 
            get { return _stage; }
            private set { _stage = value; }
        }

        protected ILogger Logger
        {
            get { return _logger; }
        }

        protected GameConfig Conf
        {
            get { return _conf; }
        }

        private bool HandleDraw()
        {
            IEnumerable<Hand> round = LastRound;
            bool hasDups = round.Select(r => r.Card.Rank).Distinct().Count() == 1;
            int num = round.Count();
            if(hasDups)
            {
                List<Hand> temp = new List<Hand>();

                while(num-- > 0)
                {
                    Hand hand = _history.Pop();
                    temp.Add(hand);
                }
                
                foreach(var h in temp)
                    _history.Push(new Hand(h.Player, h.Card, Outcome.Draw));

                return true;
            }

            return false;
        }

        private bool HandleWin(out Hand winner)
        {
            winner = null;
            IEnumerable<Hand> round = LastRound;
            if(IsWin(round))
            {
                winner = round.Aggregate((max, x) => max.Item2.Rank > x.Item2.Rank ? max : x);
                winner.Player.Collect(round.Select(r => r.Card));

                // check for past draw outcomes
                Hand draw = History.LastOrDefault(h => h.Outcome == Outcome.Draw);
                if(draw != null)
                {
                    //win.Player.Collect(draw.)
                }
                return true;
            }

            return false;
        }

        private Stage RecordHistory(Hand hand)
        {
            // if we know outcome here, game is 
            // already lost 
            if(hand.Outcome == Outcome.GameLoss)
                return Stage.GameEnd;
                
            _cycle++;
            _history.Push(hand);

            // mark end of round, switch game state
            if(_cycle % Conf.NumberOfPlayers == 0)
            {
                _cycle = 0;
                _stage = Stage.RoundEnd;
            }
            else
                _stage = Stage.NextPlayer;

            return _stage;
        }
    
        private bool IsWin(IEnumerable<Hand> round)
        {
            return Conf.NumberOfPlayers == 1 || round.Select(r => r.Card.Rank).Distinct().Skip(1).Any();
        }
    }
}