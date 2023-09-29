using UnityEngine;
using UnityEngine.AI;

public class AtomController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Transform _camTransform;
    [SerializeField] private float _speed;
    private Vector3 offset;
    private NavMeshAgent _agent;
    [SerializeField] private LayerMask _playerLayer;
    private Vector3 hitPoint;

    private void Start()
    {
        MouseEvents.Instance.OnMouseClickedPosition += CurrentMouseClickPosition;

        rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        hitPoint = transform.position;
    }

    private void Update()
    {
        _agent.SetDestination(CurrentMouseClickPosition());
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
        MouseEvents.Instance.OnMouseClickedPosition -= CurrentMouseClickPosition;
    }
}
