using UnityEngine;
using DG.Tweening;
using TMPro;

public class CrossfadeService : GenericMonoSingleton<CrossfadeService>
{
    [SerializeField] private CanvasGroup _imageCG;
    [SerializeField] private TextMeshProUGUI _levelText;

    public float fadeTime;


    public void FadeIn(string text)
    {
        _levelText.text = text;
        _imageCG.alpha = 0;
        _imageCG.DOFade(1f, fadeTime);
    }

    public void FadeOut()
    {
        _imageCG.alpha = 1;
        _imageCG.DOFade(0f, fadeTime);
    }
}
