using Mario.Application.Interfaces;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class LifeService : ILifeService
    {
        public int Lives { get; private set; }

        public LifeService()
        {
            Lives = 3;
            OnLivesChanged = new UnityEvent();
        }
        
        public UnityEvent OnLivesChanged { get; set; }

        public void Add()
        {
            this.Lives++;
            OnLivesChanged.Invoke();
        }
    }
}