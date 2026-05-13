using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public AudioSource thudSfx;

    Transform visualHolder;
    Animator animator;
    private void Start()
    {
        visualHolder = transform.Find("VisualHolder");
        animator = visualHolder.GetComponentInChildren<Animator>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (animator != null) animator.SetBool("IsDead", true);

            thudSfx.Play();
            GameManager.Instance.HandleDeath();
        }
    }
}
