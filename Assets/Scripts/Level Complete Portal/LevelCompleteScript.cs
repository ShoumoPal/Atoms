using UnityEngine;

public class LevelCompleteScript : MonoBehaviour
{
    private bool isTriggered;

    private void Start()
    {
        isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<AtomController>() && !isTriggered)
        {
            LevelManagerService.Instance.SetCurrentLevelComplete();
            LevelManagerService.Instance.LoadNextScene();
        }
    }
}
