using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearUpgradeTooltip : MonoBehaviour
{
    public void OnMouseOver()
    {
        if (StaticVar.mouseMode == MouseMode.Upgrade)
        {
            Tooltip.ShowUpgrade(StaticVar.upgradeBearCosts[0]);
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnMouseExit()
    {
        Tooltip.StaticHide();
    }
}
