namespace Mario.Application.Interfaces
{
    public interface ICharacterService : IGameService
    {
        bool AllowMove { get; set; }
    }
}