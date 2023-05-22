using Mario.Application.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.StandBy
{
    public class LoadMap : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LosMap());
        }
        private IEnumerator LosMap()
        {
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
        }
    }
}