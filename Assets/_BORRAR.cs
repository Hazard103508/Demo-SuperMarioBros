using Mario.Game.Player;
using UnityEngine;

public class _BORRAR : MonoBehaviour
{
    public PlayerController playerController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            playerController.Buff();

        if (Input.GetKeyDown(KeyCode.Keypad0))
            playerController.Nerf();

        if (Input.GetKeyDown(KeyCode.Keypad1))
            playerController.Kill();

        if (Input.GetKeyDown(KeyCode.F))
            playerController.TouchFlag();
    }
}
