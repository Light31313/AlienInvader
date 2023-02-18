using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(menuName = "EnemyStat")]
public class EnemyStat : ScriptableObject
{
    [SerializeField]                      
    private int scoreValue = 1;
    public int ScoreValue => scoreValue;

    [SerializeField]
    private int health = 5;
    public int Health => health;

    [SerializeField]
    private float speed = 10f;
    public float Speed => speed;
}
