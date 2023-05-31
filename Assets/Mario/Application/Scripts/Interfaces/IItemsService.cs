namespace Mario.Application.Interfaces
{
    public interface IItemsService : IGameService
    {
        bool CanMove { get; }

        void StopMovement();
        void ResumeMovement();
    }
}