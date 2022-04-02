using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    Hashtable UIList = new Hashtable();
    InputField InputField;


    void Start() {
        /* Here you all CanvasGroup goes to the main Hashtable */
        UIList.Add("Pause", GameObject.Find("Pause").GetComponent<CanvasGroup>());
        UIList.Add("Main", GameObject.Find("MainCanvas").GetComponent<CanvasGroup>());
        UIList.Add("Obj", GameObject.Find("Upgrades").GetComponent<CanvasGroup>());
        UIList.Add("Lose", GameObject.Find("GameOver").GetComponent<CanvasGroup>());

    }

    void Manager(string value) {
        if (value == "Main") {
            // Do Main Thingies;
        }
        if (value == "Pause") {
            if (StaticVar.gameIsPaused) {                           // Unpausing Game
                StaticVar.gameIsPaused = false;
                PauseUnpause();
                ((CanvasGroup)UIList["Pause"]).alpha = 0;
                ((CanvasGroup)UIList["Main"]).alpha = 1;
                ((CanvasGroup)UIList["Obj"]).alpha = 0;
            } else {                                            // Pausing Game
                StaticVar.gameIsPaused = true;
                PauseUnpause();
                ((CanvasGroup)UIList["Pause"]).alpha = 1;
                ((CanvasGroup)UIList["Main"]).alpha = 0;
                ((CanvasGroup)UIList["Obj"]).alpha = 0;
            }
        }
        if (value == "Obj") {                                   // Display Objectives
            if (StaticVar.gameIsPaused && (((CanvasGroup)UIList["Obj"]).alpha == 0)) { // IF paused and just sumonned
                ((CanvasGroup)UIList["Pause"]).alpha = 0;
                ((CanvasGroup)UIList["Main"]).alpha = 1;
                ((CanvasGroup)UIList["Obj"]).alpha = 1;
            } else if (!StaticVar.gameIsPaused && (((CanvasGroup)UIList["Obj"]).alpha == 0)) { // or just summon but Pause wasn't enforced
                StaticVar.gameIsPaused = true;
                PauseUnpause();
                ((CanvasGroup)UIList["Main"]).alpha = 1;
                ((CanvasGroup)UIList["Obj"]).alpha = 1;
            } else if (StaticVar.gameIsPaused && (((CanvasGroup)UIList["Obj"]).alpha == 1)) { //Otherwise Just unpause and leave
                StaticVar.gameIsPaused = false;
                PauseUnpause();
                ((CanvasGroup)UIList["Obj"]).alpha = 0;
            }
        }
        if (value == "Win" || value == "Lose") {
            foreach (DictionaryEntry entry in UIList) {
                if ((string)entry.Key == value) {
                    ((CanvasGroup)entry.Value).alpha = 1;
                } else ((CanvasGroup)entry.Value).alpha = 0;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!StaticVar.Lose) {
            if (Input.GetKeyDown(KeyCode.Escape)) Manager("Pause");
            if (Input.GetKeyDown(KeyCode.F1)) Manager("Obj");
            if (Input.GetKeyDown(KeyCode.F11)) Manager("Lose");
        }
        if (StaticVar.Lose) Manager("Lose");
    }
    public void ResumeGame() => Manager("Pause");
    public void PauseGame() => Manager("Pause");
    public void ObjPanel() => Manager("Obj");
    public void MainPanel() => Manager("Main");

    void PauseUnpause() => Time.timeScale = (StaticVar.gameIsPaused) ? 0 : 1;
}

