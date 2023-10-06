using UnityEngine;

/* Script used to return the particle effect back into the pool */

public class ReturnParticleToPool : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
