using UnityEngine;

namespace Mario.Game.ScriptableObjects.Interactable
{
    [CreateAssetMenu(fileName = "FlagPoleProfile", menuName = "ScriptableObjects/Game/Interactable/FlagPoleProfile", order = 0)]
    public class FlagPoleProfile : ScriptableObject
    {
        [SerializeField] private int[] _points;

        public int[] Points => _points;
    }
}