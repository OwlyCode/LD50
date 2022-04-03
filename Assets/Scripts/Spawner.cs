using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnGroup
{
    public GameObject prefab;
    public int count;
    public float delay;
}

public class SpawnSequence
{
    public List<SpawnGroup> spawnGroups;
    public float delay;
}

enum SpawnerState
{
    Idle,
    Spawning,
    Waiting
}

public class Spawner : MonoBehaviour
{
    public Dictionary<GameObject, int> Ennemies;
    public List<GameObject> EnnemiesGO;

    private float spawnRate = 2f;

    private float spawnRateIncrease = 0.01f;

    private float spawnDelay = 2f;

    private List<SpawnSequence> spawnSequences;

    private SpawnerState state = SpawnerState.Idle;

    private int currentSpawnSequence = 0;

    private int currentSpawnGroup = 0;

    private int currentSpawned = 0;

    private int difficulty = 1;

    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = spawnRate;
        spawnSequences = new List<SpawnSequence>();

        spawnSequences.Add(new SpawnSequence()
        {
            spawnGroups = new List<SpawnGroup>()
            {
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[0],
                    count = 3,
                    delay = 0.25f
                }
            },
            delay = 5f
        });

        spawnSequences.Add(new SpawnSequence()
        {
            spawnGroups = new List<SpawnGroup>()
            {
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[0],
                    count = 3,
                    delay = 0.25f
                },
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[1],
                    count = 1,
                    delay = 0.25f
                }
            },
            delay = 5f
        });
    }

    SpawnGroup getCurrentGroup()
    {
        return spawnSequences[currentSpawnSequence].spawnGroups[currentSpawnGroup];
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnerState.Idle)
        {
            currentSpawnSequence = 0;
            state = SpawnerState.Spawning;
        }

        if (state == SpawnerState.Spawning)
        {
            var group = getCurrentGroup();

            if (currentSpawned < group.count * difficulty)
            {
                if (spawnDelay <= 0)
                {
                    var go = Instantiate(group.prefab, tileData.path[0], Quaternion.identity);
                    currentSpawned++;
                    spawnDelay = group.delay;
                    Debug.Log("SPAWN !");
                }
            }
            else
            {
                currentSpawned = 0;
                if (currentSpawnGroup < spawnSequences[currentSpawnSequence].spawnGroups.Count - 1)
                {
                    Debug.Log("NEXT GR");
                    currentSpawnGroup++;
                }
                else
                {
                    Debug.Log("NEXT SEQ");
                    spawnDelay = spawnSequences[currentSpawnSequence].delay;
                    currentSpawnGroup = 0;

                    if (currentSpawnSequence >= spawnSequences.Count - 1)
                    {
                        difficulty++;
                    }

                    currentSpawnSequence = (currentSpawnSequence + 1) % spawnSequences.Count;
                }
            }
        }

        spawnDelay -= Time.deltaTime;
    }
}
