using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Maps
{
    public class MapSection : MonoBehaviour
    {
        #region Objects
        public Size2Int Size;
        #endregion

        #region Unity Methods
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 1f);
            Gizmos.DrawWireCube(transform.position + new Vector3(Size.Width / 2, Size.Height / 2), new Vector3(Size.Width, Size.Height, 0));
        }
        #endregion
    }
}