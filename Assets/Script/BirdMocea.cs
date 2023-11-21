using System.Collections;
using UnityEngine;

public class BirdMocea : MonoBehaviour
{
    public float movementSpeed = 1f;
    private bool isAttached = false;
    private Transform attachedObject;
    private bool isM = false;
    public Animator animator;
    public bool isL = false;
    public bool isR = false;

    bool isLeft = true;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(isLeft == true && isM == false)
        {
            transform.position += new Vector3(-0.01f,0,0);
        }
        if(isLeft == false && isM == false)
        {
            transform.position += new Vector3(0.01f,0,0);
        }
        if(isM == true)
        {
            transform.position = transform.position;
        }
        if(isL == true)
        {
            isM = true;
            if(Input.GetKeyDown(KeySetting.keys[KeyAction.LEFT]))
            {
            isM = false;
            isL = false;
            animator.SetBool("gal", false);
            }
        }
        if(isR == true)
        {
            isM = true;
            if(Input.GetKeyDown(KeySetting.keys[KeyAction.RIGHT]))
            {
            isM = false;
            isR = false;
            animator.SetBool("gal", false);
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EndPoint"))
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false; 
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
        }
        if(collision.CompareTag("Left"))
        {
            isM = true;
            isL = true;
        }
        if(collision.CompareTag("Right"))
        {
            isM = true;
            isR = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Left")
        {
            isM = true;
            animator.SetBool("gal", true);
            isL = true;
            
        }
        if(coll.gameObject.tag == "Right")
        {
            isM = true;
            animator.SetBool("gal", true);
            isR = true;
        }
    }

}