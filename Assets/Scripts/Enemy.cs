using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float MOVE_SPEED = 0.5f;

    bool walking = false;
    List<Vector3> path;
    Vector3 currentTarget;

    float offsetShakiness = 0.125f;

    float zOffset = 0f;

    Vector3 offset;

    public GameObject unicorn;
    public GameObject explosion;

    public int health = 1;

    public int damages = 3;

    void Start()
    {
        offset = new Vector3(Random.Range(-offsetShakiness, offsetShakiness), Random.Range(-offsetShakiness, offsetShakiness), 0);
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

        if (walking)
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
            Destroy(gameObject);
        }
    }

    void onHit()
    {
        if (health > 1)
        {
            health--;

            return;
        }
        // Else "on Death"
        Debug.Log(gameObject.name);
        StaticVar.EnemiesKIA = 1;
        StaticVar.Ressource = 0.3f / Mathf.Abs((float)Mathf.Log(Mathf.Pow(StaticVar.EnemiesKIA, -1)) + 5);
        Debug.Log("Ennemies value : " + 0.5f / Mathf.Abs((float)Mathf.Log(Mathf.Pow(StaticVar.EnemiesKIA, -1)) + 5));
        Debug.Log(StaticVar.Ressource);
        if (StaticVar.KiaList.Contains(gameObject.name))
        {
            StaticVar.KiaList[gameObject.name] = (int)StaticVar.KiaList[gameObject.name] + 1;
        }
        else
        {
            StaticVar.KiaList.Add(gameObject.name, 1);
        }

        Destroy(gameObject);
        if (Random.Range(0, 100) > 80)
        {
            Instantiate(unicorn, transform.position, Quaternion.identity);
            StaticVar.Ressource = 1;

        }
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
