using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    public AudioSource BGM;
    public Slider Slider;
    public void SetMusicVolume()
    {
        BGM.volume = Slider.value;
    }
}
