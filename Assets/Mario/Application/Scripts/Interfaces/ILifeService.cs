using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface ILifeService : IGameService
    {
        int Lives { get; }


        UnityEvent OnLivesChanged { get; set; }
        void Add();
    }
}