using System;
using System.Collections;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(bulletStat.BulletDamage);
            disableAction.Invoke(this);
        }
    }
}
