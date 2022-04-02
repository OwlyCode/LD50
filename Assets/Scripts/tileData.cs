using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileData : MonoBehaviour
{
    public static tileData instance;
    public Tilemap Tilemap;
    public Grid grid;

    public Dictionary<Vector3, WorldTile> tiles;

    public static List<Vector3> path;

    private void Awake()
    {
        path = new List<Vector3>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        GetWorldTiles();

        path = computePath();
    }

    // Use this for initialization
    private void GetWorldTiles()
    {
        tiles = new Dictionary<Vector3, WorldTile>();
        foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if (!Tilemap.HasTile(localPlace)) continue;
            var tile = new WorldTile
            {
                LocalPlace = localPlace,
                WorldLocation = grid.GetCellCenterWorld(localPlace),
                TileBase = Tilemap.GetTile(localPlace),
                TilemapMember = Tilemap,
                Name = localPlace.x + "," + localPlace.y,
                Cost = -1 // TODO: Change this with the proper cost from ruletile
            };

            tiles.Add(tile.LocalPlace, tile);
        }
    }

    private List<Vector3> computePath()
    {
        var path = new List<Vector3Int>();
        var worldPath = new List<Vector3>();
        var lookup = new[] { -1, 0, 1 };

        Vector3Int current = searchEntrancetoTop();
        List<Vector3Int> closed = new List<Vector3Int>();

        var safety = 1000;

        while (true && safety > 0)
        {
            safety--;
            bool found = false;

            foreach (int x in lookup)
            {
                if (found) break;
                foreach (int y in lookup)
                {
                    if (x == 0 && y == 0) continue;

                    var scanned = current + new Vector3Int(x, y, 0);

                    if (closed.Contains(scanned)) continue;

                    var tile = Tilemap.GetTile(scanned);
                    if (tile && tile.name == "pathway_tile")
                    {
                        path.Add(current + new Vector3Int(x, y, 0));
                        closed.Add(current);
                        current = scanned;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                foreach (var v in path)
                {
                    worldPath.Add(grid.CellToWorld(v));

                }
                return worldPath;
            }
        }

        return new List<Vector3>();
    }

    private Vector3Int searchEntrancetoTop()
    {
        Tilemap.CompressBounds();

        var entranceY = Tilemap.cellBounds.yMax - 1;

        for (var x = Tilemap.cellBounds.xMin; x < Tilemap.cellBounds.xMax; x++)
        {
            var tile = Tilemap.GetTile(new Vector3Int(x, entranceY, 0));

            if (tile != null)
            {
                if (tile.name == "pathway_tile")
                {
                    return new Vector3Int(x, entranceY, 0);
                }
            }
        }

        return new Vector3Int(0, 0, 0);
    }
}

