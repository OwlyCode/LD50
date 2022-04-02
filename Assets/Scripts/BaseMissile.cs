using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMissile : MonoBehaviour
{
    private GameObject target;
    private Vector3 lastKnownPosition;

    public void SetTarget(GameObject target)
    {
        transform.position += Vector3.forward * 2f;
        this.target = target;
    }

    void Start()
    {

    }

    void Update()
    {
        if (lastKnownPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastKnownPosition, Time.deltaTime * 2f);
        }

        if (target != null)
        {
            lastKnownPosition = target.transform.position;
            if (target.transform.position == transform.position)
            {
                Destroy(gameObject);
                target.BroadcastMessage("onDeath");
            }
        }
        else
        {
            if (transform.position == lastKnownPosition)
            {
                Destroy(gameObject); // Missed hit
            }
        }
    }
}
