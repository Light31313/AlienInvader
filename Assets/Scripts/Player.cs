using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{
    public const string TAG = "Player";

    [SerializeField]
    private BulletPool bulletPool;
    [SerializeField]
    private Transform bulletsTransform;
    [SerializeField]
    private GameEvent onPlayerDie;

    [SerializeField]
    private float shootDelay = 0.3f;
    private float shootTimer;
    [SerializeField]
    private float bulletSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = shootDelay;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootDelay;
        }
    }

    private void Shoot()
    {
        SpawnBullet(bulletPool.GetBullet().GetComponent<Rigidbody2D>(), 0);
        SpawnBullet(bulletPool.GetBullet().GetComponent<Rigidbody2D>(), 15);
        SpawnBullet(bulletPool.GetBullet().GetComponent<Rigidbody2D>(), -15);
    }

    private void SpawnBullet(Rigidbody2D bulletRb, float bulletAngle)
    {
        bulletRb.transform.rotation = Quaternion.Euler(0, 0, bulletAngle);
        bulletRb.velocity = bulletSpeed * bulletRb.transform.up;
        bulletRb.transform.position = transform.position;
    }

    public void Die() {
        onPlayerDie.Raise(null);
        Destroy(gameObject);
    }
}
