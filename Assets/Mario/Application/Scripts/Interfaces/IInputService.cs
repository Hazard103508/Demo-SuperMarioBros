using Mario.Application.Services;

namespace Mario.Application.Interfaces
{
    public interface IInputService : IGameService
    {
        event InputActionDelegate StartPressed;
        event InputActionDelegate PausePressed;

        void UseUIMap();
        void UseGameplayMap();
    }
}