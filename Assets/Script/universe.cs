using UnityEngine;

public class universe : MonoBehaviour
{
    [SerializeField] private float gravityScale = 0.9f;
    public Rigidbody2D rb;
    private float normal;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normal = rb.gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("universe")) 
        {
            rb.gravityScale = gravityScale;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("universe"))
        {
            rb.gravityScale = normal;
        }
    }
}
