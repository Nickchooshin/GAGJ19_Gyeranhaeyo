using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class FoodSelectUI : MonoBehaviour
{
    public Image[] dishes;

    private string[] foodList;

    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/food_list");
        string jsonString = textAsset.text;

        JSONNode node = JSON.Parse(jsonString);

        int foodLength = node.Count;
        foodList = new string[foodLength];
        for (int i = 0; i < foodLength; i++)
        {
            foodList[i] = node[i].Value;
            dishes[i].sprite = Resources.Load<Sprite>("Images/Foods/" + foodList[i]);
        }
    }

    public void OnClickSelect(int index)
    {
        string food = foodList[index];
        CustomerManager.Instance.SendFoodToCustomer(food);
    }
}
