using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerEvents : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "First")
        {
            // Main Scene Slider settings
        }
        else if (scene.name == "Game")
        {
            if (GameManager.instance != null)
            {
                GameObject musicSliderObject = GameObject.Find("Manager/Canvas/Option/Panel/Image/Music");
                if (musicSliderObject != null)
                {
                    Debug.Log("Find!");
                    Slider gameSceneSlider = musicSliderObject.GetComponent<Slider>();
                    if (gameSceneSlider != null)
                    {
                        GameManager.instance.gameSceneSlider = gameSceneSlider;
                    }
                }
            }
        }
    }
}
