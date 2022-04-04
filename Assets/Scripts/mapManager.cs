using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class mapManager : MonoBehaviour
{
    private WorldTile _tile;
    public Grid grid;
    public TileBase tower;

    public TileBase heartTower;

    public Text RessourceText;

    public GameObject bearPlaceholder;

    private GameObject placeholderInstance;

    public Sprite bearBuildingSprite;
    public Sprite heartBuildingSprite;

    public Sprite upgradeSprite;

    void Start()
    {
        StaticVar.RessourceText = RessourceText;
        StaticVar.Ressource = 3;

        placeholderInstance = Instantiate(bearPlaceholder);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!StaticVar.Lose)
        {
            if (StaticVar.mouseMode != MouseMode.None)
            {
                Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var worldPoint = grid.WorldToCell(new Vector3(p.x, p.y, 0));

                placeholderInstance.transform.position = grid.CellToWorld(worldPoint);

                var tiles = tileData.instance.tiles;
                var foundTile = tiles.TryGetValue(worldPoint, out _tile);

                if (StaticVar.mouseMode == MouseMode.BuildBear)
                {
                    placeholderInstance.GetComponentInChildren<SpriteRenderer>().sprite = bearBuildingSprite;

                    if (StaticVar.Ressource >= StaticVar.bearCost && foundTile && _tile.Constructible)
                    {
                        placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 1, 0, 0.33f);
                    }
                    else
                    {
                        placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0, 0.33f);
                    }
                }
                else if (StaticVar.mouseMode == MouseMode.BuildHeart)
                {
                    placeholderInstance.GetComponentInChildren<SpriteRenderer>().sprite = heartBuildingSprite;

                    if (StaticVar.Ressource >= StaticVar.bearCost && foundTile && _tile.Constructible)
                    {
                        placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 1, 0, 0.33f);
                    }
                    else
                    {
                        placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0, 0.33f);
                    }
                }
                else
                {
                    placeholderInstance.GetComponentInChildren<SpriteRenderer>().sprite = upgradeSprite;
                    var success = false;
                    var tooltip = false;

                    if (foundTile)
                    {
                        var go = _tile.TilemapMember.GetInstantiatedObject(_tile.LocalPlace);
                        if (go != null)
                        {
                            var tower = go.GetComponent<BaseTower>();
                            if (tower != null)
                            {
                                Tooltip.ShowUpgrade(StaticVar.upgradeBearCosts[tower.towerLevel], "Increases the firerate and adds one bullet.");
                                tooltip = true;
                            }
                            if (tower != null && tower.canUpgrade())
                            {
                                placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                                success = true;
                            }

                            var tower2 = go.GetComponent<HeartTower>();
                            if (tower2 != null)
                            {
                                Tooltip.ShowUpgrade(StaticVar.upgradeHeartCosts[tower2.towerLevel], "Increases the power of the heart.");
                                tooltip = true;

                            }
                            if (tower2 != null && tower2.canUpgrade())
                            {
                                placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                                success = true;
                            }
                        }
                    }

                    if (!tooltip)
                    {
                        Tooltip.ShowUpgrade(0);
                    }

                    if (!success)
                    {
                        placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
                    }
                }
            }
            else
            {
                placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var pointv = new Vector3(point.x, point.y, 0);
                var worldPoint = grid.WorldToCell(new Vector3(point.x, point.y, 0));

                var tiles = tileData.instance.tiles; // This is our Dictionary of tiles

                if (tiles.TryGetValue(worldPoint, out _tile))
                {
                    if (!StaticVar.gameIsPaused && _tile.Constructible && StaticVar.Ressource >= StaticVar.bearCost && StaticVar.mouseMode == MouseMode.BuildBear)
                    {
                        _tile.TilemapMember.SetTile(_tile.LocalPlace, tower);
                        StaticVar.Ressource = -StaticVar.bearCost;
                        StaticVar.bearCost = Mathf.Min(StaticVar.bearCost * 2, 75);
                        StaticVar.Tower += 1;
                        _tile.Constructible = false;
                        Tooltip.RefreshBear();
                    }

                    if (!StaticVar.gameIsPaused && _tile.Constructible && StaticVar.Ressource >= StaticVar.heartCost && StaticVar.mouseMode == MouseMode.BuildHeart)
                    {
                        _tile.TilemapMember.SetTile(_tile.LocalPlace, heartTower);
                        StaticVar.Ressource = -StaticVar.heartCost;
                        StaticVar.heartCost = Mathf.Min(StaticVar.heartCost * 2, 100);
                        StaticVar.Tower += 1;
                        _tile.Constructible = false;
                        Tooltip.RefreshHeart();
                    }

                    if (!StaticVar.gameIsPaused && StaticVar.mouseMode == MouseMode.Upgrade)
                    {
                        var go = _tile.TilemapMember.GetInstantiatedObject(_tile.LocalPlace);

                        if (go)
                        {
                            var tower = go.GetComponent<BaseTower>();
                            if (tower != null)
                            {
                                tower.UpgradeTower();
                            }

                            var tower2 = go.GetComponent<HeartTower>();
                            if (tower2 != null)
                            {
                                tower2.UpgradeTower();
                            }
                        }
                    }
                }
            }
        }
    }
}
