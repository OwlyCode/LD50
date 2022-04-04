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
            if (level < StaticVar.upgradeBearCosts.Length - 1)
            {
                Tooltip.ShowUpgrade(StaticVar.upgradeBearCosts[level], "Increases the firerate and adds one bullet.");
            }
            else
            {
                Tooltip.ShowUpgrade(0);
            }
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnMouseExit()
    {
        if (StaticVar.mouseMode == MouseMode.Upgrade)
        {
            Tooltip.ShowUpgrade(0);
        }
    }
}
