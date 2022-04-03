using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Dictionary<GameObject, int> Ennemies;
    public List<GameObject> EnnemiesGO;

    private float spawnRate = 0.25f;

    private float spawnDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnDelay < 0f && tileData.path.Count > 0)
        {
            GameObject tmp;
            spawnDelay = spawnRate;
            if (Random.Range(0, 100) > 90)
            {
                tmp = Instantiate(EnnemiesGO[1], tileData.path[0], Quaternion.identity);
                tmp.name = "Nightmare";
            }
            else
            {
                tmp = Instantiate(EnnemiesGO[0], tileData.path[0], Quaternion.identity);
                tmp.name = "Bad dream";
            }
        }

        spawnDelay -= Time.deltaTime;
    }
}
