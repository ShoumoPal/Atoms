using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LobbyService : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private CanvasGroup _levelSelectionCG;
    [SerializeField] private RectTransform _levelSelectionRT;

    [SerializeField] private float _fadeSpeed;

    private void Awake()
    {
        _playButton.onClick.AddListener(PlayLevelOne);
        _quitButton.onClick.AddListener(QuitGame);
        _levelButton.onClick.AddListener(ShowLevelSelectionMenu);
        _backButton.onClick.AddListener(BackToLobby);
    }

    private void BackToLobby()
    {
        SoundManager.Instance.PlayFX1(SoundType.Button_Click);
        StartCoroutine(FadeOutPanel());
    }

    private void ShowLevelSelectionMenu()
    {
        SoundManager.Instance.PlayFX1(SoundType.Button_Click);
        FadeInPanel();
    }

    private void QuitGame()
    {
        SoundManager.Instance.PlayFX1(SoundType.Button_Click);
        Application.Quit();
    }

    private void PlayLevelOne()
    {
        SoundManager.Instance.PlayFX1(SoundType.Button_Click);
        LevelManagerService.Instance.LoadScene(LevelManagerService.Instance.Levels[0].Name);
    }

    private void FadeInPanel()
    {
        _levelSelectionCG.gameObject.SetActive(true);
        _levelSelectionCG.alpha = 0f;
        _levelSelectionRT.localPosition = new Vector3(0f, -500f, 0f);
        _levelSelectionRT.DOAnchorPos(new Vector2(0f, 0f), _fadeSpeed).SetEase(Ease.OutBounce);
        _levelSelectionCG.DOFade(1f, _fadeSpeed);
    }

    private IEnumerator FadeOutPanel()
    {
        _levelSelectionCG.alpha = 1f;
        _levelSelectionRT.localPosition = new Vector3(0f, 0f, 0f);
        _levelSelectionRT.DOAnchorPos(new Vector2(0f, -500f), _fadeSpeed).SetEase(Ease.InOutQuint);
        _levelSelectionCG.DOFade(0f, _fadeSpeed);

        yield return new WaitForSeconds(_fadeSpeed);

        _levelSelectionCG.gameObject.SetActive(false);
    }
}
