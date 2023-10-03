using UnityEngine;

public class LevelCompleteScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<AtomController>())
        {
            Debug.Log("Level Complete");
        }
    }
}
