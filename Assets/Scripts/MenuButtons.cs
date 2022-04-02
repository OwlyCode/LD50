using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    public void goToMain()      => SceneManager.LoadScene("Main", LoadSceneMode.Single);
    public void goToMenu()      => SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    public void goToIntro()     => SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    public void goToDesktop()   => Application.Quit();
    
    public void SetVolume(Slider slider) => StaticVar.SetVolume(slider.value);
   // public void Resume(UIManager UIMan) => UIMan.ResumeGame();

}
