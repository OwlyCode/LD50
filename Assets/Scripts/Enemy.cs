using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float MOVE_SPEED = 0.5f;

    bool walking = false;
    List<Vector3> path;
    Vector3 currentTarget;

    bool freeze = false;

    float offsetShakiness = 0.250f;

    float zOffset = 0f;

    Vector3 offset;

    public GameObject unicorn;
    public GameObject explosion;

    public int health = 1;

    public int damages = 3;

    public int unicornProbability = 90;

    public int unicornAmount = 1;

    public int unicornValue = 1;

    void Start()
    {
        offset = new Vector3(Random.Range(0, offsetShakiness), Random.Range(0, offsetShakiness), 0);
    }

    IEnumerator BlinkRed()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        freeze = true;
        yield return new WaitForSeconds(0.33f);
        freeze = false;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    void Update()
    {
        if (!walking && tileData.path.Count > 0)
        {
            walking = true;
            path = new List<Vector3>(tileData.path); // copy
            currentTarget = path[0] + Vector3.forward * zOffset + offset;
            path.RemoveAt(0);
        }

        if (walking && !freeze)
        {
            if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
            {
                if (path.Count > 0)
                {
                    currentTarget = path[0] + Vector3.forward * zOffset + offset;
                    path.RemoveAt(0);
                }
                else
                {
                    walking = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * MOVE_SPEED);
            }
        }

        if (walking && path.Count == 0)
        {
            GameObject.Find("/Map").BroadcastMessage("DestroyDreamland", damages);
            ScreenShaker.Shake(0.1f, 0.025f);
            Destroy(gameObject);
        }
    }

    void onHit()
    {
        if (health > 1)
        {
            health--;
            StartCoroutine(BlinkRed());

            return;
        }
        // Else "on Death"
        StaticVar.EnemiesKIA = 1;
        // StaticVar.Ressource = 0.3f / Mathf.Abs((float)Mathf.Log(Mathf.Pow(StaticVar.EnemiesKIA, -1)) + 5);
        if (StaticVar.KiaList.Contains(gameObject.name))
        {
            StaticVar.KiaList[gameObject.name] = (int)StaticVar.KiaList[gameObject.name] + 1;
        }
        else
        {
            StaticVar.KiaList.Add(gameObject.name, 1);
        }

        if (Random.Range(0, 100) > unicornProbability)
        {
            StartCoroutine(SpawnUnicorns());
            //Instantiate(unicorn, transform.position, Quaternion.identity);
        }
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    IEnumerator SpawnUnicorns()
    {
        Debug.Log("Spawning unicorns");
        for (int i = 0; i < unicornAmount; i++)
        {
            Debug.Log("+++" + unicornAmount);
            var uni = Instantiate(unicorn, transform.position, Quaternion.identity);
            uni.GetComponent<Unicorn>().value = unicornValue;
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
}
