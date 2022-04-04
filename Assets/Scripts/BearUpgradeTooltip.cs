using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearUpgradeTooltip : MonoBehaviour
{
    public void OnMouseOver()
    {
        int level = GetComponentInParent<BaseTower>().towerLevel;

        if (StaticVar.mouseMode == MouseMode.Upgrade)
        {
            Tooltip.ShowUpgrade(StaticVar.upgradeBearCosts[level]);
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnMouseExit()
    {
        Tooltip.StaticHide();
    }
}
