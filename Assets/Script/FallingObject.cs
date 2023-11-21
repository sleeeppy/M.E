using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 initialPosition;
    bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && !isFalling)
        {
            rb.isKinematic = false;
            isFalling = true;
            StartCoroutine(RespawnAfterDelay(2f)); // 2초 뒤에 리스폰
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && !isFalling)
        {
            isFalling = true;
            
            StartCoroutine(RespawnAfterDelay(2f)); // 2초 뒤에 리스폰
        }
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 오브젝트를 처음 위치로 되돌립니다.
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
        isFalling = false;
        gameObject.SetActive(true); // 오브젝트 다시 활성화
    }
}