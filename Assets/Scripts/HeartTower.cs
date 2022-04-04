using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeartTower : MonoBehaviour
{
    Tilemap tilemap;

    float buffPower = 0.2f;

    public int towerLevel = 0;

    public Sprite tier2Sprite;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int pos = tilemap.WorldToCell(transform.position);

        var top = tilemap.GetInstantiatedObject(pos + new Vector3Int(0, 1, 0));
        var bottom = tilemap.GetInstantiatedObject(pos + new Vector3Int(0, -1, 0));
        var left = tilemap.GetInstantiatedObject(pos + new Vector3Int(-1, 0, 0));
        var right = tilemap.GetInstantiatedObject(pos + new Vector3Int(1, 0, 0));

        if (top)
        {
            Buff(top);
        }
        if (bottom)
        {
            Buff(bottom);
        }
        if (left)
        {
            Buff(left);
        }
        if (right)
        {
            Buff(right);
        }
    }

    void Buff(GameObject other)
    {
        if (other.GetComponent<BaseTower>() != null)
        {
            other.GetComponent<BaseTower>().cooldown -= Time.deltaTime * buffPower;
        }
    }

    public bool canUpgrade()
    {
        var cost = StaticVar.upgradeHeartCosts[towerLevel];

        return towerLevel < StaticVar.upgradeHeartCosts.Length - 1 && StaticVar.Ressource >= cost;
    }

    public void UpgradeTower()
    {
        var cost = StaticVar.upgradeHeartCosts[towerLevel];

        if (canUpgrade())
        {
            GetComponent<AudioSource>().Play();
            StaticVar.Ressource = -cost;
            towerLevel++;

            if (towerLevel == 1)
            {
                GetComponent<SpriteRenderer>().sprite = tier2Sprite;
            }

            // TODO BALANCING
            buffPower *= 2f;
        }
    }

    public void OnMouseOver()
    {
        if (StaticVar.mouseMode == MouseMode.Upgrade)
        {
            if (towerLevel < StaticVar.upgradeHeartCosts.Length - 1)
            {
                Tooltip.ShowUpgrade(StaticVar.upgradeHeartCosts[towerLevel], "Increases the power of the heart.");
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
