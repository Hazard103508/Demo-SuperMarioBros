using Mario.Application.Services;
using System.Collections;
using UnityEngine;

namespace Mario.TimeUp
{
    public class TimeUp : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LoadStandBy());
        }
        private IEnumerator LoadStandBy()
        {
            yield return new WaitForSeconds(2.4f);
            Services.SceneService.LoadStandByScene();
        }
    }
}