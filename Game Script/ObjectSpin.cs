using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour
{
    public float spinSpeed = 100f;

    private Vector3 spinDirection;

    void Start()
    {
        spinDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Rotate(spinDirection * spinSpeed * Time.deltaTime);
    }
}