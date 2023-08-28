using Mario.Game.Player;
using UnityEngine;

public class _BORRAR : MonoBehaviour
{
    public PlayerController playerController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            playerController.Buff();
    }
}
