using UnityEngine;

public class PointerController : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(PlayerService.Instance.ArePlayersPresent())
            transform.position = EventService.Instance.InvokeOnMouseClickedPosition();
    }
}
