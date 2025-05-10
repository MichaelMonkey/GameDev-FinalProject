using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScreenSettings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;
    Resolution[] resolutions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resolutions = Screen.resolutions;
        Resolution currentResolution = Screen.currentResolution;
        
        int currentIndex = PlayerPrefs.GetInt("resolution", -1);
        if(currentIndex == -1){
            currentIndex = Array.IndexOf(resolutions, currentResolution);
        }

        for(int i = 0; i < resolutions.Length; i++){
            TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData(resolutions[i].ToString());
            resolutionDropdown.options.Add(newOption);
        }
        resolutionDropdown.value = currentIndex;

        int fullscreenStatus = PlayerPrefs.GetInt("fullscreen", 0);
        bool isFullscreen = (fullscreenStatus == 1);
        fullscreenToggle.isOn = isFullscreen;
        SetFullscreen(isFullscreen);

        int vSyncCount = PlayerPrefs.GetInt("vsync", 1);
        bool isVsync = (vSyncCount == 1); 
        vsyncToggle.isOn = isVsync;
        SetVsync(isVsync);
    }
    public void SetResolution(){
        int currentIndex = resolutionDropdown.value;
        Resolution newResolution = resolutions[currentIndex];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", currentIndex);
    }

    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
        int fullscreenStatus = 0;
        if(isFullscreen){
            fullscreenStatus = 1;
        }
        PlayerPrefs.SetInt("fullscreen", fullscreenStatus);
        if(isFullscreen){
            print("Enabled fullscreen");
        } else {
            print("Disabled fullscreen");
        }
    }

    public void SetVsync(bool isVsync){
        int vSyncCount = 0;
        if(isVsync){
            vSyncCount = 1;
        }
        QualitySettings.vSyncCount = vSyncCount;
        PlayerPrefs.SetInt("vsync", vSyncCount);
        if(isVsync){
            print("Enabled vsync");
        } else {
            print("Disabled vsync");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
