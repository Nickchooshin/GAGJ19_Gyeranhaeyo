using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanningTextBlink : MonoBehaviour
{
    public Text text;
    public float blinkTime = 0.5f;

    public void StartAnimation()
    {
        StartCoroutine(FadeInAnimation());
    }

    private IEnumerator FadeInAnimation()
    {
        float startTime = Time.time;

        while (Time.time < startTime + blinkTime)
        {
            float percent = (Time.time - startTime) / blinkTime;
            text.color = new Color(1.0f, 1.0f, 1.0f, percent);

            yield return null;
        }

        text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        StartCoroutine(FadeOutAnimation());
    }

    private IEnumerator FadeOutAnimation()
    {
        float startTime = Time.time;

        while (Time.time < startTime + blinkTime)
        {
            float percent = 1.0f - ((Time.time - startTime) / blinkTime);
            text.color = new Color(1.0f, 1.0f, 1.0f, percent);

            yield return null;
        }

        text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        StartCoroutine(FadeInAnimation());
    }
}
