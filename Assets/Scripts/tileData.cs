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

    private void Awake() 
	{
		if (instance == null) 
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		GetWorldTiles();
	}

    	// Use this for initialization
	private void GetWorldTiles () 
	{
        tiles = new Dictionary<Vector3, WorldTile>();
		foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
		{
            Debug.Log(pos.x+" "+pos.y+" "+pos.z);
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
            Debug.Log("Dict:" +tile.LocalPlace + " " + tile.Name.ToString());
		}
		searchEntrancetoTop();
	}

	private void searchEntrancetoTop()
	{
		/*for(var x = Tilemap.bounds.min.x; x< Tilemap.bounds.max.x;x++){
			for(var y= Tilemap.bounds.min.y; y< Tilemap.bounds.max.y;y++){
				for(var z= Tilemap.bounds.min.z;z< Tilemap.bounds.max.z;z++){
 				tilemap.GetTile( new vector3Int(x,y,z));
				}
			}
		}
			foreach (WorldTile _tile in tiles.Keys)
		{
		Debug.Log("Entry is here");
		}*/
	}
}

