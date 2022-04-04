using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornSpawner : MonoBehaviour
{
    public int unicornAmount = 1;
    public int unicornValue = 1;

    public GameObject unicorn;

    void Start()
    {
        StartCoroutine(SpawnUnicorns());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnUnicorns()
    {
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
