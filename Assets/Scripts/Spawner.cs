using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    private float spawnRate = 0.25f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnRate < 0f && tileData.path.Count > 0)
        {
            spawnRate = 0.25f;
            Instantiate(enemy, tileData.path[0], Quaternion.identity);
        }

        spawnRate -= Time.deltaTime;
    }
}
