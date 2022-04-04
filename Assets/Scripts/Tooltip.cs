using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text text;
    public Text cost;

    public GameObject unicorn;

    public Text help;

    static Tooltip _instance;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowBear()
    {
        Show("Build Teddy Bear", StaticVar.bearCost, "Fires bullets at enemies.");
    }

    public void ShowHeart()
    {
        Show("Build Heart", StaticVar.heartCost, "Increases firerate of adjacent towers.");
    }

    public static void RefreshBear()
    {

        if (_instance)
        {
            _instance.ShowBear();
        }
    }

    public static void RefreshHeart()
    {

        if (_instance)
        {
            _instance.ShowHeart();
        }
    }


    public void ShowBlankUpgrade()
    {
        Show("Upgrade", 0, "Select a tower to upgrade.");
    }

    public static void ShowUpgrade(int cost, string help = "Select a tower to upgrade.")
    {
        if (_instance)
        {
            _instance.Show("Upgrade", cost, help);
        }
    }

    public static void StaticHide()
    {
        if (_instance)
        {
            _instance.Hide();
        }
    }

    public void Show(string textValue, int costValue, string help = "")
    {
        text.text = textValue;
        cost.text = costValue > 0 ? costValue.ToString() : "";

        unicorn.SetActive(costValue > 0);


        this.help.text = help;

        if (costValue > StaticVar.Ressource)
        {
            cost.color = Color.red;
        }
        else
        {
            cost.color = Color.black;
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
