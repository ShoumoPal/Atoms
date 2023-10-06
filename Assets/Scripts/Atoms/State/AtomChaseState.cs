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
        if(_atomSM.gameObject.GetComponent<AtomController>().GetAtomType() == AtomType.ENEMY)
        {
            _agent.acceleration = 100f;
            _agent.angularSpeed = 80f;
            _agent.speed = 50f;
            _agent.stoppingDistance = 0.5f;
            _agent.radius = 0.5f;
        }
        else if(_atomSM.gameObject.GetComponent<AtomController>().GetAtomType() == AtomType.BOSS)
        {
            _agent.acceleration = 50f;
            _agent.angularSpeed = 80f;
            _agent.speed = 20f;
            _agent.stoppingDistance = 0.5f;
            _agent.radius = 0.25f;
        }
    }

    public override void Tick()
    {
        if(PlayerService.Instance.ArePlayersPresent())
        {
            Debug.Log("Chasing for gameobject: " + _atomSM.gameObject.name + " and chasing: " + PlayerService.Instance._players[0].gameObject.name + " and agent: " + _agent);
            _agent.SetDestination(PlayerService.Instance._players[0].transform.position);
        }
    }
}

