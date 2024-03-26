using UnityEngine;

public class universe : MonoBehaviour
{
    [SerializeField] private float gravityScale = 0.6f;
    [SerializeField] private float drag = 0.4f;
    [SerializeField] private float angularDrag = 0.4f;
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
            rb.drag = drag;
            rb.angularDrag = angularDrag;
            //Debug.Log($"gravityScale : {gravityScale}, {other.name}");
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
