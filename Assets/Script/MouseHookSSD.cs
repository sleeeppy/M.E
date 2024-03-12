using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HookSSD : MonoBehaviour
{
    private bool move = false;
    private bool stop = false;
    public string Snowball;

    Vector2 mousePos;
    Transform player;
    [SerializeField,Range(0,100)]private float distance;
    [SerializeField]private float speed = 10;
    [SerializeField]private float hookDistance = 10;
    private Rigidbody2D rig;
    private Vector3 direction;
    private bool oneShot;
    private Collider2D col;
    public GameObject Menu;
    public GameObject Option;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;

        player = GameObject.Find("Player").transform;
        rig = player.GetComponent<Rigidbody2D>();
    }
    private void Start(){
        int ignoredLayerIndex = LayerMask.NameToLayer(Snowball);
        Physics.IgnoreLayerCollision(gameObject.layer, ignoredLayerIndex);
    }
    void Update()
    {
        if(!stop) MoveState();
    }

    private void MoveState()
    {
        if (move == true)
        {
            col.enabled = true;

            transform.Translate(speed * Time.deltaTime, 0, 0);
            float _hookDistance = Vector2.Distance(transform.position, player.position);

            if (_hookDistance > hookDistance) move = false;
        }
        else
        {
            if(!Menu.activeSelf || !Option.activeSelf)
            {
                Vector2 mousePos;

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                direction = (mousePos - (Vector2)player.position).normalized;

                transform.position = player.position + direction * distance;

                float angle = Mathf.Atan2(mousePos.y - player.position.y, mousePos.x - player.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Wall"))
        {
            col.enabled = false;
            stop = true;
        }
        if (collision.transform.CompareTag("Frozen"))
        {
            col.enabled = false;
            stop = true;
        }
        if (collision.transform.CompareTag("Bird"))
        {
            col.enabled = false;
            stop = true;
        }
    }
    
}