using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEditor;
using UnityEngine;

namespace Mario.Application
{
    [InitializeOnLoadAttribute]
    public class Installer : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.Initiailze();
            ServiceLocator.Current.Register<IGameDataService>(new GameDataService());
            ServiceLocator.Current.Register<ICoinService>(new CoinService());
        }
    }
}