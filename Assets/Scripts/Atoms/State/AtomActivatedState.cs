using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class AtomActivatedState : AtomBaseState
{
    private Vector3 hitPoint;
    private NavMeshAgent _agent;

    public AtomActivatedState(AtomStateMachine stateMachine) : base(stateMachine) { }

    public override void OnStateEnter()
    {
        EventService.Instance.OnMouseClickedPosition += CurrentMouseClickPosition;
        hitPoint = _atomSM.transform.position;
        _agent = null;

        // Setting controller
        if (_atomSM.gameObject.GetComponent<AtomController>() == null)
        {
            _atomSM.gameObject.AddComponent<AtomController>();
            _atomSM.gameObject.GetComponent<AtomController>()._enemyMat = _atomSM._enemyMat;
            _atomSM.gameObject.GetComponent<AtomController>().SetMaxHealthValue(PlayerService.Instance._players[0]._maxHealth);
            _atomSM.gameObject.GetComponent<AtomController>().SetHealthText(_atomSM.gameObject.GetComponentInChildren<TextMeshPro>());
        }

        // Setting Navmesh
        if (_agent == null && _atomSM.gameObject.GetComponent<NavMeshAgent>())
        {
            _agent = _atomSM.gameObject.GetComponent<NavMeshAgent>();
        }
        else
        {
            _agent = _atomSM.gameObject.AddComponent<NavMeshAgent>();
        }

        // Setting AtomController parameters
        _atomSM.gameObject.GetComponent<AtomController>().SetAtomType(AtomType.FRIENDLY);

        //Navmesh parameters
        _agent.acceleration = 100f;
        _agent.angularSpeed = 120f;
        _agent.speed = 30f;
        _agent.stoppingDistance = 0.5f;
        _agent.radius = 0.5f;
    }

    public override void Tick()
    {
        if (_agent.remainingDistance < 2f)
        {
            _agent.isStopped = true;
            _atomSM.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (_agent.remainingDistance > 2f)
        {
            _agent.isStopped = false;
        }

        if(PlayerService.Instance.ArePlayersPresent())
            _agent.SetDestination(CurrentMouseClickPosition());
    }

    private Vector3 CurrentMouseClickPosition()
    {
        RaycastHit hit;

        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, ~(_atomSM._bodyLayerMask)) && Input.GetMouseButton(0))
        {
            hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
        return hitPoint;
    }

    public override void OnStateExit()
    {
        EventService.Instance.OnMouseClickedPosition -= CurrentMouseClickPosition;
    }
}
