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
    public Text RessourceText;

    public GameObject bearPlaceholder;

    private GameObject placeholderInstance;

    public Sprite bearBuildingSprite;

    public Sprite upgradeSprite;

    void Start()
    {
        StaticVar.RessourceText = RessourceText;
        StaticVar.Ressource = 200;

        placeholderInstance = Instantiate(bearPlaceholder);
    }

    // Update is called once per frame
    private void Update()
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
            else
            {
                placeholderInstance.GetComponentInChildren<SpriteRenderer>().sprite = upgradeSprite;
                var success = false;

                if (foundTile)
                {
                    var go = _tile.TilemapMember.GetInstantiatedObject(_tile.LocalPlace);
                    if (go != null)
                    {
                        var tower = go.GetComponent<BaseTower>();
                        if (tower != null && tower.canUpgrade())
                        {
                            placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                            success = true;
                        }
                    }
                }

                if (!success)
                {
                    placeholderInstance.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0.33f);
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
                /* print("Tile " + _tile.Name + " costs: " + _tile.Cost);
                 _tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
                 _tile.TilemapMember.SetColor(_tile.LocalPlace, Color.red);*/
                if (!StaticVar.gameIsPaused && _tile.Constructible && StaticVar.Ressource >= StaticVar.bearCost && StaticVar.mouseMode == MouseMode.BuildBear)
                {
                    _tile.TilemapMember.SetTile(_tile.LocalPlace, tower);
                    StaticVar.Ressource = -StaticVar.bearCost;
                    StaticVar.bearCost = StaticVar.bearCost * 2;
                    StaticVar.Tower += 1;
                    _tile.Constructible = false;
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
                    }
                }
            }
        }
    }
}
