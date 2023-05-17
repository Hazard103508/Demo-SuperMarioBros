namespace Mario.Application.Interfaces
{
    public interface ICharacterService : IGameService
    {
        bool CanMove { get; }

        void StopMovement();
        void ResumeMovement();
    }
}