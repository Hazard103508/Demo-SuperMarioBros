using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface ICoinService : IGameService
    {
        int Coins { get; }


        UnityEvent OnCoinsChanged { get; set; }
        void Add();
    }
}