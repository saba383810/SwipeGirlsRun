using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(EndlessStageDestroy),6f);
            Debug.Log("Destroy開始");
        }
    }

    public void  EndlessStageDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
