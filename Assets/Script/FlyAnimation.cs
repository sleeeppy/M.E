using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyAnimation : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            animator.SetBool("Collider", true);
        }
        if (collision.gameObject.CompareTag("Frozen"))
        {
            animator.SetBool("Collider", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("Collider", false);
    }
}