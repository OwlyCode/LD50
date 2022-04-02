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
    public static int EnemiesKIA { 
        get {
            return _enemieskia;
        }
        set {
            _enemieskia += value;
        }
    }
    private static int _ressource;
    public static int Ressource { 
        get {
            return _ressource;
        }
        set {
            _ressource += value;
            StaticVar.RessourceText.text = "Ressources : "+ _ressource;
        }
    }
    
    public static float Getvolume() {return Volume;}

    public static float SetVolume(float Volumetoset) {Volume = Volumetoset; return Volume;}

    
}
