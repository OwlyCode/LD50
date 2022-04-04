using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{

    Vector3 originalPosition;

    bool exclusiveShake = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    public static void Shake(float shakeDuration = 0.5f, float shakeMagnitude = 0.1f, bool exclusiveShake = false)
    {
        Camera.main.GetComponent<ScreenShaker>().DoShake(shakeDuration, shakeMagnitude, exclusiveShake);
    }


    void DoShake(float shakeDuration = 3f, float shakeMagnitude = 0.1f, bool exclusiveShake = false)
    {
        if (this.exclusiveShake)
        {
            return;
        }

        this.exclusiveShake = exclusiveShake;
        transform.position = originalPosition;
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(shakeDuration, shakeMagnitude));
    }


    IEnumerator ShakeCoroutine(float shakeDuration = 3f, float shakeMagnitude = 0.1f)
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {

            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = originalPosition + new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return new WaitForSeconds(0.01f);
        }

        transform.position = originalPosition;
        this.exclusiveShake = false;
    }
}
