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
    public SpriteRenderer hair2;

    public void Init(CustomerInfo info)
    {
        body.sprite = Resources.Load<Sprite>("Images/Customer/Body/" + info.Body);
        face.sprite = Resources.Load<Sprite>("Images/Customer/Face/" + info.Face);
        emote.sprite = Resources.Load<Sprite>("Images/Customer/Emote/" + info.Emote);
        hair.sprite = Resources.Load<Sprite>("images/Customer/Hair/" + info.Hair);

        if (info.Hair2 != "")
            hair2.sprite = Resources.Load<Sprite>("Images/Customer/Hair/" + info.Hair2);
    }

    public void MoveToPosition(Vector3 position, float moveTime)
    {
        StartCoroutine(Move(position, moveTime));
    }

    private IEnumerator Move(Vector3 position, float moveTime)
    {
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