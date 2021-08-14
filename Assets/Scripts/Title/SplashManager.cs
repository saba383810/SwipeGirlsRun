using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SplashManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup backGroundImg =default;
    [SerializeField] private CanvasGroup splashImg = default;
    IEnumerator Start()
    {
        backGroundImg.gameObject.SetActive(true);
        backGroundImg.alpha = 1;
        yield return new WaitForSeconds(1f);
        splashImg.DOFade(1, 1);
        yield return new WaitForSeconds(1.5f);
        splashImg.DOFade(0, 1);
        yield return new WaitForSeconds(1.5f);
        splashImg.gameObject.SetActive(false);
        backGroundImg.DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        backGroundImg.gameObject.SetActive(false);
    }
}
