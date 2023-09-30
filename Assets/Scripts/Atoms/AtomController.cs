using UnityEngine;
using UnityEngine.AI;

public enum AtomType
{
    PLAYER,
    ENEMY
}

public class AtomController : MonoBehaviour
{
    [SerializeField] private Transform _camTransform;
    [SerializeField] private float _speed;
    private NavMeshAgent _agent;
    public LayerMask _playerLayer;
    private Vector3 hitPoint;
    [SerializeField] private AtomType _atomType;

    private void Start()
    {
        EventService.Instance.OnMouseClickedPosition += CurrentMouseClickPosition;

        _agent = GetComponent<NavMeshAgent>();
        hitPoint = transform.position;
    }

    private void Update()
    {
        if(_atomType == AtomType.PLAYER)
            _agent.SetDestination(CurrentMouseClickPosition());
        else if(_atomType == AtomType.ENEMY)
            _agent.SetDestination(PlayerService.Instance._player.transform.position);
    }

    public void SetAtomType(AtomType atomType)
    {
        _atomType = atomType;
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

    private void OnDisable()
    {
        EventService.Instance.OnMouseClickedPosition -= CurrentMouseClickPosition;
    }
}
