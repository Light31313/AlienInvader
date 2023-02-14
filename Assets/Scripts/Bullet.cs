using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private const float bulletLifeTime = 3f;
    private Action<Bullet> disableAction;

    private void OnEnable()
    {
        StartCoroutine(DisableBullet());
    }

    public void Init(Action<Bullet> action)
    {
        disableAction = action;
    }

    private IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        disableAction?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        disableAction?.Invoke(this);
    }
}
