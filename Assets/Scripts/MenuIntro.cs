using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuIntro : MonoBehaviour
{
    public Slider VolumeSlider;   
    public GameObject Sound;
    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider.value = 0.14f;
        StaticVar.Sound = Sound;
        StaticVar.SetVolume(VolumeSlider.value);

        //StaticVar.SetVolume(StaticVar.Volume);
        
        foreach(Transform child in Sound.transform)
        {
            child.GetComponent<AudioSource>().volume = StaticVar.Volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
