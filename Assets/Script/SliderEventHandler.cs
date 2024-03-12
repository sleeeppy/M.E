using UnityEngine;
using UnityEngine.UI;

public class SliderEventHandler : MonoBehaviour
{
    public GameManager gameManager; 

    private Slider slider; 

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float newValue)
    {
        if (gameManager != null)
        {
            gameManager.SetVolume(newValue);
        }
    }

}
