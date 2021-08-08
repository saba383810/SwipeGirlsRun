using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PrefabGenerationManager : MonoBehaviour
{
   private Transform player;
    [SerializeField] private GameObject breadPrefab =default;
    [SerializeField] private GameObject[] monsters =new GameObject[10];
    [SerializeField] private GameObject[] terrainPrefabs =new GameObject[5];
    [SerializeField] private bool isEndless =default;
    private int terrainCnt = 1;
    private int breadsCnt = 1;
    private int monstersCnt = 1;

    private float time;
    private void Start()
    {
        
        StartCoroutine(GenerationBreads());
        StartCoroutine(GenerationMonsters());
        if(isEndless) StartCoroutine(GenerationTerrain());
        
    }
    private IEnumerator GenerationBreads()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            yield return new WaitUntil(() => player.transform.position.z > breadsCnt * 15);
            var position = player.position;
            var randNum = (int) Random.Range(0, 3);

            var xPos = randNum switch
            {
                0 => -2,
                1 => 0,
                _ => 2
            };

            for (var i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.3f);
                var instancePos = new Vector3(xPos, position.y + 0.3f, breadsCnt* 15 + 20 + i);
                Instantiate(breadPrefab).transform.position = instancePos;
            }
            breadsCnt++;
        }
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator GenerationMonsters()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            yield return new WaitUntil(() => player.transform.position.z > monstersCnt * 15);
            var playerPos = player.position;
            var generatePos = new Vector3(-2f, 0.5f, monstersCnt * 15 + 30);

            for (var i = 0; i < 3; i++)
            {
                var randomNum = Random.Range(10, 51);
                //本来数字によってモンスターの種類を変更する。
                GameObject instanceMonster;

                if (randomNum < 20) instanceMonster = Instantiate(monsters[0]);
                else if (randomNum < 40) instanceMonster = Instantiate(monsters[1]);
                else instanceMonster = Instantiate(monsters[2]);

                instanceMonster.transform.position = generatePos;
                instanceMonster.GetComponent<Enemy>().SetHp(randomNum);
                generatePos.x += 2f;
            }

            monstersCnt++;
            yield return null;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator GenerationTerrain()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            yield return new WaitUntil(() => player.transform.position.z > (100 * terrainCnt) - 80);
            var randNum = Random.Range(0, 1);
            Instantiate(terrainPrefabs[randNum]).transform.position = new Vector3(-30, 0.5f, terrainCnt * 100);
            terrainCnt++;
            yield return null;
        }
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }
    
    
}
