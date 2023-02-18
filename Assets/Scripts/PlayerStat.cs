using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(menuName = "PlayerStat")]
public class PlayerStat : ScriptableObject
{
    [SerializeField]
    private GameEvent onScoreChange;

    [SerializeField]                
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            onScoreChange.Raise(score);
        }
    }

    public void ResetStat()
    {
        score = 0;
    }
}
