using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

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
            spawnDelay = spawnRate;
            Instantiate(enemy, tileData.path[0], Quaternion.identity);
        }

        spawnDelay -= Time.deltaTime;
    }
}
