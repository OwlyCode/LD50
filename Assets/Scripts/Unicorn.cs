using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicorn : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += 10f * new Vector3(-1, 0.35f, 0) * Time.deltaTime;
    }
}
