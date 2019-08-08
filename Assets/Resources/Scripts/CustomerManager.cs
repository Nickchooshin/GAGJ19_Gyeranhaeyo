using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class CustomerManager : MonoBehaviour
{
    private JSONNode m_node;
    private string[] negativeReview = null;
    private int[] m_customerIndex = null;
    private int m_customerLength = 0;
    private int m_currentCustomerIndex = 0;
    private CustomerInfo m_currentCustomerInfo = null;
    private Customer m_currentCustomer = null;

    public Customer prefabCustomer;

    public Vector3 createCustomerPosition;
    public Vector3 customerPosition;
    public CustomerScriptBubble customerScriptBubble;
    public Text advice;

    public static CustomerManager Instance = null;

    private CustomerManager()
    {
    }

    private void Start()
    {
        Instance = this;

        Init();
        VisitCustomer();
    }

    private void Init()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/customers");
        string jsonString = textAsset.text;
        m_node = JSON.Parse(jsonString);

        m_customerLength = m_node.Count;
        m_customerIndex = new int[m_customerLength];
        InitCustomerIndex();
        InitNegativeReview();
    }

    private void InitCustomerIndex()
    {
        for (int i = 0; i < m_customerLength; i++)
            m_customerIndex[i] = i;

        for (int i = 0; i < m_customerLength; i++)
        {
            int rand = Random.Range(0, m_customerLength);
            int temp = m_customerIndex[i];
            m_customerIndex[i] = m_customerIndex[rand];
            m_customerIndex[rand] = temp;
        }

        m_currentCustomerIndex = 0;
    }

    private void InitNegativeReview()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/negative");
        string jsonString = textAsset.text;
        JSONNode node = JSON.Parse(jsonString);

        int count = node.Count;
        negativeReview = new string[count];
        for (int i = 0; i < count; i++)
            negativeReview[i] = node[i].Value;
    }

    private void VisitCustomer()
    {
        if (m_currentCustomerIndex >= m_customerLength)
        {
            // End of Day
            return;
        }

        int index = m_customerIndex[m_currentCustomerIndex];
        m_currentCustomerInfo = new CustomerInfo(m_node[index]);

        m_currentCustomer = Instantiate<Customer>(prefabCustomer);
        m_currentCustomer.Init(m_currentCustomerInfo);
        m_currentCustomer.transform.position = createCustomerPosition;
        m_currentCustomer.MoveToPosition(customerPosition, 1.0f);

        //
        ShowCustomerScript();
    }

    private void OutCustomer()
    {
        Vector3 position = createCustomerPosition;
        position.x = -position.x;
        m_currentCustomer.MoveToPosition(position, 1.0f);

        m_currentCustomerIndex += 1;

        VisitCustomer();
    }

    public void ShowCustomerScript()
    {
        customerScriptBubble.gameObject.SetActive(true);
        customerScriptBubble.text.text = m_currentCustomerInfo.script;
    }

    public void HideCustomerScript()
    {
        customerScriptBubble.gameObject.SetActive(false);
    }

    public void ShowCustomerAdvice()
    {
        advice.text = m_currentCustomerInfo.advice;
    }

    public void SendFoodToCustomer(string food)
    {
        //
        if (m_currentCustomerInfo.want == food)
        {
        }
        else
        {
            customerScriptBubble.text.text = negativeReview[Random.Range(0, negativeReview.Length)];
        }

        OutCustomer();
    }
}
