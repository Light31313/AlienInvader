using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public const string TAG = "Hexagon";

    private List<Transform> hexagonPoints;

    // Start is called before the first frame update
    void Awake()
    {
        hexagonPoints = new List<Transform>();

        foreach(Transform point in transform)
        {
            hexagonPoints.Add(point.transform);
        }
    }

    public List<Transform> HexagonPoints => hexagonPoints;
}
