using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane { Left, Center, Right }

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    private Lane currentLane = Lane.Center;
    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance.isPaused || GameManager.Instance.isDead) return;

        // INPUT
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        MoveToLane();
    }

    void MoveToLane()
    {
        float targetX = 0f;

        switch (currentLane)
        {
            case Lane.Left:
                targetX = -laneDistance;
                break;
            case Lane.Center:
                targetX = 0f;
                break;
            case Lane.Right:
                targetX = laneDistance;
                break;
        }

        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * laneChangeSpeed
        );
    }

    void MoveLeft()
    {
        if (currentLane == Lane.Center)
            currentLane = Lane.Left;
        else if (currentLane == Lane.Right)
            currentLane = Lane.Center;
    }

    void MoveRight()
    {
        if (currentLane == Lane.Center)
            currentLane = Lane.Right;
        else if (currentLane == Lane.Left)
            currentLane = Lane.Center;
    }
}