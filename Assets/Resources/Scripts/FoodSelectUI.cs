using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class FoodSelectUI : MonoBehaviour
{
    public Image[] dishes;
    public Text[] names;

    private string[] foodList;
    private bool m_isInteractable = true;

    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/food_list");
        string jsonString = textAsset.text;

        JSONNode node = JSON.Parse(jsonString);

        int foodLength = node.Count;
        foodList = new string[foodLength];
        for (int i = 0; i < foodLength; i++)
        {
            foodList[i] = node[i]["value"].Value;
            dishes[i].sprite = Resources.Load<Sprite>("Images/Foods/" + foodList[i]);
            names[i].text = node[i]["name"].Value;
        }
    }

    public void SetInteractable(bool flag)
    {
        m_isInteractable = flag;
    }

    public void OnClickSelect(int index)
    {
        if (!m_isInteractable)
            return;
        string food = foodList[index];
        CustomerManager.Instance.SendFoodToCustomer(food);
    }
}
