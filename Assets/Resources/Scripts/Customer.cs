using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class Customer : MonoBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer face;
    public SpriteRenderer emote;
    public SpriteRenderer hair;

    public void Init(CustomerInfo info)
    {
        body.sprite = Resources.Load<Sprite>("Images/Customer/Body/" + info.body);
        face.sprite = Resources.Load<Sprite>("Images/Customer/Face/" + info.face);
        emote.sprite = Resources.Load<Sprite>("Images/Customer/Emote/" + info.emote);
        hair.sprite = Resources.Load<Sprite>("images/Customer/Hair/" + info.hair);
    }

    public void MoveToPosition(Vector3 position, float moveTime, float delayTime)
    {
        StartCoroutine(Move(position, moveTime, delayTime));
    }

    private IEnumerator Move(Vector3 position, float moveTime, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time < startTime + moveTime)
        {
            float percent = (Time.time - startTime) / moveTime;
            transform.position = Vector3.Lerp(startPosition, position, percent);

            yield return null;
        }

        transform.position = position;
    }
}