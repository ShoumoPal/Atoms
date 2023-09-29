using System;
using UnityEngine;

public class MouseEvents : GenericMonoSingleton<MouseEvents>
{
    public event Func<Vector3> OnMouseClickedPosition;

    public Vector3 InvokeOnMouseClickedPosition()
    {
        return (Vector3)OnMouseClickedPosition?.Invoke();
    }
}
