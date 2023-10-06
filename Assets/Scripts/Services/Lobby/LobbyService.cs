using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LobbyService : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _volumeButton;

    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private CanvasGroup _backGroundObjects;

    [SerializeField] private CanvasGroup _levelSelectionCG;
    [SerializeField] private RectTransform _levelSelectionRT;
    [SerializeField] private CanvasGroup _sliderImageCG;

    [SerializeField] private float _fadeSpeed;

    private bool _isVolumeSliderShown;

    private void Awake()
    {
        _playButton.onClick.AddListener(PlayLevelOne);
        _quitButton.onClick.AddListener(QuitGame);
        _levelButton.onClick.AddListener(ShowLevelSelectionMenu);
        _backButton.onClick.AddListener(BackToLobby);
        _volumeButton.onClick.AddListener(ShowVolumeBar);

        _volumeSlider.onValueChanged.AddListener(ChangeValue);


        _isVolumeSliderShown = false;
    }

    private void ChangeValue(float value)
    {
        _audioMixer.SetFloat("Volume", Mathf.Log10(value / 100) * 20);
    }

    private void ShowVolumeBar()
    {
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
        if (_isVolumeSliderShown)
        {
            SliderFadeOut();
        }
        else
        {
            SliderFadeIn();
        }
    }

    private void BackToLobby()
    {
        _backGroundObjects.blocksRaycasts = true;
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
        StartCoroutine(FadeOutPanel());
    }

    private void ShowLevelSelectionMenu()
    {
        _backGroundObjects.blocksRaycasts = false;
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
        FadeInPanel();
    }

    private void QuitGame()
    {
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
        Application.Quit();
    }

    private void PlayLevelOne()
    {
        SoundManager.Instance.Play(SourceType.FX1, SoundType.Button_Click);
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

    private void SliderFadeIn()
    {
        _sliderImageCG.gameObject.SetActive(true);
        _sliderImageCG.DOFade(1f, 0.5f);
        _isVolumeSliderShown = true;
    }

    private void SliderFadeOut()
    {
        _sliderImageCG.DOFade(0f, 0.5f);
        _isVolumeSliderShown = false;
        _sliderImageCG.gameObject.SetActive(false);
    }
}
