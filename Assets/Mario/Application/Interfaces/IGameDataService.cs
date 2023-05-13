using Mario.Game.ScriptableObjects;

namespace Mario.Application.Interfaces
{
    public interface IGameDataService : IGameService
    {
        MapProfile MapProfile { get; set; }
        int Coins { get; set; }
        int Score { get; set; }
        int Time { get; set; }
    }
}