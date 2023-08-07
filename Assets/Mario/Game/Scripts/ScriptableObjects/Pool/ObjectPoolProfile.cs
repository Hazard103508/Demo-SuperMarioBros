using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "ObjectPoolProfile", menuName = "ScriptableObjects/Game/Pool/ObjectPoolProfile", order = 4)]
    public class ObjectPoolProfile : ScriptableObject
    {
        public string Type;
        public AssetReferenceGameObject Reference;
        public bool RequireCanvas;
        public string CanvasSortingLayer;
    }
}