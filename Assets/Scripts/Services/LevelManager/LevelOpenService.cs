using UnityEngine;
using UnityEngine.UI;

// Script used on the level selection buttons to go to a particular level

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
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
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
