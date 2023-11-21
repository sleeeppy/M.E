using UnityEngine;

public class ReduceMovementOnGround : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 충돌한 오브젝트가 ground 태그인 경우, x축 속도를 70% 감소시킵니다.
            Vector2 velocity = rb.velocity;
            velocity.x *= 0.3f;
            rb.velocity = velocity;
        }
    }
}