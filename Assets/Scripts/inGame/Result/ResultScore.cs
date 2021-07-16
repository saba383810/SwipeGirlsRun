using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    //GCAlloc防止
    private static DG.Tweening.Core.DOGetter<int> scoreGetter;
    private static DG.Tweening.Core.DOSetter<int> scoreSetter;

    
    private int dispScore ;
    private static Tween coinTween = null;
    private Text scoreText;


    private void Start()
    {
        scoreText = GetComponent<Text>();
        scoreGetter = GetScore;
        scoreSetter = SetScore;

        scoreGetter = () => dispScore;
        scoreSetter = (val) =>
        {
            dispScore = val;
            scoreText.text = val.ToString("#,0");
        };
    }

    public static void UpdateScore(int num)
    {
        DOTween.Kill(coinTween);
        //coinTween = DOTween.To(GetScore, SetScore, num,1f);
        //GCAlloc防止
        coinTween = DOTween.To(scoreGetter, scoreSetter, num, 3f);
    }

    private int GetScore() =>dispScore;

    private void SetScore(int val)
    {
        dispScore = val;
        scoreText.text = val.ToString("#,0");

    }
}
