using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected BulletStat bulletStat;
    protected Action<Bullet> disableAction;

    private void Update()
    {
        CheckIfInCamera();
    }

    public void Init(Action<Bullet> action)
    {
        disableAction = action;
    }

    private void CheckIfInCamera()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (!(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0))
        {
            disableAction?.Invoke(this);
        }
    }
}
