using Mario.Application.Services;

namespace Mario.Application.Interfaces
{
    public interface IInputService : IGameService
    {
        event InputActionDelegate StartPressed;
        event InputActionDelegate PausePressed;
        event InputActionDelegate<float> MovePressed;
        event InputActionDelegate<bool> JumpPressed;
        event InputActionDelegate<bool> SprintPressed;
        event InputActionDelegate<bool> DuckPressed;

        void UseUIMap();
        void UseGameplayMap();
    }
}