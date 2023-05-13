namespace Mario.Application.Interfaces
{
    public interface IGameDataService : IGameService
    {
        int Coins { get; set; }
    }
}