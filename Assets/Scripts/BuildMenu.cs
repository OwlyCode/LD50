using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public void setBearMode()
    {
        StaticVar.mouseMode = MouseMode.BuildBear;
    }

    public void setUpgradeMode()
    {
        StaticVar.mouseMode = MouseMode.Upgrade;
    }
    public void setBuildHeart()
    {
        StaticVar.mouseMode = MouseMode.BuildHeart;
    }
}
