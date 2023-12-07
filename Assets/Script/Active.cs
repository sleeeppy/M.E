using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    public GameObject PlayerN;
    // Start is called before the first frame update
    void Start()
    {
        PlayerN.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            PlayerN.SetActive(true);
        }
    }
}
