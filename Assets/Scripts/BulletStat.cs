using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(menuName = "BulletStat")]
public class BulletStat : ScriptableObject
{
    [SerializeField]                      
    private int bulletDamage = 1;
    public int BulletDamage => bulletDamage;
}
