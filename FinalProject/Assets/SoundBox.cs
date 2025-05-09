using UnityEngine;
using System.Collections.Generic;

public class SoundBox : MonoBehaviour
{
    public AudioSource musicBox;
    public AudioSource sfxBox;
    public AudioSource systemBox;
    public List<AudioClip> SFXSounds;
    public List<AudioClip> SystemSounds;
    //public AudioSource systemBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //musicBox = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        //sfxBox = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        //systemBox = transform.GetChild(2).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startMusic(){
        musicBox.Play();
    }

    public void stopMusic(){
        musicBox.Stop();
    }

    
    public void playSFX(int pick){
        if(pick > SFXSounds.Count-1){
            return;
        }
        sfxBox.resource = SFXSounds[pick];
        sfxBox.Play();
    }
    public void playSystem(int pick){
        if(pick > SystemSounds.Count-1){
            return;
        }
        sfxBox.resource = SystemSounds[pick];
        sfxBox.Play();
    }
}
