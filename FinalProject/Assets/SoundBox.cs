using UnityEngine;
using System.Collections.Generic;

public class SoundBox : MonoBehaviour
{
    public AudioSource musicBox;
    public AudioSource sfxBox;
    public List<AudioClip> sfx; 
    //public AudioSource systemBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicBox = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        sfxBox = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
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
        if(pick > sfx.Count){
            return;
        }
        sfxBox.resource = sfx[pick];
        sfxBox.Play();
    }
}
