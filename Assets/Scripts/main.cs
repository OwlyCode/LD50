using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    private GameObject mapTile;
    public Slider VolumeSlider;
    public GameObject Sound;
    
    //private void Awake() => Init();

    public void Start(){
        VolumeSlider.value = StaticVar.Volume;
        StaticVar.Sound = Sound;
        foreach(Transform child in Sound.transform)
        {
            child.GetComponent<AudioSource>().volume = StaticVar.Volume;
        }
    }
}
