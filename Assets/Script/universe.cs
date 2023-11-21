using UnityEngine;

public class universe : MonoBehaviour
{
    public Rigidbody2D rb; // 리지드바디2D 컴포넌트 참조
    private float nomal; // 원래 중력값 저장용 변수

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nomal = rb.gravityScale; // 시작 시 원래 중력값 저장
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("universe")) // "우주" 태그에 닿았을 때
        {
            rb.gravityScale = 0.9f; // 중력값을 0.6으로 설정
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("universe")) // "우주" 태그에서 나왔을 때
        {
            rb.gravityScale = nomal; // 원래 중력값으로 복원
        }
    }
}
