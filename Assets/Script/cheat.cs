using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cheat : MonoBehaviour
{
    public bool S1 = false;
    public bool S2 = false;
    public int speed = 3;
    public GameObject player;
    public Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(Input.GetKeyDown(KeyCode.F7)) 
        {
            S1 = false;
        } 
        if(Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            S2 = true;
        }   
        if(Input.GetKeyUp(KeyCode.LeftControl)) 
        {
            S2 = false;
        }  
        if(S2 == true)
        {   
        if(Input.GetKeyDown(KeyCode.P))
        {
            transform.position = new Vector3(-3.461f,-3.458f,0);
        }
        if(S1 == true)
        {
            if(Input.GetKeyDown(KeyCode.J)) 
        {
            S1 = false;
        } 
        }
        if(Input.GetKeyDown(KeyCode.J)) 
        {
            S1 = true;
        } 

        }

        if(S1 == true)
        {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
        rig.isKinematic = true;

    }
        if(S1 == false)
        {
            rig.isKinematic = false;

}

}
}
