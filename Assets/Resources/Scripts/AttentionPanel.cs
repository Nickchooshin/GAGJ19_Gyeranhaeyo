using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttentionPanel : MonoBehaviour
{
    public float moveTime = 1.0f;
    public float stayTime = 2.0f;
    public float movePositionY = 0.0f;

    public void ShowAttentionPanel()
    {
        StopAllCoroutines();
        StartCoroutine(MoveAnimation());
    }

    private IEnumerator MoveAnimation()
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition;
        endPosition.y += movePositionY;

        while (Time.time < startTime + moveTime)
        {
            float percent = (Time.time - startTime) / moveTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, percent);

            yield return null;
        }

        transform.position = endPosition;

        yield return new WaitForSeconds(stayTime);

        startTime = Time.time;

        while (Time.time < startTime + moveTime)
        {
            float percent = (Time.time - startTime) / moveTime;
            transform.position = Vector3.Lerp(endPosition, startPosition, percent);

            yield return null;
        }

        transform.position = startPosition;
    }
}
