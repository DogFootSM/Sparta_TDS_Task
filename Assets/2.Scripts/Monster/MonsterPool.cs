using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] private GameObject _monsterPrefab;
    
    private Queue<GameObject> _monsterPool = new Queue<GameObject>();
    private int _totalMonsterCount = 10;

    private void Awake()
    {
        MonsterSpawn();
    }

    private void MonsterSpawn()
    {
        for (int i = 0; i < _totalMonsterCount; i++)
        {
            GameObject instance = Instantiate(_monsterPrefab);
            instance.SetActive(false);
            _monsterPool.Enqueue(instance);
        } 
    }

    private int temp = 0;
    /// <summary>
    /// 풀에 있는 몬스터를 넘겨줌 
    /// </summary>
    /// <param name="spawnPosition">몬스터를 스폰할 라인</param>
    /// <returns></returns>
    public GameObject GetMonster(Vector3 spawnPosition)
    {
        GameObject monster;
        
        if (_monsterPool.Count > 0)
        {
            monster = _monsterPool.Dequeue();
            // monster.name = $"{monster.name} - {temp++}";
            // monster.transform.position = spawnPosition;
            // monster.SetActive(true);
            // return monster; 
        }
        else
        {
            monster = Instantiate(_monsterPrefab);
        } 
        
        monster.name = $"{monster.name} - {temp++}";
        monster.transform.position = spawnPosition;
        monster.SetActive(true);
        return monster; 

        //return null;
    }

    public void ReturnMonster(GameObject monster)
    {
        monster.SetActive(false);
        _monsterPool.Enqueue(monster);
    }
    
}
