using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NewRecord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScaleUpDown());
    }

    IEnumerator ScaleUpDown()
    {
        transform.DOScale(new Vector3(1.1f,1.1f,1.1f), 0.5f).SetLoops(-1,LoopType.Yoyo);
        yield return null;
    }
}
