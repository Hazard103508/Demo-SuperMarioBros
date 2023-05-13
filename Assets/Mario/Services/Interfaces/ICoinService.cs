using UnityEngine.Events;

namespace Mario.Services.Interfaces
{
    public interface ICoinService : IGameService
    {
        UnityEvent OnCoinsChanged { get; set; }
        int Coins { get; set; }
    }
}