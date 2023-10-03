using System;
using UnityEngine;
using UnityEngine.AI;

public enum AtomType
{
    PLAYER,
    FRIENDLY,
    ENEMY
}

public class AtomController : MonoBehaviour
{
    public Material _enemyMat;
    [SerializeField] private Transform _camTransform;
    [SerializeField] private float _speed;
    [SerializeField] private NavMeshAgent _agent;
    public LayerMask _playerLayer;
    private Vector3 hitPoint;
    [SerializeField] private AtomType _atomType;

    private void Start()
    {
        EventService.Instance.OnMouseClickedPosition += CurrentMouseClickPosition;
        hitPoint = transform.position;
        _agent = null;
    }

    private void FixedUpdate()
    {
        //if (Input.GetMouseButton(0))
        //{
        if(_agent == null && gameObject.GetComponent<NavMeshAgent>())
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
        }

        if ((_atomType == AtomType.FRIENDLY || _atomType == AtomType.PLAYER) && _agent != null && Input.GetMouseButton(0))
        {
            _agent.SetDestination(CurrentMouseClickPosition());
            if(_agent.remainingDistance < 3f)
            {
                _agent.isStopped = true;
            }
            else
            {
                _agent.isStopped = false;
            }
        }
                
        //}
    }

    public void SetAtomType(AtomType atomType)
    {
        if (atomType == AtomType.FRIENDLY)
        {
            _agent = gameObject.AddComponent<NavMeshAgent>();
            Debug.Log("Added component: " + _agent + "\nIn obj: " + gameObject.name);
            if(_atomType != AtomType.PLAYER)
                PlayerService.Instance.AddAtomToList(gameObject);

            // Setting Navmesh parameters
            _agent.acceleration = 100f;
            _agent.angularSpeed = 120f;
            _agent.speed = 30f;
            _agent.stoppingDistance = 1.5f;
            _agent.radius = 1f;

            // Changing material
            gameObject.GetComponent<MeshRenderer>().material = PlayerService.Instance._players[0].GetComponent<MeshRenderer>().material;
        }
        if(atomType == AtomType.ENEMY)
        {
            PlayerService.Instance.RemoveAtomFromList(gameObject);

            // Changing material
            gameObject.GetComponent<MeshRenderer>().material = _enemyMat;
            gameObject.GetComponent<AtomStateMachine>().ChangeAtomState(AtomState.CHASE);
        }
        _atomType = atomType;
    }

    public AtomType GetAtomType()
    {
        return _atomType;
    }

    private Vector3 CurrentMouseClickPosition()
    {
        RaycastHit hit;

        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        
        if(Physics.Raycast(castPoint, out hit, Mathf.Infinity, ~_playerLayer) && Input.GetMouseButton(0))
        {
            hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
        return hitPoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_atomType == AtomType.ENEMY && collision.gameObject.GetComponent<AtomController>())
        {
            if(collision.gameObject.GetComponent<AtomController>()._atomType != AtomType.ENEMY)
            {
                Debug.Log("Collided with : " + collision.gameObject.name);
                collision.gameObject.GetComponent<AtomController>().SetAtomType(AtomType.ENEMY);
            }
        }
    }

    private void OnDisable()
    {
        EventService.Instance.OnMouseClickedPosition -= CurrentMouseClickPosition;
    }
}
