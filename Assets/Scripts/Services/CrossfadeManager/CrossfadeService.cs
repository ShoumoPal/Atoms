using UnityEngine;
using DG.Tweening;
using TMPro;

public class CrossfadeService : GenericMonoSingleton<CrossfadeService>
{
    [SerializeField] private CanvasGroup _imageCG;
    [SerializeField] private TextMeshProUGUI _levelText;

    public float fadeTime;

    public bool IsSceneCovered()
    {
        return _imageCG.alpha == 1;
    }

    public void FadeIn(string text)
    {
        _levelText.text = text;
        _imageCG.DOFade(1f, fadeTime);
    }

    public void FadeOut()
    {
        _imageCG.DOFade(0f, fadeTime);
    }
}
