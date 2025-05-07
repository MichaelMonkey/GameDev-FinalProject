using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class ScreenSettings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
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
    }
    public void SetResolution(){
        int currentIndex = resolutionDropdown.value;
        Resolution newResolution = resolutions[currentIndex];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", currentIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
