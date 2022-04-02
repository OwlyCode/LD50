using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float MOVE_SPEED = 0.5f;

    bool walking = false;
    List<Vector3> path;
    Vector3 currentTarget;

    float offsetShakiness = 0.25f;

    float zOffset = 2f;

    Vector3 offset;

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

    }

    void onDeath()
    {
        StaticVar.EnemiesKIA = 1;
        StaticVar.Ressource = 1; /// Modifier la vazleur par Ennemi.value si on fait differents types d'ennemis.
        Debug.Log("Ennemies Killed : "+StaticVar.EnemiesKIA);
        Destroy(gameObject);
    }
}
