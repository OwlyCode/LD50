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
        VolumeSlider.value = 0.3f;
        StaticVar.SetVolume(VolumeSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
