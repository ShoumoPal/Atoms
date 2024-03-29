using DG.Tweening;
using TMPro;
using UnityEngine;

/* Public enum for atom type */

public enum AtomType
{
    PLAYER,
    FRIENDLY,
    ENEMY,
    BOSS
}

/* Main controller for all atoms in the game */

public class AtomController : MonoBehaviour, IDamagable
{
    public Material _enemyMat;
    public int _maxHealth;

    private int _health;
    [SerializeField] private TextMeshPro _healthText;
    [SerializeField] private Transform _camTransform;
    [SerializeField] private float _speed;
    
    [SerializeField] private ParticleSystem _sparks;
    public LayerMask _playerLayer;
    
    [SerializeField] private AtomType _atomType;

    private AtomStateMachine _atomSM;

    private void Start()
    {
        _health = _maxHealth;
        _atomSM = GetComponent<AtomStateMachine>();
        ShowText();
    }

    private void LateUpdate() // Used for the health text on the screen to face the camera all the time
    {
        _healthText.transform.LookAt(_healthText.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    public void SetAtomType(AtomType atomType) // Sets the atom type along with all the properties
    {
        if (atomType == AtomType.FRIENDLY)
        {
            _health = _maxHealth;

            _atomType = atomType;
            PlayerService.Instance.AddAtomToList(this);
            PlayerService.Instance.CameraFollowPlayer();

            // Setting layer and text object
            SetHealthTextValue();
            ShowText();
            _playerLayer = PlayerService.Instance._players[0].GetComponent<AtomController>()._playerLayer;

            // Changing material
            gameObject.GetComponent<MeshRenderer>().material = PlayerService.Instance._players[0].GetComponent<MeshRenderer>().material;
        }
        if(atomType == AtomType.ENEMY)
        {
            _health = (int)(_maxHealth * 0.5);

            _atomType = atomType;
            PlayerService.Instance.RemoveAtomFromList(this);
            PlayerService.Instance.CameraFollowPlayer();

            // Changing state
            _atomSM.ChangeAtomState(AtomState.CHASE);

            // Changing material
            gameObject.GetComponent<MeshRenderer>().material = _enemyMat;

            ShowText();
        }  
    }

    public AtomType GetAtomType()
    {
        return _atomType;
    }

    private void OnCollisionEnter(Collision collision) // Object pool implemented here as well as collisions
    {
        if((_atomType == AtomType.ENEMY || _atomType == AtomType.BOSS) && collision.gameObject.GetComponent<AtomController>())
        {
            if(collision.gameObject.GetComponent<AtomController>()._atomType != AtomType.ENEMY)
            {
                SoundManager.Instance.Play(SourceType.FX1, SoundType.Connect);
                // Spawning from pool
                if(_sparks != null)
                {
                    ObjectPoolManager.SpawnObject(_sparks.gameObject, collision.GetContact(0).point, Quaternion.identity);
                }
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

    public void SetMaxHealthValue(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void SetHealthTextValue()
    {
        _healthText.text = _health.ToString();
    }

    public void ShowText() // For Viewing the health text
    {
        _healthText.text = _health.ToString();
        _healthText.color = new Color(_healthText.color.r, _healthText.color.g, _healthText.color.b, 1f);
        _healthText.DOFade(0f, 1.5f).Restart();
    }

    public void TakeDamage() // Function from interface which manages the damage an atom takes
    {
        _health--;
        ShowText();
        if (_health <= 0)
        {
            if(_atomType == AtomType.FRIENDLY) // Entire sequence used to switch sides
            {
                SetAtomType(AtomType.ENEMY);
            }    
            else if (_atomType == AtomType.ENEMY || _atomType == AtomType.BOSS)
            {
                _atomSM.ChangeAtomState(AtomState.ACTIVATED);
            }    
        }       
    }
}
