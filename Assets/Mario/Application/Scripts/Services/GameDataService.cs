using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects;

namespace Mario.Application.Services
{
    public class GameDataService : IGameDataService
    {
        public PlayerProfile PlayerProfile { get; set; }
        public MapProfile MapProfile { get; set; }
        public int Coins { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
    }
}