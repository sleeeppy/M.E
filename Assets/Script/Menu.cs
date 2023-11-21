using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject MenuObject;
    public GameObject Option;
    public bool isPaused = false;
    void Start()
    {
        MenuObject.SetActive(false);
        Option.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                MenuObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                MenuObject.SetActive(false);
                Time.timeScale = 1f;
            }

        }

        else if (Option.activeSelf)
        {
            MenuObject.SetActive(false);
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Option.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
            }
        }

        else
        {
            if (isPaused)
            {
                MenuObject.SetActive(true);
            }
        }
    }
    public void OptionClick()
    {
        MenuObject.SetActive(false);
        Option.SetActive(true);

    }
    public void PlayClick()
    {
        MenuObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
    void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 0:

                Time.timeScale = 0f;

                break;
            case 1:

                Time.timeScale = 1f;

                break;
        }

    }
}