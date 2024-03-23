using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FrozenTile : MonoBehaviour
{
    public bool isFrozen;
    public bool isTouchedGround;
    [SerializeField] private float slideSpeed;
    public float savedYPosition;
    private Rigidbody2D playerRigidbody;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isFrozen && isTouchedGround && playerRigidbody.velocity.magnitude <= 0.2f)
        {
            savedYPosition = transform.position.y;
        }
        
        if (isFrozen && isTouchedGround && transform.position.y <= savedYPosition)
        {
            float slideAmount = slideSpeed * Time.deltaTime;
            transform.Translate(new Vector3(slideAmount, 0f, 0f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Frozen"))
        {
            isFrozen = true;
            Debug.Log($"slideSpeed: {slideSpeed}, {isFrozen}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Frozen"))
        {
            isFrozen = false;
            Debug.Log($"slideSpeed: {slideSpeed}, {isFrozen}");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isTouchedGround = true;
        if(isFrozen) savedYPosition = transform.position.y;
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        isTouchedGround = false;
    }
}