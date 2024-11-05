using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public AudioSource musicSource; 
    public AudioSource vfxSource;   
    public Slider musicSlider;      
    public Slider vfxSlider;        

    void Start()
    {
        
        musicSlider.value = musicSource.volume;
        vfxSlider.value = vfxSource.volume;

        
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        vfxSlider.onValueChanged.AddListener(SetVFXVolume);
    }

    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    
    public void SetVFXVolume(float volume)
    {
        vfxSource.volume = volume;
    }
}
