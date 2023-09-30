using UnityEngine;

public class AtomBaseState
{
    protected AtomStateMachine _atomSM;

    public AtomBaseState(AtomStateMachine stateMachine)
    {
        _atomSM = stateMachine;
    }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public virtual void Tick() { }
}
