using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] List<PoolableObject> enemies = new List<PoolableObject>();

    private void Awake()
    {
        InitializeEnemyPools();    
    }

    private void InitializeEnemyPools()
    {
        foreach (var enemy in enemies)
        {
            Pooler.InitializePool( enemy );
        }
    }
}
