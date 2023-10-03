using System;
using UnityEngine;

public class EventService : GenericMonoSingleton<EventService>
{
    public event Func<Vector3> OnMouseClickedPosition;
    public event Func<bool> HasSatisfiedAtomCondition;

    public Vector3 InvokeOnMouseClickedPosition()
    {
        return (Vector3)OnMouseClickedPosition?.Invoke();
    }

    public bool InvokeHasSatisfiedAtomCondition()
    {
        return (bool)HasSatisfiedAtomCondition?.Invoke();
    }
}
