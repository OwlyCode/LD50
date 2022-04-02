using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaticVar
{
    public static Camera MainCam;// {get; private set;}
    public static GameObject CamO {get; private set;}
    public static GameObject LightO {get; private set;}
    public static Text RessourceText;
    public static float Volume {get; private set;}
    private static int _enemieskia;
    public static int Tower {get; set;}
    public static int EnemiesKIA { 
        get {
            return _enemieskia;
        }
        set {
            _enemieskia += value;
        }
    }
    private static float _ressource;
    public static float Ressource { 
        get {
            return _ressource;
        }
        set {
            _ressource += value;
            StaticVar.RessourceText.text = "Ressources : "+ Mathf.FloorToInt(_ressource);
        }
    }
    
    public static float Getvolume() {return Volume;}

    public static float SetVolume(float Volumetoset) {Volume = Volumetoset; return Volume;}

    
}
