using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text text;
    public Text cost;

    static Tooltip _instance;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        //Hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowBear()
    {
        Show("Bear", StaticVar.bearCost);
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
        cost.text = costValue.ToString();

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
