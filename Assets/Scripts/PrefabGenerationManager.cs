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
        
        StartCoroutine(GenerationManger());
    }

    private IEnumerator GenerationManger()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            yield return  GenerationBreads();
            yield return new WaitForSeconds(1f);
            yield return GenerationMonsters();
            yield return new WaitForSeconds(1f);

        }
        // ReSharper disable once IteratorNeverReturns
    }


    private IEnumerator GenerationBreads()
    {
        var position = player.position;
        var randNum = (int)Random.Range(0, 3);

        var xPos = randNum switch
        {
            0 => -2,
            1 => 0,
            _ => 2
        };
        
        for (var i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.3f);
            var instancePos = new Vector3(xPos, position.y+0.3f, position.z + 30 + i);
            Instantiate(breadPrefab).transform.position = instancePos;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator GenerationMonsters()
    {
        var playerPos = player.position;
        var generatePos = new Vector3(-2f,0.5f,playerPos.z+35);
        
        for (var i = 0; i <3; i++)
        {
            var randomNum = Random.Range(10, 51);
            //本来数字によってモンスターの種類を変更する。
            GameObject instanceMonster;
            
            if(randomNum<20) instanceMonster = Instantiate(monsters[0]);
            else if(randomNum<40)instanceMonster = Instantiate(monsters[1]);
            else instanceMonster = Instantiate(monsters[2]);
            
            instanceMonster.transform.position = generatePos;
            instanceMonster.GetComponent<Enemy>().SetHp(randomNum);
            generatePos.x += 2f;
        }
        
        
        
        yield return null;
    }
}
