using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WindowAnimationManager : MonoBehaviour
{
    public static void SlideInWindow(CanvasGroup window)
    {
        window.transform.DOMove(new Vector3(0, 0, 0), 1);
    }
}
