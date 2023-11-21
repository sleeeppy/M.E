using UnityEngine;

public class FloatingAstronaut : MonoBehaviour
{
    public float floatSpeed = 1.0f; // 둥둥 떠다니는 속도 조절
    public float floatRange = 1.0f; // 둥둥 떠다니는 범위 조절

    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private float randomOffset;

    void Start()
    {
        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();

        // 초기 위치 저장
        initialPosition = rb.position;

        // 초기 위치에서 무작위한 높이(offset) 설정
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        // 우주인을 위아래로 둥둥 떠다니게 하기
        float yOffset = Mathf.Sin(Time.time * floatSpeed + randomOffset) * floatRange;
        Vector2 newPosition = initialPosition + new Vector2(0f, yOffset);

        // Rigidbody2D를 사용하여 위치 변경
        rb.MovePosition(newPosition);
    }
}
