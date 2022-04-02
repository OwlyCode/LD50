using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    private GameObject mapTile;
    public mapGenerator mapGen;
    private void Awake() => Init();

    public void Init(){
        Debug.Log(mapGen);
        
        mapGen.SetParent(GameObject.Find("Map").gameObject);
        mapGen.SetMapTile(Resources.Load("Prefabs/mapTile")  as GameObject);
        mapGen.generateMap();
        Debug.Log("Init !");
    }
}
