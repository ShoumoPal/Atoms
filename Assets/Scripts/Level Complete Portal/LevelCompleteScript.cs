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
            SoundManager.Instance.Play(SourceType.FX1, SoundType.Level_Complete);
            LevelManagerService.Instance.SetCurrentLevelComplete();
            LevelManagerService.Instance.LoadNextScene();
        }
    }
}
