using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class main : MonoBehaviour
{
    public Slider VolumeSlider;
    public GameObject Sound;
    public AudioMixer masterMixer;
    
    //private void Awake() => Init();

    public void Start(){
        VolumeSlider.minValue = 0.0001f;
        VolumeSlider.maxValue = 1f;


        VolumeSlider.value = StaticVar.Volume;
        SetVolume(VolumeSlider.value);
    }

   public void SetVolume(float sliderValue)
    {
        masterMixer.SetFloat("MixerVolume",Mathf.Log10(sliderValue) * 20);
    }
}
