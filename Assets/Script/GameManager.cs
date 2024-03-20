using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float volume; 

    public AudioSource BGM;

    public Slider mainSceneSlider;
    public Slider gameSceneSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;

        if (BGM != null)
        {
            BGM.volume = volume; 
        }
    }
    
    public void ApplyVolume()
    {
        if (gameSceneSlider != null)
        {
            gameSceneSlider.onValueChanged.RemoveAllListeners();

            gameSceneSlider.value = volume;
            gameSceneSlider.onValueChanged.AddListener((float newValue) =>
            {
                SetVolume(newValue);
            });
        }

        if (BGM != null)
        {
            BGM.volume = volume;
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            mainSceneSlider = GameObject.Find("Canvas/Option/Panel/Image/Music").GetComponent<Slider>();
            mainSceneSlider.onValueChanged.AddListener((float newValue) =>
            {
                SetVolume(newValue);
            });

            mainSceneSlider.value = volume;
        }
        else if (scene.name == "First")
        {
            gameSceneSlider = GameObject.Find("Canvas/Option/Panel/Image/Music").GetComponent<Slider>();
        }

        if (scene.name == "First")
        {
            ApplyVolume();
        }
    }
}
