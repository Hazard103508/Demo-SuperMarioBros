using UnityEngine;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "PooledUIProfile", menuName = "ScriptableObjects/Game/Pool/PooledUIProfile", order = 1)]
    public class PooledUIProfile : BasePooledObjectProfile
    {
        [Header("Canvas")]
        public RenderMode RenderMode;
        public string CanvasSortingLayer;
    }
}