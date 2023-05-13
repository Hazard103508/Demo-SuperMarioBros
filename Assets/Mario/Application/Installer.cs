using Mario.Application.Interfaces;
using Mario.Application.Services;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mario.Application
{
    [InitializeOnLoad]
    public class Installer : MonoBehaviour
    {
        private List<IGameService> lstServices;

        private void Awake()
        {
            ServiceLocator.Initiailze();
            ServiceLocator.Current.Register<IGameDataService>(new GameDataService());
            ServiceLocator.Current.Register<ICoinService>(new CoinService());
            ServiceLocator.Current.Register<IScoreService>(new ScoreService());
            ServiceLocator.Current.Register<ITimeService>(new TimeService());

            lstServices = new List<IGameService>()
            {
                ServiceLocator.Current.Get<IGameDataService>(),
                ServiceLocator.Current.Get<ICoinService>(),
                ServiceLocator.Current.Get<IScoreService>(),
                ServiceLocator.Current.Get<ITimeService>()
            };
        }
        private void Update()
        {
            foreach (IGameService service in lstServices)
                service.Update();
        }
    }
}