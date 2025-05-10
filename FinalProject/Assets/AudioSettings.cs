using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioSettings : MonoBehaviour
{
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;
    public Slider systemSlider;
    public AudioMixer audioMixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float masterVolume = PlayerPrefs.GetFloat("masterVolume", 0f);
        masterSlider.value = masterVolume;
        SetMasterVolume();
        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0f);
        sfxSlider.value = sfxVolume;
        SetSFXVolume();
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", -10f);
        musicSlider.value = musicVolume;
        SetSFXVolume();
        float systemVolume = PlayerPrefs.GetFloat("systemVolume", 0f);
        systemSlider.value = systemVolume;
        SetSystemVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetVolume(string groupName, float value){
        audioMixer.SetFloat(groupName, value);
    }
    
    public void SetMasterVolume(){
        SetVolume("MasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
    }
    public void SetSFXVolume(){
        SetVolume("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }
    public void SetMusicVolume(){
        SetVolume("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
    public void SetSystemVolume(){
        SetVolume("SystemVolume", systemSlider.value);
        PlayerPrefs.SetFloat("systemVolume", systemSlider.value);
    }
}
