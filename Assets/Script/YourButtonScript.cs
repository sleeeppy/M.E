using System.Collections;
using UnityEngine;

public class YourButtonScript : MonoBehaviour
{
    public Shot shotScript;
    public VideoOption videoOptionScript; 
    public GameObject uiObject;

    private bool isUIVisible = false; 

    private void Start()
    {
        uiObject.SetActive(false);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isUIVisible)
            {
                ToggleUI();
            }
        }
    }

    public void ToggleUI()
    {
        isUIVisible = !isUIVisible;

        if (isUIVisible)
        {
            shotScript.enabled = false;
            
            videoOptionScript.enabled = false;
            
            uiObject.SetActive(true);
        }
        else
        {
            shotScript.enabled = true;
            
            videoOptionScript.enabled = true;
            uiObject.SetActive(false);
        }
    }
}
