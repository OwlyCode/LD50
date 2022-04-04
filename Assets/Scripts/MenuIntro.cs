using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuIntro : MonoBehaviour
{
    public Slider VolumeSlider;   
    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider.maxValue = 100f;
        VolumeSlider.minValue = 0f;
        VolumeSlider.value = 50f;
        StaticVar.SetVolume(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
