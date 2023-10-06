using UnityEngine;

/* Script used by the in-game pointer which controls the movement of the atom */

public class PointerController : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(PlayerService.Instance.ArePlayersPresent())
            transform.position = EventService.Instance.InvokeOnMouseClickedPosition();
    }
}
