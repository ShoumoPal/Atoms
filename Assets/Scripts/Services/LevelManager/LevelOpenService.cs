using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class LevelOpenService : MonoBehaviour
{
    [SerializeField] private string _levelName;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OpenLevel);
    }

    private void OnEnable()
    {
        LevelStatus status = LevelManagerService.Instance.GetLevelStatus(_levelName);

        if (status == LevelStatus.LOCKED)
            _button.interactable = false;
        else
            _button.interactable = true;
    }

    private void OpenLevel()
    {
        LevelStatus status = LevelManagerService.Instance.GetLevelStatus(_levelName);

        switch (status)
        {
            case LevelStatus.LOCKED:
                Debug.Log("Locked");
                break;
            case LevelStatus.UNLOCKED:
                LevelManagerService.Instance.LoadScene(_levelName);
                break;
            case LevelStatus.COMPLETED:
                LevelManagerService.Instance.LoadScene(_levelName);
                break;
        }
    }
}
