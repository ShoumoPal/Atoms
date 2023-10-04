using UnityEngine;
using UnityEngine.AI;

public class AtomChaseState : AtomBaseState
{
    private NavMeshAgent _agent;

    public AtomChaseState(AtomStateMachine stateMachine) : base(stateMachine) { }

    public override void OnStateEnter()
    {
        _agent = _atomSM.gameObject.GetComponent<NavMeshAgent>();

        // Setting Navmesh parameters
        _agent.acceleration = 100f;
        _agent.angularSpeed = 80f;
        _agent.speed = 50f;
        _agent.stoppingDistance = 0.5f;
        _agent.radius = 0.5f;
    }

    public override void Tick()
    {
        if(PlayerService.Instance.ArePlayersPresent())
        {
            _agent.SetDestination(PlayerService.Instance._players[0].transform.position);
        }
    }
}

