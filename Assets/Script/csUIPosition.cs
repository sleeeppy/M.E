using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class csUIPosition : MonoBehaviour
{
    void Start()
    {
        float fScaleWidth = ((float)Screen.width / (float)Screen.height) / ((float)1920 / (float)1080 );
        Vector3 vecButtonPos = GetComponent<RectTransform>().localPosition;
        vecButtonPos.x = vecButtonPos.x * fScaleWidth;
        GetComponent<RectTransform>().localPosition = new Vector2 (vecButtonPos.x, vecButtonPos.y);
    }
}
