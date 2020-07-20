namespace Game.Lib
{
    public class Result
    {
        Outcome _outcome = Outcome.Unknown;
        Hand _winner = null;

        public Result(Outcome outcome)
        {
            _outcome = outcome;
            _winner = null;
        }

        public Result(Hand winner)
            : this(Outcome.RoundWin)
        {
            _winner = winner;
        }

        public Outcome Outcome
        {
            get { return _outcome; }
        }
        
        public Hand Winner
        {
            get { return _winner; }
        }
    }
}