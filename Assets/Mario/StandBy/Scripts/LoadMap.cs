using Mario.Application.Services;
using UnityEngine;

namespace Mario.StandBy
{
    public class LoadMap : MonoBehaviour
    {
        private void Start()
        {
            AllServices.SceneService.LoadMapScene(2.5f);
        }
    }
}