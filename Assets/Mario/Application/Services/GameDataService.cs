using Mario.Application.Interfaces;

namespace Mario.Application.Services
{
    public class GameDataService : IGameDataService
    {
        public int Coins { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }

        public void Update()
        {
        }
    }
}