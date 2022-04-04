using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MouseMode {
    None,
    BuildBear,
    Upgrade
}

public class StaticVar : MonoBehaviour {
    public static MouseMode mouseMode = MouseMode.None;
    public static bool Lose = false;
    public static bool gameIsPaused = false;
    public static Text RessourceText;
    public static float Volume { get; set; }
    private static int _enemieskia;
    public static int Tower { get; set; }
    public static float TimeStart { get; set; }
    public static float TimeStop { get; set; }
    public static GameObject Sound;
    public static Hashtable KiaList = new Hashtable();

    public static int bearCost = 1;
    public static int[] upgradeBearCosts = new int[] { 1, 4, 8 };

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
            StaticVar.RessourceText.text = "Ressources : " + Mathf.FloorToInt(_ressource);
        }
    }

    public static float Getvolume() { return Volume; }

    public static float SetVolume(float Volumetoset) {
        Volume = Volumetoset;
        if (Sound) {
            foreach (Transform child in Sound.transform) {
                child.GetComponent<AudioSource>().volume = StaticVar.Volume;
            }
        }
        return Volume;
    }

    public void Reset() {
        bearCost = 1;
        _enemieskia = 0;
        _ressource = 0;
        TimeStart = Time.time;
        Time.timeScale = 1;
        Tower = 0;
        Lose = false;
        gameIsPaused = false;
        KiaList.Clear();
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
