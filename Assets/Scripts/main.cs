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
        Debug.Log("VolumeMain"+StaticVar.Volume);
        VolumeSlider.value = StaticVar.Volume;

        //SetVolume(VolumeSlider.value);
    }

   public void SetVolume(float sliderValue)
    {
        masterMixer.SetFloat("MixerVolume",Mathf.Log10(sliderValue) * 20);
        StaticVar.Volume = sliderValue;
    }
}
