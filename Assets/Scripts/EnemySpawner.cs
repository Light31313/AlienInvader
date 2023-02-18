using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameEvent onAllEnemiesDie;
    [SerializeField]
    private Enemy enemyPrefab;
    [SerializeField]
    private int enemyCount;
    [SerializeField]
    private float spawnTime;
    private float spawnTimer;

    private int enemyIndex = 0;
    private int enemyAlive = 0;
    private bool isFinalIndex = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyIndex >= enemyCount)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnTime;
        }
    }

    private void SpawnEnemy()
    {
        var createdEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        var enemyScript = createdEnemy.GetComponent<Enemy>();
        if(enemyScript != null)
        {
            if (enemyIndex >= enemyCount - 1)
            {
                isFinalIndex = true;
            }
            enemyScript.SetLineUpPosition(enemyIndex, isFinalIndex);
            enemyIndex++;
            enemyAlive++;
        }
    }

    public void OnEnemyDie(object data)
    {
        enemyAlive--;
        if(enemyAlive <= 0 && isFinalIndex)
        {
            onAllEnemiesDie.Raise();
        }
    }
}
