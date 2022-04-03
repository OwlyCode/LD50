using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void SelfRemove()
    {
        StartCoroutine(AsyncDestroy());
        GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator AsyncDestroy()
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);

        yield return null;
    }
}
