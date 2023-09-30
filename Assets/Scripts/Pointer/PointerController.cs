using UnityEngine;

public class PointerController : MonoBehaviour
{
    private void Update()
    {
        transform.position = EventService.Instance.InvokeOnMouseClickedPosition();
    }
}
