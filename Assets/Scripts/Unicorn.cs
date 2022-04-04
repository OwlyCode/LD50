using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicorn : MonoBehaviour
{
    void Update()
    {
        Vector3 target = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));


        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 10f);
    }
}
