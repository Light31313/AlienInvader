using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    MOVE_HEXAGON,
    MOVE_LINE_UP,
    MOVE_DOWN,
    MOVE_UP
}


public class Enemy : MonoBehaviour
{
    private Hexagon hexagon;

    private Vector2 lineUpPosition;

    private List<Transform> hexagonPoints;
    private int currentPoint;
    private bool isFinalEnemy;
    [SerializeField]
    private EnemyState enemyState;
    
    private float minShootingDelayTime = 3f;
    [SerializeField]
    private float maxShootingDelayTime = 8f;
    [SerializeField]
    private float minBulletSpeed = 2f;
    [SerializeField]
    private float maxBulletSpeed = 5f;
    private float shootingTimer;

    [Header("Refer Instance")]
    [SerializeField]
    private PlayerStat playerStat;
    [SerializeField]
    private EnemyStat enemyStat;
    private BulletPool bulletPool;

    [Header("Events")]
    [SerializeField]
    private GameEvent onLastEnemyLineUp;
    [SerializeField]
    private GameEvent onEnemyDie;

    private Rigidbody2D enemy;

    private int currentHealth;

    private bool isCompleteHexagon;
    private Vector3 initialPatrolPosition;

    private Vector3 currentDirection;

    private void Awake()
    {
        bulletPool = GameObject.FindGameObjectWithTag(BulletPool.TAG).GetComponent<BulletPool>();
        hexagon = GameObject.FindGameObjectWithTag(Hexagon.TAG).GetComponent<Hexagon>();
        enemy = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hexagonPoints = hexagon.HexagonPoints;
        enemyState = EnemyState.MOVE_HEXAGON;
        currentPoint = 0;
        currentDirection = (hexagonPoints[currentPoint].position - transform.position).normalized;
        MoveToDirection();
        currentHealth = enemyStat.Health;
        shootingTimer = Random.Range(minShootingDelayTime, maxShootingDelayTime);
    }

    private void Update()
    {
        shootingTimer -= Time.deltaTime;
        if(shootingTimer <= 0)
        {
            Shoot();
            shootingTimer = Random.Range(minShootingDelayTime, maxShootingDelayTime);
        }
    }

    private void FixedUpdate()
    {
        switch(enemyState)
        {
            case EnemyState.MOVE_HEXAGON:
                HexagonMove();
                break;
            case EnemyState.MOVE_LINE_UP:
                LineUpMove();
                break;
            case EnemyState.MOVE_DOWN:
                MoveDown();
                break;
            case EnemyState.MOVE_UP:
                MoveUp();
                break;
        }
    }

    private void Shoot()
    {
        var bulletSpeed = Random.Range(minBulletSpeed, maxBulletSpeed);

        var bulletRb1 = bulletPool.GetBullet().GetComponent<Rigidbody2D>();
        bulletRb1.transform.rotation = Quaternion.Euler(0, 0, 180);
        bulletRb1.velocity = Vector2.down * bulletSpeed;
        bulletRb1.transform.position = transform.position;
    }

    private void HexagonMove()
    {
        var dir = hexagonPoints[currentPoint].position - transform.position;
        if (dir.magnitude <= 0.02f)
        {
            if(isCompleteHexagon)
            {
                enemyState = EnemyState.MOVE_LINE_UP;
                currentDirection = (lineUpPosition - (Vector2)transform.position).normalized;
                MoveToDirection();
                return;
            }

            currentPoint++;
            if (currentPoint >= 6)
            {
                isCompleteHexagon = true;
                currentPoint = 0;
            }
            currentDirection = (hexagonPoints[currentPoint].position - transform.position).normalized;
            MoveToDirection();
        }
    }

    private void LineUpMove()
    {
        var dir = lineUpPosition - (Vector2)transform.position;
        if (dir.magnitude <= 0.02f)
        {
            enemy.velocity = Vector2.zero;
            if (isFinalEnemy)
                onLastEnemyLineUp.Raise();
        }
    }

    public void OnLastEnemyLineUp(object data)
    {
        enemyState = EnemyState.MOVE_DOWN;
        currentDirection = Vector2.down;
        MoveToDirection();
        initialPatrolPosition = transform.position;
    }

    private void MoveToDirection()
    {
        enemy.velocity = currentDirection * enemyStat.Speed;
    }

    private void MoveDown()
    {
        var dir = initialPatrolPosition - transform.position;
        if(dir.magnitude > 4f)
        {
            enemyState = EnemyState.MOVE_UP;
            currentDirection = Vector2.up;
            MoveToDirection();
        }
    }

    private void MoveUp()
    {
        var dir = initialPatrolPosition - transform.position;
        if (dir.magnitude < 0.1f) 
        {
            currentDirection = Vector2.down;
            enemyState = EnemyState.MOVE_DOWN;
            MoveToDirection();
        }
    }

    public void SetLineUpPosition(int index, bool isFinalIndex)
    { 
        isFinalEnemy = isFinalIndex;
        lineUpPosition = new Vector2(-1.5f + index / 3, 4 - index % 3);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        playerStat.Score += enemyStat.ScoreValue;
        Destroy(gameObject);
        onEnemyDie.Raise();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if(player != null)
        {
            player.Die();
        }
    }
}
