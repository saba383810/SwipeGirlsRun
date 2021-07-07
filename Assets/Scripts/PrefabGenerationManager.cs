using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PrefabGenerationManager : MonoBehaviour
{
    [SerializeField] private Transform player =default;
    [SerializeField] private GameObject breadPrefab =default;
    [SerializeField] private GameObject[] monsters =new GameObject[10];
    private void Start()
    {
        StartCoroutine(GenerationBreads());
    }
    

    private IEnumerator GenerationBreads()
    {
        var position = player.position;
        var randNum = Random.Range(-1.5f, 1.5f);
        for (var i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.3f);
            var instancePos = new Vector3(randNum, position.y+0.5f, position.z + 15 + i);
            Instantiate(breadPrefab).transform.position = instancePos;
        }
    }

    private IEnumerator GenerationMonsters()
    {
        
        yield return null;
    }
}
