using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool walking = false;
    List<Vector3> path;
    Vector3 currentTarget;


    void Update()
    {
        if (!walking && tileData.path.Count > 0)
        {
            walking = true;
            path = new List<Vector3>(tileData.path); // copy
            currentTarget = path[0] + Vector3.forward * 2f;
            path.RemoveAt(0);
        }

        if (walking)
        {
            if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
            {
                if (path.Count > 0)
                {
                    currentTarget = path[0] + Vector3.forward * 2f + (Vector3.up * 0.5f) / 2f;
                    path.RemoveAt(0);
                }
                else
                {
                    walking = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * 1f);
            }
        }

    }

    void onDeath()
    {
        Destroy(gameObject);
    }
}
