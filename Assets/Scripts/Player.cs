using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{
    public const string TAG = "Player";

    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private Transform bulletsTransform;

    [SerializeField]
    private float shootDelay = 0.3f;
    private float shootTimer;
    [SerializeField]
    private float bulletSpeed = 10f;

    private IObjectPool<Bullet> bulletPool;
    public IObjectPool<Bullet> BulletPool => bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, false, 100);

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
        var bulletRb1 = bulletPool.Get().GetComponent<Rigidbody2D>();
        var bulletRb2 = bulletPool.Get().GetComponent<Rigidbody2D>();
        var bulletRb3 = bulletPool.Get().GetComponent<Rigidbody2D>();
        bulletRb1.transform.rotation = Quaternion.identity;
        bulletRb1?.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        bulletRb2.transform.rotation = Quaternion.Euler(0, 0, 15);
        bulletRb2?.AddForce(bulletRb2.transform.up * bulletSpeed, ForceMode2D.Impulse);
        bulletRb3.transform.rotation = Quaternion.Euler(0, 0, -15);
        bulletRb3?.AddForce(bulletRb3.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    private Bullet CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.Init(ReleaseBullet);
        if(bulletsTransform != null)
        {
            bullet.transform.SetParent(bulletsTransform);
        }
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }
}
