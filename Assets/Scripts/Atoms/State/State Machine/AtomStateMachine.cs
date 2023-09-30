using System;
using UnityEngine;

public enum AtomState
{
    IDLE,
    ACTIVATED
}

public class AtomStateMachine : MonoBehaviour
{
    private AtomBaseState _currentAtomState = null;

    private AtomIdleState _idleState;
    private AtomActivatedState _activatedState;

    private void Start()
    {
        _idleState = new AtomIdleState(this);
        _activatedState = new AtomActivatedState(this);
        ChangeAtomState(AtomState.IDLE);
    }

    private void Update()
    {
        _currentAtomState?.Tick();
    }

    public void ChangeAtomState(AtomState state)
    {
        Debug.Log("Changing state to: " + state);
        AtomBaseState newState = GetStateFromEnum(state);

        if (_currentAtomState == newState)
            return;

        _currentAtomState?.OnStateExit();
        _currentAtomState = newState;
        _currentAtomState.OnStateEnter();
    }

    private AtomBaseState GetStateFromEnum(AtomState state)
    {
        switch (state)
        {
            case AtomState.IDLE:
                return _idleState;
            case AtomState.ACTIVATED:
                return _activatedState;
        }
        return null;
    }
}
