using Mario.Game.ScriptableObjects.Map;

namespace Mario.Application.Interfaces
{
    public interface IGameDataService : IGameService
    {
        MapProfile MapProfile { get; set; }
        PlayerProfile PlayerProfile { get; set; }
    }
}