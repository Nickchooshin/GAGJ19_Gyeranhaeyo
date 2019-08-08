using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Function();

public class ScanBar : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float time = 1.0f;

    private Function m_func;

    public void StartScan(Function func)
    {
        m_func = func;

        StartCoroutine(ScanAnimation());
    }

    private IEnumerator ScanAnimation()
    {
        float startTime = Time.time;

        while (Time.time < startTime + time)
        {
            float percent = (Time.time - startTime) / time;

            transform.position = Vector3.Lerp(startPosition, endPosition, percent);

            yield return null;
        }

        transform.position = endPosition;
        startTime = Time.time;

        while (Time.time < startTime + time)
        {
            float percent = (Time.time - startTime) / time;

            transform.position = Vector3.Lerp(endPosition, startPosition, percent);

            yield return null;
        }

        transform.position = startPosition;

        m_func();

        gameObject.SetActive(false);
    }
}
