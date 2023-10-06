using DG.Tweening;
using UnityEngine;

public class WallMovementController : MonoBehaviour
{
    [SerializeField] private bool _rotateLeft;
    [SerializeField] private float _duration;
    [SerializeField] private ParticleSystem _sparks;
    [SerializeField] private bool isMovable;

    private Tween _tween;

    private void Start()
    {
        if(isMovable)
            Rotate();
    }

    private void Rotate()
    {
        if (_rotateLeft)
        {
            _tween = transform.DORotate(new Vector3(0f, 360f, 0f), _duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
        else
        {
            _tween = transform.DORotate(new Vector3(0f, -360f, 0f), _duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<AtomController>() != null)
        {
            collision.gameObject.GetComponent<AtomController>().TakeDamage();

            SoundManager.Instance.Play(SourceType.FX1, SoundType.Connect);
            ObjectPoolManager.SpawnObject(_sparks.gameObject, collision.GetContact(0).point, Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}
