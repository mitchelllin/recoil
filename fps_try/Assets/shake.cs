using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-.25f, .25f) * magnitude;
            float y = Random.Range(0f, 1f) * magnitude;

            transform.localPosition = new Vector3(x,y, originalPos.z);

            elapsed += Time.deltaTime;
            magnitude -= magnitude/4;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
