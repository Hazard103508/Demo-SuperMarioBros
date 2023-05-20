using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface ILifeService : IGameService
    {
        int Lives { get; }


        UnityEvent OnLivesAdded { get; set; }
        UnityEvent OnLivesRemoved { get; set; }

        void Add();
        void Remove();
    }
}