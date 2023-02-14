using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private Hexagon hexagon;

    private Vector2 lineUpPosition;

    private List<Transform> hexagonPoints;
    private int currentPoint;
    private bool isFinalIndex;

    [Header("Events")]
    [SerializeField]
    private GameEvent onLastEnemyLineUp;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        hexagon = GameObject.FindGameObjectWithTag(Hexagon.TAG).GetComponent<Hexagon>();
        hexagonPoints = hexagon.HexagonPoints;
        HexagonMove();
    }

    private void HexagonMove()
    {
        if (currentPoint >= hexagonPoints.Count)
        {
            transform.DOMove(hexagonPoints[0].position, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                LineUpMove();
            });
            return;
        }

        transform.DOMove(hexagonPoints[currentPoint].position, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            currentPoint++;
            HexagonMove();
        });
    }

    private void LineUpMove()
    {
        transform.DOMove(lineUpPosition, 2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if(isFinalIndex)
            {
                onLastEnemyLineUp.Raise();
            }
        });
    }

    public void MoveDown()
    {
        transform.DOMoveY(transform.position.y - 4, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveUp();
        });
    }

    private void MoveUp()
    {
        transform.DOMoveY(transform.position.y + 4, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveDown();
        });
    }

    public void SetLineUpPosition(int index, bool isFinalIndex)
    {
        this.isFinalIndex = isFinalIndex;
        lineUpPosition = new Vector2(-1.5f + index / 3, 4 - index % 3);
    }
}
