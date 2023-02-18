using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public const string TAG = "BulletPool";

    [SerializeField]
    private Bullet bulletPrefab;
    private IObjectPool<Bullet> bulletPool;

    private void Start()
    {
        bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, false, 100);
    }

    private Bullet CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.Init(ReleaseBullet);
        bullet.transform.SetParent(transform);
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
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

    public Bullet GetBullet()
    {
        return bulletPool.Get();
    }
}
