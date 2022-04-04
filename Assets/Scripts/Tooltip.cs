using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text text;
    public Text cost;

    public GameObject unicorn;

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
        Show("Build Teddy Bear", StaticVar.bearCost);
    }

    public static void RefreshBear()
    {

        if (_instance)
        {
            _instance.ShowBear();
        }
    }

    public void ShowBlankUpgrade()
    {
        Show("Upgrade", 0);
    }

    public static void ShowUpgrade(int cost)
    {
        if (_instance)
        {
            _instance.Show("Upgrade", cost);
        }
    }

    public static void StaticHide()
    {
        if (_instance)
        {
            _instance.Hide();
        }
    }

    public void Show(string textValue, int costValue)
    {
        text.text = textValue;
        cost.text = costValue > 0 ? costValue.ToString() : "";

        unicorn.SetActive(costValue > 0);


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
