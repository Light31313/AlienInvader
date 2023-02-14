using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform enemiesTransform;
    [SerializeField]
    private int enemyCount;
    [SerializeField]
    private float spawnTime;
    private float spawnTimer;

    private int enemyIndex = 0;
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
        var createdEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        if(enemiesTransform != null)
        {
            createdEnemy.transform.SetParent(enemiesTransform);
        }
        var enemyScript = createdEnemy.GetComponent<Enemy>();
        if(enemyScript != null)
        {
            if (enemyIndex >= enemyCount - 1)
            {
                isFinalIndex = true;
            }
            enemyScript.SetLineUpPosition(enemyIndex, isFinalIndex);
            enemyIndex++;
        }
    }
}
