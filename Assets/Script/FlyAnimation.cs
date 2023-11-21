using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyAnimation : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            animator.SetBool("Collider", true);
        }
        if (collision.gameObject.tag == "Frozen")
        {
            animator.SetBool("Collider", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("Collider", false);
    }
}