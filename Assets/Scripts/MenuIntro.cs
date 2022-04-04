using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuIntro : MonoBehaviour
{
    public Slider VolumeSlider;   
    public AudioMixer masterMixer;
    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider.minValue = 0.0001f;
        VolumeSlider.maxValue = 1f;
        VolumeSlider.value = 0.14f;
        StaticVar.SetVolume(VolumeSlider.value);

        SetVolume(VolumeSlider.value);
    }
   public void SetVolume(float sliderValue)
    {
        masterMixer.SetFloat("MixerVolume",Mathf.Log10(sliderValue) * 20);
        StaticVar.Volume = sliderValue;
    }
}
