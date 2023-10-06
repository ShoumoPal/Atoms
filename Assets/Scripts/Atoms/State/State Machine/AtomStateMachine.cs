using UnityEngine;

public enum AtomState
{
    IDLE,
    ACTIVATED,
    CHASE
}

public class AtomStateMachine : MonoBehaviour
{
    private AtomBaseState _currentAtomState = null;

    public Material _enemyMat;
    public LayerMask _bodyLayerMask;

    private AtomIdleState _idleState;
    private AtomActivatedState _activatedState;
    private AtomChaseState _chaseState;

    private void Start()
    {
        _idleState = new AtomIdleState(this);
        _activatedState = new AtomActivatedState(this);
        _chaseState = new AtomChaseState(this);

        ChangeAtomState(AtomState.IDLE);
    }

    private void Update()
    {
        _currentAtomState?.Tick();
    }

    public void ChangeAtomState(AtomState state)
    {
        Debug.Log("Changing state to: " + state + "For object: " + gameObject.name);
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
            case AtomState.CHASE:
                return _chaseState;
        }
        return null;
    }
}
