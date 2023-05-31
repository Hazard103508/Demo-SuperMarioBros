using Mario.Application.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Mario.Application.Services
{
    public class SceneService : MonoBehaviour, ISceneService
    {
        private Dictionary<AssetReference, AsyncOperationHandle> _references;

        public void LoadService()
        {
            _references = new Dictionary<AssetReference, AsyncOperationHandle>();
        }
        public void AddAsset(AssetReference assetReference)
        {
            if (_references.ContainsKey(assetReference))
                return;

            _references.Add(assetReference, default);

            var asyncOperationHandle = assetReference.LoadAssetAsync<GameObject>();
            asyncOperationHandle.Completed += handle => _references[assetReference] = handle;
        }
        public T GetAssetReference<T>(AssetReference assetReference)
        {
            if (_references.ContainsKey(assetReference))
                return (T)_references[assetReference].Result;

            return default;
        }
        public void ReleaseAllAssets()
        {
            foreach (var item in _references)
                item.Key.ReleaseAsset();

            _references.Clear();
        }
        public void LoadMapScene(float minDelay) => StartCoroutine(LoadMapSceneCO(minDelay));

        private IEnumerator LoadMapSceneCO(float minDelay)
        {
            yield return null;

            float timer = 0;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone || timer < minDelay)
            {
                if (asyncOperation.progress >= 0.9f && timer >= minDelay)
                    asyncOperation.allowSceneActivation = true;

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}