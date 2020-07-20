namespace Game.Lib
{
    public class GameConfig
    {
        public int DrawsInRow { get; set; }
        public int NumberOfDealtCards { get;set; }
        public int NumberOfPlayers { get; set; }
        public string[] ExcludeCards { get; set; }
        public bool RandomPlayerOrder { get; set; }
    }
}
