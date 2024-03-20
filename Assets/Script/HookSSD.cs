using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*public class MouseHookSSD : MonoBehaviour
{
    private bool move = false;
    private bool stop = false;

    Vector2 mousePos;
    Transform player;
    [SerializeField,Range(0,100)]private float distance;
    [SerializeField]private float speed = 10;
    [SerializeField]private float force = 10;
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
        rig.isKinematic  = false;
    }

    void Update()
    {
        if(move == false)
        {
            col.enabled = false;
        }
        if(!stop) MoveState();
    }

private void MoveState()
    {
        if (move == true)
        {
            col.enabled = true;

            transform.Translate(speed * Time.deltaTime, 0, 0);
            float hookDistance = Vector2.Distance(transform.position, player.position);

            if (10 < hookDistance) move = false;
        }
        else
        {
            if(!Menu.activeSelf || !Option.activeSelf)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                var position = player.position;
                direction = (mousePos - (Vector2)position).normalized;

                transform.position = position + direction * distance;

                float angle = Mathf.Atan2(mousePos.y - position.y, mousePos.x - position.x) * Mathf.Rad2Deg;
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
    }

    public void ResetHookPos()
    {
        if (stop == false) return;

        col.enabled = false;

        oneShot = false;

        stop = false; 
        move = false;
    }
    public void ResetHookPos1()
    {
        if (stop == false) return;

        col.enabled = false;

        oneShot = false;

        stop = false; 
        move = false;
    }
    public void Follow()
    {
        if (stop == true && oneShot == false)
        {
            oneShot = true;

            Vector2 targetPosition = transform.position;
            rig.AddForce((targetPosition - (Vector2)player.position).normalized * force, ForceMode2D.Impulse);

            return;
        }

        move = true;
    }

}*/