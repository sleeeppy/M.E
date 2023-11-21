using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 originalPos;
    Quaternion originalRotation;
    bool isTouchingWall;
    float timeElapsed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPos = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (isTouchingWall)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 4f)
            {
                ResetPosition();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            timeElapsed = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
            timeElapsed = 0f;
        }
    }

    private void ResetPosition()
    {
        rb.isKinematic = true;
        transform.position = originalPos;
        transform.rotation = originalRotation;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}