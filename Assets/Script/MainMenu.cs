using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject Option;
    // Start is called before the first frame update
    void Start()
    {
        Option.SetActive(false);
    }
    private void Update()
    {
        if (Option.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Option.SetActive(true);

            }
        }
        if (Option.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Option.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void FinishGame()
    {
        Application.Quit();
    }

    public void ToOption()
    {
        Option.SetActive(true);
    }
}
