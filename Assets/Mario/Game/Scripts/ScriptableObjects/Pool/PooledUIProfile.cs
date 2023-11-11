using UnityEngine;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "PooledUIProfile", menuName = "ScriptableObjects/Game/Pool/PooledUIProfile", order = 1)]
    public class PooledUIProfile : PooledBaseProfile
    {
        [Header("UI")]
        [SerializeField] private RenderMode _renderMode;
        [SerializeField] private string _canvasSortingLayer;

        public RenderMode RenderMode => _renderMode;
        public string CanvasSortingLayer => _canvasSortingLayer;
    }
}