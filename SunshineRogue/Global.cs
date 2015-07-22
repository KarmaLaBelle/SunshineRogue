using RogueSharp;
using RogueSharp.Random;

namespace SunshineRogue
{
    public enum GameStates
    {
        None = 0,
        PlayerTurn = 1,
        EnemyTurn = 2,
        Debugging = 3
    }

    class Global
    {
        public static readonly IRandom Random = new DotNetRandom();
        public static GameStates GameState { get; set; }
    }
}
