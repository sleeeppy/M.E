using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    void OnSceneLoaded(Scene scene, LoadSceneMode level)
    {
        switch (level)
        {
            case (LoadSceneMode)1:
                Time.timeScale = 0f;
                break;
            case (LoadSceneMode)0:
                Time.timeScale = 1f;
                break;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}