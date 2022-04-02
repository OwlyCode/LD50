using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    [SerializeField] private int mapWidth=4;
    [SerializeField] private int mapHeight=4;

    private GameObject MapGO;

   public void SetParent(GameObject tmp) {MapGO = tmp;}

   public void SetMapTile(GameObject tmp) {mapTile = tmp;}

   public void generateMap()
   {
       for (int y=0;y < mapHeight; y++)
       {
           for (int x= 0; x < mapWidth; x++)
           {
               GameObject newTile = Instantiate(mapTile);
               newTile.transform.SetParent(MapGO.transform);
               newTile.name= "mapTile"+x+" "+y;

//newTile.transform.position = new Vector2(x-(mapWidth-x),y-(mapHeight-y));
               newTile.transform.position = new Vector2(x,y);
           }
       }
   }
}
