using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreadAutoGeneration : MonoBehaviour
{
    [SerializeField] private Transform player =default;
    [SerializeField] private GameObject breadPrefab =default;
    private void Start()
    {
        StartCoroutine(GenerationBread());
    }
    

    private IEnumerator GenerationBread()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            var position = player.position;
            var randNum = Random.Range(-1.5f, 1.5f);
            for (var i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.3f);
                var instancePos = new Vector3 (randNum, position.y, position.z + 15+i);
                Instantiate(breadPrefab).transform.position = instancePos;
            }
        }
    }
}
