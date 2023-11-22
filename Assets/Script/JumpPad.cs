using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f;

    public Rigidbody2D RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            RB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}