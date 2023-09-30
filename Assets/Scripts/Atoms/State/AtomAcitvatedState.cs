using UnityEngine;
using UnityEngine.AI;

public class AtomActivatedState : AtomBaseState
{
    public AtomActivatedState(AtomStateMachine stateMachine) : base(stateMachine) { }

    public override void OnStateEnter()
    {
        _atomSM.gameObject.AddComponent<AtomController>();
        _atomSM.gameObject.AddComponent<NavMeshAgent>();

        // Setting Navmesh parameters
        _atomSM.gameObject.GetComponent<NavMeshAgent>().acceleration = 80f;
        _atomSM.gameObject.GetComponent<NavMeshAgent>().angularSpeed = 120f;
        _atomSM.gameObject.GetComponent<NavMeshAgent>().speed = 20f;
        _atomSM.gameObject.GetComponent<NavMeshAgent>().stoppingDistance = 2.5f;

        // Setting AtomController parameters
        _atomSM.gameObject.GetComponent<AtomController>().SetAtomType(AtomType.ENEMY);
        _atomSM.gameObject.GetComponent<AtomController>()._playerLayer = PlayerService.Instance._player.GetComponent<AtomController>()._playerLayer;

        // Changing material
        _atomSM.GetComponent<MeshRenderer>().material = PlayerService.Instance._player.GetComponent<MeshRenderer>().material;
    }
}
