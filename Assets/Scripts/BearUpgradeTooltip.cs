using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearUpgradeTooltip : MonoBehaviour
{
    public void OnMouseOver()
    {
        int level = GetComponentInParent<BaseTower>().towerLevel;

        if (StaticVar.mouseMode == MouseMode.Upgrade && level < StaticVar.upgradeBearCosts.Length - 1)
        {
            Tooltip.ShowUpgrade(StaticVar.upgradeBearCosts[level]);
        }
        else
        {
            Tooltip.StaticHide();
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnMouseExit()
    {
        Tooltip.StaticHide();
    }
}
