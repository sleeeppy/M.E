using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.value = GameManager.instance.volume; 
    }

    public void SetMusicVolume()
    {
        float volume = slider.value;
        GameManager.instance.SetVolume(volume);
    }
}
