using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    private bool isTriggered;

    private void Start()
    {
        _text.alpha = 0f;
        isTriggered = false;
    }

    private IEnumerator FadeText()
    {
        _text.DOFade(1f, 2f);
        yield return new WaitForSeconds(3f);
        _text.DOFade(0f, 2f);
    }

    private void Update()
    {
        if (PlayerService.Instance.ArePlayersPresent())
        {
            if (Vector3.Distance(transform.position, PlayerService.Instance._players[0].transform.position) < 10f && !isTriggered)
            {
                isTriggered = true;
                StartCoroutine(FadeText());
            }
        }
    }
}
