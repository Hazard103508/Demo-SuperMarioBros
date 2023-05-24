using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "FlagPoleProfile", menuName = "ScriptableObjects/Game/Interactable/FlagPoleProfile", order = 0)]
    public class FlagPoleProfile : ScriptableObject
    {
        public int[] Points;
    }
}