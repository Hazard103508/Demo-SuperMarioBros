using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Map;

namespace Mario.Application.Services
{
    public class GameDataService : IGameDataService
    {
        public PlayerProfile PlayerProfile { get; set; }
        public MapProfile CurrentMapProfile { get; set; }
        public MapProfile NextMapProfile { get; set; }
    }
}