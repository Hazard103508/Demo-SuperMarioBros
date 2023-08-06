using Mario.Application.Services;
using UnityEngine;

namespace Mario.StandBy
{
    public class LoadMap : MonoBehaviour
    {
        private void Start()
        {
            Services.SceneService.LoadMapScene(2.5f);
        }
    }
}