using UnityEngine;

public class FloatingAstronaut : MonoBehaviour
{
    public float floatSpeed = 1.0f;
    public float floatRange = 1.0f;

    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private float randomOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        initialPosition = rb.position;
        
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed + randomOffset) * floatRange;
        Vector2 newPosition = initialPosition + new Vector2(0f, yOffset);
        
        rb.MovePosition(newPosition);
    }
}
