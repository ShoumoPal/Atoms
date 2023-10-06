using System;
using UnityEngine;

/* Script containing some events in the game */

public class EventService : GenericMonoSingleton<EventService>
{
    public event Func<Vector3> OnMouseClickedPosition;
    public event Func<bool> HasSatisfiedAtomCondition;
    public event Func<bool> IsGameOver;

    public Vector3 InvokeOnMouseClickedPosition()
    {
        return (Vector3)OnMouseClickedPosition?.Invoke();
    }

    public bool InvokeHasSatisfiedAtomCondition()
    {
        return (bool)HasSatisfiedAtomCondition?.Invoke();
    }

    public bool InvokeIsGameOver()
    {
        return (bool)IsGameOver?.Invoke();
    }
}
