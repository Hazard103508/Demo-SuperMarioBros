namespace Mario.Application.Interfaces
{
    public interface IGameDataService : IGameService
    {
        int Coins { get; set; }
        int Score { get; set; }
        int Time { get; set; }
    }
}