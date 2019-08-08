using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using SimpleJSON;

public class Customer : MonoBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer face;
    public SpriteRenderer emote;
    public SpriteRenderer hair;

    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/customers");
        string jsonString = textAsset.text;
        JSONNode node = JSON.Parse(jsonString);

        Init(new CustomerInfo(node[0]));
    }

    private void Update()
    {
        
    }

    public void Init(CustomerInfo info)
    {
        body.sprite = Resources.Load<Sprite>("Images/Customer/Body/" + info.body);
        face.sprite = Resources.Load<Sprite>("Images/Customer/Face/" + info.face);
        emote.sprite = Resources.Load<Sprite>("Images/Customer/Emote/" + info.emote);
        hair.sprite = Resources.Load<Sprite>("images/Customer/Hair/" + info.hair);
    }
}