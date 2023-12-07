using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlMove : MonoBehaviour
{
    public float jumpPower;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump"))
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
