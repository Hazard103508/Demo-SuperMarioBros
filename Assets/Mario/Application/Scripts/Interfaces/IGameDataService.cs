using Mario.Game.ScriptableObjects.Map;

namespace Mario.Application.Interfaces
{
    public interface IGameDataService : IGameService
    {
        PlayerProfile PlayerProfile { get; set; }
        MapProfile CurrentMapProfile { get; set; }
        MapProfile NextMapProfile { get; set; }
    }
}