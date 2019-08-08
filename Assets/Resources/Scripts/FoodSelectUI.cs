using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class FoodSelectUI : MonoBehaviour
{
    public Image panel;

    JSONNode m_node = null;
    private int m_currentIndex = 0;
    private int m_foodLength;
    private Sprite[] foodSpriteList;

    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/food_list");
        string jsonString = textAsset.text;

        m_node = JSON.Parse(jsonString);

        m_foodLength = m_node.Count;
        foodSpriteList = new Sprite[m_foodLength];
        for (int i = 0; i < m_foodLength; i++)
            foodSpriteList[i] = Resources.Load<Sprite>("Images/Foods/" + m_node[i].Value);

        UpdatePanelImage();
    }

    public void OnClickPrev()
    {
        m_currentIndex -= 1;

        if (m_currentIndex < 0)
            m_currentIndex += m_foodLength;

        UpdatePanelImage();
    }

    public void OnClickNext()
    {
        m_currentIndex += 1;

        if (m_currentIndex >= m_foodLength)
            m_currentIndex -= m_foodLength;

        UpdatePanelImage();
    }

    private void UpdatePanelImage()
    {
        panel.sprite = foodSpriteList[m_currentIndex];
    }

    public void OnClickSelect()
    {
        //m_node[m_currentIndex].Value;
    }
}
