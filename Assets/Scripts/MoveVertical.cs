using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVertical : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = -5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * moveSpeed, 0);
    }
}
