using UnityEngine;

public class PointerController : MonoBehaviour
{
    private void Update()
    {
        transform.position = MouseEvents.Instance.InvokeOnMouseClickedPosition();
    }
}
