using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pltlqkf : MonoBehaviour
{
    private float playerX;
    public float forceMagnitude = 5f;
    public float bounceDirectionX = 1f;
    private Rigidbody2D rb;
    public float hitForce = 400f;
    public float upwardForce = 5f;

    void Start()
    {
        playerX = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float currentX = transform.position.x;

        if (currentX != playerX)
        {
            Vector3 rotation = transform.rotation.eulerAngles;

            if (currentX < playerX)
            {
                rotation.y = 180;
            }
            else
            {
                rotation.y = 0;
            }

            transform.rotation = Quaternion.Euler(rotation);

            playerX = currentX;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FallingTrap"))
        {
            Vector2 force = new Vector2(bounceDirectionX * forceMagnitude, 0f);
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Snowball"))
        {
            Vector2 forceDirection = collision.contacts[0].normal;
            Vector2 force = forceDirection * hitForce * 2f;

            rb.velocity = Vector2.zero;

            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}