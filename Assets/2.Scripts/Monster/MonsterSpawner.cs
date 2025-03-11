using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnALine;
    [SerializeField] private Transform _spawnBLine;
    [SerializeField] private Transform _spawnCLine;

    public bool IsGameOver;
    
    private Transform[] _spawnLines = new Transform[3];
    private MonsterPool _monsterPool;
    private Coroutine _monsterSpawnCo;

    private List<GameObject> _monsterALineTargets = new List<GameObject>();
    private List<GameObject> _monsterBLineTargets = new List<GameObject>();
    private List<GameObject> _monsterCLineTargets = new List<GameObject>();
    private int _randomLine = 1;
    
    
    private void Awake()
    {
        _spawnLines[0] = _spawnCLine;
        _spawnLines[1] = _spawnBLine;
        _spawnLines[2] = _spawnALine;

        _monsterPool = GetComponent<MonsterPool>();
    }

    private void Start()
    {
        _monsterSpawnCo = StartCoroutine(MonsterSpawnRoutine());
    }
    
    /// <summary>
    /// 몬스터 스폰 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator MonsterSpawnRoutine()
    {
        while (true)
        { 
            yield return new WaitUntil(() => !IsGameOver);
            
            yield return new WaitForSeconds(2f);
            MonsterSpawn();
        } 
    }
    
    /// <summary>
    /// 풀에서 몬스터를 가져와서 생성
    /// </summary>
    private void MonsterSpawn()
    {
        int randIndex = UnityEngine.Random.Range(0, 3);
        //int randIndex = 0;
        
        GameObject monster = _monsterPool.GetMonster(_spawnLines[randIndex].position);
        MonsterController monsterController = monster.GetComponent<MonsterController>();
        monsterController.SetSortingLayer(randIndex);
 
        if (monsterController.CheckMonsterPool())
        {
            monsterController.GetMonsterPool(_monsterPool);
        } 
        LayerMask mask;
        switch (randIndex)
        {  
            case 0 :
                _monsterCLineTargets.Add(monster);
                mask = LayerMask.NameToLayer("CLine");
                monsterController.GetTargets(_monsterCLineTargets, mask);
                break;
            case 1 :
                _monsterBLineTargets.Add(monster);
                mask = LayerMask.NameToLayer("BLine");
                monsterController.GetTargets(_monsterBLineTargets, mask);
                break;
            case 2 :
                _monsterALineTargets.Add(monster);
                mask = LayerMask.NameToLayer("ALine");
                monsterController.GetTargets(_monsterALineTargets, mask);
                break; 
        }
        
    }

    
}
