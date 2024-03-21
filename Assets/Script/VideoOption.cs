using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    List<Resolution> resolutions = new List<Resolution>();
    private Toggle toggle;
    public GameObject timerObject;
    public bool isSkip;
    void Start()
    {
        InitUI();
        toggle = GetComponent<Toggle>();
    }

    void InitUI()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            Resolution resolution = resolutions[i];
            options.Add(resolution.width + "x" + resolution.height + " " + resolution.refreshRate + "Hz");

            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height &&
                resolution.refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;
    }

    public void OnResolutionChanged(int index)
    {
        Resolution resolution = resolutions[index];

        if (fullscreenToggle.isOn)
        {
            Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow, resolution.refreshRate);
        }
        else
        {
            float aspectRatio = (float)resolution.width / resolution.height;
            int height = Mathf.RoundToInt(Screen.width / aspectRatio);
            Screen.SetResolution(Screen.width, height, FullScreenMode.Windowed, resolution.refreshRate);
        }
    }

    public void OnFullscreenToggle(bool isFullscreen)
    {
        if (isFullscreen)
        {
            Resolution resolution = resolutions[resolutionDropdown.value];
            Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow, resolution.refreshRate);
        }
        else
        {
            Resolution resolution = resolutions[resolutionDropdown.value];
            float aspectRatio = (float)resolution.width / resolution.height;
            int height = Mathf.RoundToInt(Screen.width / aspectRatio);
            Screen.SetResolution(Screen.width, height, FullScreenMode.Windowed, resolution.refreshRate);
        }
    }

    public void ApplyVideoSettings()
    {
        OnResolutionChanged(resolutionDropdown.value);
        OnFullscreenToggle(fullscreenToggle.isOn);
    }
    public void OnToggleValueChanged(bool value)
    {
        timerObject.SetActive(value);
    }

    public void OnToggleIsSkip(bool value)
    {
        if (isSkip) isSkip = false;
        else isSkip = true;
        Debug.Log($"isSkip: {isSkip}");
    }
}