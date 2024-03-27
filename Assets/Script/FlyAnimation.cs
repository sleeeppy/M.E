using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyAnimation : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D playerRigidbody;
    private bool isGround;

    public FrozenTile frozenTile;
    
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        frozenTile = GetComponent<FrozenTile>();
    }
    
    private void Update()
    {
        if (!frozenTile.isFrozen)
        {
            if(playerRigidbody.velocity.magnitude <= 0.05f)
            {
                animator.SetBool("isSlide", false);
            }
            else
            {
                if (isGround)
                {
                    animator.SetBool("isSlide", true);
                }
                else
                {
                    animator.SetBool("isSlide", false);
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            animator.SetBool("Collider", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("Collider", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isGround = false;
        }
    }
}