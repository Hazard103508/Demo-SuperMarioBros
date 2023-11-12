using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Components
{
    public class PoolFactoryUI : PoolFactory
    {
        public override Pool CreatePool(PooledBaseProfile profile, Transform parent)
        {
            var pool = base.CreatePool(profile, parent);
            pool.gameObject.AddComponent<Canvas>();
            pool.OnCreate = OnCreate;
            pool.PrefabReference = _addressablesService.GetAssetReference<GameObject>(profile.name);

            pool.Load();
            return pool;
        }

        private void OnCreate(Pool pool, GameObject obj)
        {
            var _profile = (PooledUIProfile)pool.Profile;

            var canvas = pool.GetComponent<Canvas>();
            canvas.renderMode = _profile.RenderMode;
            if (_profile.RenderMode != RenderMode.ScreenSpaceOverlay)
                canvas.worldCamera = Camera.main;

            canvas.sortingLayerName = _profile.CanvasSortingLayer;
        }
    }
}