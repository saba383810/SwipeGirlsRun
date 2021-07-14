using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AttackPoint : MonoBehaviour
{
    //GCAlloc防止
    private static DG.Tweening.Core.DOGetter<int> atkPointGetter;
    private static DG.Tweening.Core.DOSetter<int> atkPointSetter;

    
    private int displayAtkPoint;
    private static Tween atkPointTween = null;
    private Text atkText;


    private void Start()
    {
        atkText = GetComponent<Text>();
        atkPointGetter = GetAttackPoint;
        atkPointSetter = SetAttackPoint;

        atkPointGetter = () => displayAtkPoint;
        atkPointSetter = (val) =>
        {
            displayAtkPoint = val;
            atkText.text = val.ToString("#,0");
        };
    }
    
    public static void UpdateAttackPoint(int num)
    {
        DOTween.Kill(atkPointTween);
        //coinTween = DOTween.To(GetScore, SetScore, num,1f);
        //GCAlloc防止
        atkPointTween = DOTween.To(atkPointGetter,atkPointSetter, num, 1f);
    }

    private int GetAttackPoint() => displayAtkPoint;

    private void SetAttackPoint(int val)
    {
        displayAtkPoint = val;
        atkText.text = val.ToString("#,0");
    }
}
