using Mario.Game.ScriptableObjects.Pool;
using System.Threading.Tasks;
using UnityEngine;

namespace Mario.Application.Components
{
    public class PoolFactoryUI : PoolFactory
    {
        private PooledUIProfile _profile;

        public override Pool CreatePool(PooledBaseProfile profile, Transform parent)
        {
            _profile = (PooledUIProfile)profile;

            base.CreatePool(profile, parent);
            Pool.gameObject.AddComponent<Canvas>();
            Pool.OnCreate = OnCreate;

            Pool.PrefabReference = _addressablesService.GetAssetReference<GameObject>(profile.Reference);
            if (Pool.PrefabReference == null)
                Debug.LogError($"Missing asset reference: {profile.name}");

            Pool.Load();
            return Pool;
        }

        private void OnCreate(GameObject obj)
        {
            var canvas = Pool.GetComponent<Canvas>();
            canvas.renderMode = _profile.RenderMode;
            if (_profile.RenderMode != RenderMode.ScreenSpaceOverlay)
                canvas.worldCamera = Camera.main;

            canvas.sortingLayerName = _profile.CanvasSortingLayer;
        }
    }
}