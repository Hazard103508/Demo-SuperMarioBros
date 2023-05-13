using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface ICoinService : IGameService
    {
        UnityEvent OnCoinsChanged { get; set; }
        void Add();
    }
}