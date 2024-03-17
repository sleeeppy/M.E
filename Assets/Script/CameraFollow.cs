using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float minY = -1f;
    public float maxY = 178f;

    private bool shouldFollow = true;
    private float speed = 5.0f;

    void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;

        if (shouldFollow)
        {
            cameraPosition.y = Mathf.Max(target.position.y, minY);
        }

        if (Input.GetKey(KeyCode.K))
        {
            cameraPosition.y += speed * Time.deltaTime;
            shouldFollow = false;
            
            if (cameraPosition.y > maxY)
                cameraPosition.y = maxY;
        }
    
        if (Input.GetKey(KeyCode.J))
        {
            cameraPosition.y -= speed * Time.deltaTime;

            if (cameraPosition.y < minY)
                cameraPosition.y = minY;

            shouldFollow = false;
        }

        else if (!Input.anyKey)
        {
            shouldFollow = true;
        }

        transform.position = cameraPosition;
    }
}