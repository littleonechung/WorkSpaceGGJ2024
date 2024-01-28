using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    [SerializeField]
    GameObject successImg = null;
    [SerializeField]
    GameObject failImg = null;
    [SerializeField]
    CanvasGroup canvasGroup;
    [SerializeField]
    GodManager godManager = null;

    [SerializeField]
    float fadeDuration = 1;


    public void SuccessEnd()
    {
        gameObject.SetActive(true);
        successImg.SetActive(true);
        failImg.SetActive(false);
    }
    public void FailEnd()
    {
        gameObject.SetActive(true);
        successImg.SetActive(false);
        failImg.SetActive(true);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void FadeIn()
    {
        canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad);
    }

    public void FadeOut()
    {
        canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad);
    }

}
