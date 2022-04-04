using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMissile : MonoBehaviour
{
    private GameObject target;
    private Vector3 lastKnownPosition;

    public void SetTarget(GameObject target)
    {
        transform.position += Vector3.forward * 0f;
        this.target = target;
    }

    void Start()
    {

    }

    void Update()
    {
        if (lastKnownPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastKnownPosition, Time.deltaTime * 6f);
        }

        if (target != null)
        {
            GetComponent<DepthFixer>().offset = transform.position.y - target.transform.position.y;

            lastKnownPosition = target.transform.position;
            if (Vector3.Distance(transform.position, lastKnownPosition) < 0.05f)
            {
                Destroy(gameObject);
                target.BroadcastMessage("onHit");
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, lastKnownPosition) < 0.05f)
            {
                Destroy(gameObject); // Missed hit
            }
        }
    }
}
