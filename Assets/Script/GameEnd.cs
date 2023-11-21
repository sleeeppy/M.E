using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    public Text endingText;

    void Start()
    {
        float currentTime = PlayerPrefs.GetFloat("GameTimer_CurrentTime", 0.0f);
        UpdateTimerText(currentTime);
    }

    void UpdateTimerText(float currentTime)
    {
        int milliseconds = Mathf.FloorToInt((currentTime * 100.0f) % 100);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int minutes = Mathf.FloorToInt((currentTime / 60.0f) % 60);
        int hours = Mathf.FloorToInt((currentTime / 3600.0f) % 24);

        endingText.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, milliseconds);
    }
}