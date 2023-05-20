using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class LifeService : ILifeService
    {
        public int Lives { get; private set; }

        public LifeService()
        {
            Lives = 3;
            OnLivesAdded = new UnityEvent();
            OnLivesRemoved = new UnityEvent();
        }

        public UnityEvent OnLivesAdded { get; set; }
        public UnityEvent OnLivesRemoved { get; set; }

        public void Add()
        {
            this.Lives++;
            OnLivesAdded.Invoke();
        }
        public void Remove()
        {
            this.Lives--;
            OnLivesRemoved.Invoke();
        }
    }
}