using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : GenericLazySingleton<UIService>
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _restartButton_PM;
    [SerializeField] private Button _restartButton_GM;
    [SerializeField] private Button _homeButton_PM;
    [SerializeField] private Button _homeButton_GM;
    [SerializeField] private CanvasGroup _backDropCG;
    [SerializeField] private RectTransform _pauseMenuRT;
    [SerializeField] private CanvasGroup _pauseMenuCG;
    [SerializeField] private RectTransform _gameOverMenuRT;
    [SerializeField] private CanvasGroup _gameOverMenuCG;
    [SerializeField] private TextMeshProUGUI _atomCountText;

    private bool isMenuRunning;

    private void Awake()
    {
        isMenuRunning = false;
        _restartButton_PM.onClick.AddListener(RestartLevel);
        _restartButton_GM.onClick.AddListener(RestartLevel);
        _pauseButton.onClick.AddListener(PauseGame);
        _resumeButton.onClick.AddListener(ResumeGame);
        _homeButton_PM.onClick.AddListener(BackToLobby);
        _homeButton_GM.onClick.AddListener(BackToLobby);
    }

    private void Update()
    {
        if (EventService.Instance.InvokeIsGameOver() && !isMenuRunning)
        {
            isMenuRunning = true;
            SoundManager.Instance.Stop(SourceType.BG1);
            SoundManager.Instance.Play(SourceType.FX1, SoundType.Game_Over);
            StartCoroutine(ShowMenu(_gameOverMenuCG, _gameOverMenuRT));
        }

        _atomCountText.text = "Atoms : " + PlayerService.Instance._players.Count.ToString();
    }

    private void BackToLobby()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
        SoundManager.Instance.Play(SourceType.BG1, SoundType.Background1);
        LevelManagerService.Instance.LoadScene("Lobby");
    }

    private void ResumeGame()
    {
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
        StartCoroutine(HideMenu(_pauseMenuCG, _pauseMenuRT));
    }

    private void PauseGame()
    {
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
        StartCoroutine(ShowMenu(_pauseMenuCG, _pauseMenuRT));
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
        SoundManager.Instance.Play(SourceType.BG1, SoundType.Background1);
        LevelManagerService.Instance.ReloadScene();
    }

    private IEnumerator ShowMenu(CanvasGroup CG, RectTransform RT)
    {
        ShowBackDrop();
        CG.gameObject.SetActive(true);
        RT.transform.localPosition = new Vector3(0f, -500f, 0f);
        RT.DOAnchorPos(new Vector2(0f, 0f), 0.5f).SetEase(Ease.Linear);
        CG.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f; 
    }

    private IEnumerator HideMenu(CanvasGroup CG, RectTransform RT)
    {
        Time.timeScale = 1f;
        RT.transform.localPosition = new Vector3(0f, 0f, 0f);
        RT.DOAnchorPos(new Vector2(0f, -500f), 0.5f).SetEase(Ease.Linear);
        CG.DOFade(0f, 0.5f);
        HideBackDrop();
        yield return new WaitForSeconds(0.5f);
        CG.gameObject.SetActive(false);
    }

    private void ShowBackDrop()
    {
        _backDropCG.gameObject.SetActive(true);
        _backDropCG.DOFade(1f, 1.5f);
    }

    private void HideBackDrop()
    {
        _backDropCG.gameObject.SetActive(false);
    }
}
