using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float minY = -1f;
    public float maxY = 178f;
    public float Range; 

    private bool shouldFollow = true;
    private float speed = 7f;
    
    private float playerPos;

    void LateUpdate()
    {
        playerPos = target.transform.position.y;
        Vector3 cameraPosition = transform.position;
        
        if (shouldFollow)
        {
            cameraPosition.y = Mathf.Max(target.position.y, minY);
        }

        if (Input.GetKey(KeyCode.W))
        {
            cameraPosition.y += speed * Time.deltaTime;
            shouldFollow = false;

            if (cameraPosition.y > playerPos + Range)
            {
                cameraPosition.y = (playerPos + Range);
            }
            else if (cameraPosition.y > maxY) cameraPosition.y = maxY;

        }
    
        if (Input.GetKey(KeyCode.S))
        {
            cameraPosition.y -= speed * Time.deltaTime;

            if (cameraPosition.y < playerPos - Range)
            {
                cameraPosition.y = playerPos - Range;
            }
            else if (cameraPosition.y < minY) cameraPosition.y = minY;
            
            shouldFollow = false;
        }

        else if (!Input.anyKey)
        {
            shouldFollow = true;
        }

        transform.position = cameraPosition;
    }
}