using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenTile : MonoBehaviour
{
    private bool isFrozen;
    [SerializeField] private float slideSpeed;

    private void Update()
    {
        if (isFrozen)
        {
            float slideAmount = slideSpeed * Time.deltaTime;
            transform.Translate(new Vector3(slideAmount, 0f, 0f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Frozen")) isFrozen = true;
        Debug.Log($"slideSpeed: {slideSpeed}");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Frozen")) isFrozen = false;
    }
}