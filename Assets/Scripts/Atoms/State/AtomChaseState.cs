using UnityEngine;
using UnityEngine.AI;

public class AtomChaseState : AtomBaseState
{
    private NavMeshAgent _agent;

    public AtomChaseState(AtomStateMachine stateMachine) : base(stateMachine) { }

    public override void OnStateEnter()
    {
        _agent = _atomSM.gameObject.GetComponent<NavMeshAgent>();
    }

    public override void Tick()
    {
        if(PlayerService.Instance.ArePlayersPresent())
        {
            _agent.SetDestination(PlayerService.Instance._players[0].transform.position);
        }
    }
}

