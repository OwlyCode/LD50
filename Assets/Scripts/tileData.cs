using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileData : MonoBehaviour
{
    const string PATHWAY_TILE = "pathway_v2";

    public static tileData instance;
    public Tilemap Tilemap;
    public Grid grid;

    public GameObject tileExplosion;

    public List<Tile> nightmareTiles;

    public Dictionary<Vector3, WorldTile> tiles;
    private WorldTile _tile;

    private List<Vector3Int> dreamlandCells;

    private int initialHealthyCells = 0;

    public static List<Vector3> path;

    private AudioSource music;
    private AudioSource badMusic;
    private AudioSource breakSound;

    bool brokeSound = false;

    private void Awake()
    {
        music = GameObject.Find("/Music").GetComponent<AudioSource>();
        badMusic = GameObject.Find("/BadMusic").GetComponent<AudioSource>();
        breakSound = GameObject.Find("/BreakSound").GetComponent<AudioSource>();

        dreamlandCells = new List<Vector3Int>();
        path = new List<Vector3>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //StaticVar.Ressource=1;

        GetWorldTiles();

        path = computePath();
        StaticVar.TimeStart = Time.time;
    }

    public void DestroyDreamland(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (dreamlandCells.Count > 0)
            {
                Debug.Log("BOOM");

                var coords = Random.Range(0, dreamlandCells.Count);

                // Tilemap.SetTile(dreamlandCells[coords], nightmareTiles[Random.Range(0, nightmareTiles.Count)]);
                if (tiles.TryGetValue(dreamlandCells[coords], out _tile))
                {
                    _tile.Constructible = false;
                    _tile.TilemapMember.SetTile(_tile.LocalPlace, nightmareTiles[Random.Range(0, nightmareTiles.Count)]);
                    Instantiate(tileExplosion, Tilemap.CellToWorld(dreamlandCells[coords]), Quaternion.identity);
                }
                dreamlandCells.RemoveAt(coords);
            }
            else
            {
                StaticVar.Lose = true;
            }
        }


        if (!brokeSound && (float)dreamlandCells.Count / (float)initialHealthyCells < 0.95f)
        {
            music.volume = 0;
            breakSound.Play();
            badMusic.volume = 1;
            badMusic.PlayDelayed(2.8f);
            brokeSound = true;
        }

        // Maybe we add that back later with Max
        // GameObject.Find("/Music").GetComponent<AudioSource>().pitch = Mathf.Lerp(0.8f, 1f, (float)dreamlandCells.Count / (float)initialHealthyCells);
    }

    // Use this for initialization
    private void GetWorldTiles()
    {
        tiles = new Dictionary<Vector3, WorldTile>();
        foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
        {
            bool tmp = true;
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if (!Tilemap.HasTile(localPlace)) continue;
            if (Tilemap.GetTile(localPlace).name == PATHWAY_TILE) { tmp = false; }

            if (tmp)
            {
                dreamlandCells.Add(localPlace);
            }

            var tile = new WorldTile
            {
                LocalPlace = localPlace,
                WorldLocation = grid.GetCellCenterWorld(localPlace),
                TileBase = Tilemap.GetTile(localPlace),
                TilemapMember = Tilemap,
                Constructible = tmp,
                Name = localPlace.x + "," + localPlace.y,
                Cost = -1 // TODO: Change this with the proper cost from ruletile
            };

            tiles.Add(tile.LocalPlace, tile);
        }

        initialHealthyCells = dreamlandCells.Count;
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

                    if (x != 0 && y != 0) continue;

                    var scanned = current + new Vector3Int(x, y, 0);

                    if (closed.Contains(scanned)) continue;

                    var tile = Tilemap.GetTile(scanned);
                    if (tile && tile.name == PATHWAY_TILE)
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
                if (tile.name == PATHWAY_TILE)
                {
                    return new Vector3Int(x, entranceY, 0);
                }
            }
        }

        return new Vector3Int(0, 0, 0);
    }
}

