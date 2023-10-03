using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum AtomType
{
    PLAYER,
    FRIENDLY,
    ENEMY
}

public class AtomController : MonoBehaviour, IDamagable
{
    public Material _enemyMat;
    [SerializeField] private int _health;
    [SerializeField] private TextMeshPro _healthText;
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

        ShowText();
    }

    private void FixedUpdate()
    {
        //if (Input.GetMouseButton(0))
        //{
        if (_agent == null && gameObject.GetComponent<NavMeshAgent>())
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
        }

        if(_atomType != AtomType.ENEMY)
        {
            if (_agent.remainingDistance < 2f)
            {
                _agent.isStopped = true;
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            if ((_atomType == AtomType.FRIENDLY || _atomType == AtomType.PLAYER) && _agent != null && Input.GetMouseButton(0))
            {
                _agent.isStopped = false;
                _agent.SetDestination(CurrentMouseClickPosition());
            }
        }      
        //}
    }

    private void LateUpdate()
    {
        _healthText.transform.LookAt(_healthText.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    public void SetAtomType(AtomType atomType)
    {
        _atomType = atomType;
        _health = 10;

        if (atomType == AtomType.FRIENDLY)
        {
            if (_agent == null)
                _agent = gameObject.AddComponent<NavMeshAgent>();
            else
                _agent = gameObject.GetComponent<NavMeshAgent>();

            if(_atomType != AtomType.PLAYER)
                PlayerService.Instance.AddAtomToList(this);

            Debug.Log(_agent);
            // Setting Navmesh parameters
            _agent.acceleration = 100f;
            _agent.angularSpeed = 120f;
            _agent.speed = 30f;
            _agent.stoppingDistance = 0.5f;
            _agent.radius = 0.5f;

            // Changing material
            gameObject.GetComponent<MeshRenderer>().material = PlayerService.Instance._players[0].GetComponent<MeshRenderer>().material;
        }
        if(atomType == AtomType.ENEMY)
        {
            PlayerService.Instance.RemoveAtomFromList(this);

            // Changing state
            gameObject.GetComponent<AtomStateMachine>().ChangeAtomState(AtomState.CHASE);

            // Changing material
            gameObject.GetComponent<MeshRenderer>().material = _enemyMat;
            gameObject.GetComponent<AtomStateMachine>().ChangeAtomState(AtomState.CHASE);

            PlayerService.Instance.CameraFollowPlayer();
        }
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
                this.TakeDamage();
                collision.gameObject.GetComponent<AtomController>().TakeDamage();
            }
        }
    }

    public void SetHealthText(TextMeshPro text)
    {
        _healthText = text;
    }

    public void SetHealth(int health)
    {
        _health = health;
    }

    public int GetHealthValue()
    {
        return _health;
    }

    public void SetHealthTextValue()
    {
        _healthText.text = _health.ToString();
    }

    public void ShowText()
    {
        _healthText.text = _health.ToString();
        _healthText.color = new Color(_healthText.color.r, _healthText.color.g, _healthText.color.b, 1f);
        _healthText.DOFade(0f, 2f);
    }

    private void OnDisable()
    {
        EventService.Instance.OnMouseClickedPosition -= CurrentMouseClickPosition;
    }

    public void TakeDamage()
    {
        _health--;
        if(_health <= 0)
        {
            if(_atomType == AtomType.FRIENDLY)
                SetAtomType(AtomType.ENEMY);
            if (_atomType == AtomType.ENEMY)
                SetAtomType(AtomType.FRIENDLY);
        }
        else
        {
            ShowText();
        }        
    }
}
