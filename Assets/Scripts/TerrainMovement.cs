using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isPaused || GameManager.Instance.isDead) return;

        Move();

    }

    void Move()
    {
        transform.position += Vector3.back * GameManager.Instance.speed * Time.deltaTime;
    }
}
