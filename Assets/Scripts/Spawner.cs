using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnGroup
{
    public GameObject prefab;
    public int count;
    public string name;
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
                    delay = 0.25f,
                    name = "Bad dream"
                }
            },
            delay = 15f
        });

        spawnSequences.Add(new SpawnSequence()
        {
            spawnGroups = new List<SpawnGroup>()
            {
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[0],
                    count = 6,
                    delay = 0.25f,
                    name = "Bad dream"
                }
            },
            delay = 15f
        });

        spawnSequences.Add(new SpawnSequence()
        {
            spawnGroups = new List<SpawnGroup>()
            {
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[1],
                    count = 1,
                    delay = 0.25f,
                    name = "Nightmare"
                }
            },
            delay = 15f
        });

        spawnSequences.Add(new SpawnSequence()
        {
            spawnGroups = new List<SpawnGroup>()
            {
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[0],
                    count = 3,
                    delay = 0.25f,
                    name = "Bad dream"
                },
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[1],
                    count = 1,
                    delay = 0.25f,
                    name = "Nightmare"
                }
            },
            delay = 15f
        });

        spawnSequences.Add(new SpawnSequence()
        {
            spawnGroups = new List<SpawnGroup>()
            {
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[0],
                    count = 3,
                    delay = 0.25f,
                    name = "Bad dream"
                },
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[1],
                    count = 1,
                    delay = 0.25f,
                    name = "Nightmare"
                },
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[0],
                    count = 3,
                    delay = 0.25f,
                    name = "Bad dream"
                },
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[1],
                    count = 1,
                    delay = 0.25f,
                    name = "Nightmare"
                }
            },
            delay = 15f
        });

        spawnSequences.Add(new SpawnSequence()
        {
            spawnGroups = new List<SpawnGroup>()
            {
                new SpawnGroup()
                {
                    prefab = EnnemiesGO[2],
                    count = 1,
                    delay = 3f,
                    name = "The Scariest Nightmare"
                }
            },
            delay = 15f
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

        if (state == SpawnerState.Spawning && !StaticVar.gameIsPaused)
        {
            var group = getCurrentGroup();

            if (currentSpawned < group.count * Mathf.Pow(difficulty, 2))
            {
                if (spawnDelay <= 0)
                {
                    var go = Instantiate(group.prefab, tileData.path[0], Quaternion.identity);
                    go.name = group.prefab.name;
                    var enemy = go.GetComponent<Enemy>();

                    enemy.health *= difficulty;
                    enemy.unicornProbability = Mathf.Min(80, (difficulty - 1) * 10);
                    enemy.movespeed += Mathf.Min(0.025f * (difficulty - 2), 0.25f);

                    currentSpawned++;
                    spawnDelay = group.delay;
                }
            }
            else
            {
                currentSpawned = 0;
                if (currentSpawnGroup < spawnSequences[currentSpawnSequence].spawnGroups.Count - 1)
                {
                    currentSpawnGroup++;
                }
                else
                {
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
