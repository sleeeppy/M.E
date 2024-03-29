using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cheat : MonoBehaviour
{
    public bool S1 = false;
    public bool S2 = false;
    public int speed = 7;
    public GameObject player;
    public Rigidbody2D rig;
    public bool isCheat;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    void Update()
    {  
        if(Input.GetKeyDown(KeyCode.F7)) S1 = false;

        if(Input.GetKeyDown(KeyCode.LeftControl)) S2 = true;

        if(Input.GetKeyUp(KeyCode.LeftControl)) S2 = false;
        
        if(S2)
        {   
            if(Input.GetKeyDown(KeyCode.P))
            {
                transform.position = new Vector3(-3.461f,-3.458f,0);
            }
            if(S1)
            {
                if(Input.GetKeyDown(KeyCode.C)) 
                {
                    S1 = false;
                } 
            }
            if(Input.GetKeyDown(KeyCode.C)) 
            {
                S1 = true;
            } 
        }

        if(S1)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
            rig.isKinematic = true;
            isCheat = true;

        }
        if(!S1)
        {
            isCheat = false;
            rig.isKinematic = false;
        }
    }
}
