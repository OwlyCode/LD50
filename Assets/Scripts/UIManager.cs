using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Hashtable UIList = new Hashtable();
    InputField InputField;


    void Start()
    {
        /* Here you all CanvasGroup goes to the main Hashtable */
        UIList.Add("Pause", GameObject.Find("Pause").GetComponent<CanvasGroup>());
        UIList.Add("Main", GameObject.Find("MainCanvas").GetComponent<CanvasGroup>());
        UIList.Add("Lose", GameObject.Find("GameOver").GetComponent<CanvasGroup>());
    }

    void setScore()
    {
        if (StaticVar.TimeStop == 0) { StaticVar.TimeStop = Time.time; }
        Debug.Log("In Set Score");
        //Debug.Log(StaticVar.KiaList);
        GameObject.Find("Score").GetComponent<Text>().text = "ScoreBoard :\n\n";

        TimeSpan timeSpan = TimeSpan.FromSeconds(StaticVar.TimeStop - StaticVar.TimeStart);

        GameObject.Find("Score").GetComponent<Text>().text += "Time Dreamed :" + string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds) + "\n\n";
        //Debug.Log(StaticVar.KiaList);
        if (StaticVar.KiaList.Count != 0)
        {
            foreach (DictionaryEntry entry in StaticVar.KiaList)
            {
                GameObject.Find("Score").GetComponent<Text>().text += (string)entry.Key + "   :   " + entry.Value + "\n";
            }
        }
        else
        {
            GameObject.Find("Score").GetComponent<Text>().text += "How did you manage to hit no monsters ?\n";
            GameObject.Find("Score").GetComponent<Text>().text += "Try to click on grass to construct towers next time !\n";
        }
    }

    void Manager(string value)
    {
        if (value == "Main")
        {
            // Do Main Thingies;
        }
        if (value == "Pause")
        {
            if (StaticVar.gameIsPaused)
            {                           // Unpausing Game
                StaticVar.gameIsPaused = false;
                PauseUnpause();
                ((CanvasGroup)UIList["Pause"]).alpha = 0;
                ((CanvasGroup)UIList["Main"]).alpha = 1;

            } else {                                            // Pausing Game
                GameObject.Find("VolumeSlider").GetComponent<Slider>().value = StaticVar.Getvolume();

                StaticVar.gameIsPaused = true;
                PauseUnpause();
                ((CanvasGroup)UIList["Pause"]).alpha = 1;
                ((CanvasGroup)UIList["Main"]).alpha = 0;
            }
        }
        if (value == "Lose")
        {
            setScore();
            StaticVar.Lose = StaticVar.gameIsPaused = true;
            PauseUnpause();
            //StaticVar.Reset();
            foreach (DictionaryEntry entry in UIList)
            {
                if ((string)entry.Key == value)
                {
                    ((CanvasGroup)entry.Value).alpha = 1;
                }
                else ((CanvasGroup)entry.Value).alpha = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!StaticVar.Lose)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Manager("Pause");
        }
        if (StaticVar.Lose) Manager("Lose");
    }
    public void ResumeGame() => Manager("Pause");
    public void PauseGame() => Manager("Pause");
    public void MainPanel() => Manager("Main");

    void PauseUnpause() => Time.timeScale = (StaticVar.gameIsPaused) ? 0 : 1;
}

