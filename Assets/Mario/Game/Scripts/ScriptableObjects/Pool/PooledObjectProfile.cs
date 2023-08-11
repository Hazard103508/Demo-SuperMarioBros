using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "PooledObjectProfile", menuName = "ScriptableObjects/Game/Pool/PooledObjectProfile", order = 0)]
    public class PooledObjectProfile : ScriptableObject
    {
        public AssetReferenceGameObject Reference;
        public bool CollectionCheck = true;
        public int DefaultCapacity = 1;
        public int MaxSize = 1;
        
        [Header("Canvas")]
        public bool IsCanvasPool;
        public string CanvasSortingLayer;
    }
}