namespace Mario.Application.Interfaces
{
    public interface ICharacterService : IGameService
    {
        bool CanMove();
        void StopMovement();
        void ResumeMovement();
    }
}