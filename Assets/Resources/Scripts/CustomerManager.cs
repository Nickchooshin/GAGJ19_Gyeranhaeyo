using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class CustomerManager : MonoBehaviour
{
    private string[] negativeReview = null;
    private int[] m_customerIndex = null;
    private int m_customerCount = 0;
    private int m_currentCustomerIndex = 0;
    private CustomerInfo[] m_customerInfoList = null;
    private Customer m_currentCustomer = null;
    private Dictionary<string, int> m_customerMental = new Dictionary<string, int>();
    private Dictionary<string, int> m_customerPhysical = new Dictionary<string, int>();

    public Customer prefabCustomer;

    public float customerMoveTime = 1.0f;
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
    }

    private void Init()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/customers");
        string jsonString = textAsset.text;
        JSONNode node = JSON.Parse(jsonString);

        m_customerCount = node.Count;
        m_customerIndex = new int[m_customerCount];

        m_customerInfoList = new CustomerInfo[m_customerCount];
        for (int i = 0; i < m_customerCount; i++)
            m_customerInfoList[i] = new CustomerInfo(node[i]);

        //InitDayCustomer();
    }

    public void InitDayCustomer()
    {
        InitCustomerIndex();
        InitNegativeReview();
        VisitCustomer();
    }

    private void InitCustomerIndex()
    {
        for (int i = 0; i < m_customerCount; i++)
            m_customerIndex[i] = i;

        for (int i = 0; i < m_customerCount; i++)
        {
            int rand = Random.Range(0, m_customerCount);
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
        if (m_currentCustomerIndex >= m_customerCount)
        {
            // End of Day
            GameManager.Instance.EndOfDay();
            return;
        }
        int index = m_customerIndex[m_currentCustomerIndex];

        if (!m_customerInfoList[index].isVisit)
        {
            m_currentCustomerIndex += 1;
            VisitCustomer();
            return;
        }

        m_currentCustomer = Instantiate<Customer>(prefabCustomer);
        m_currentCustomer.Init(m_customerInfoList[index]);
        m_currentCustomer.transform.position = createCustomerPosition;
        m_currentCustomer.MoveToPosition(customerPosition, customerMoveTime);

        StartCoroutine(VisitCustomerAnimationWait(customerMoveTime));
    }

    private IEnumerator VisitCustomerAnimationWait(float time)
    {
        yield return new WaitForSeconds(time);

        //
        UpdatePoint();
        ShowCustomerScript();
    }

    private void OutCustomer()
    {
        Vector3 position = createCustomerPosition;
        position.x = -position.x;
        m_currentCustomer.MoveToPosition(position, customerMoveTime);

        m_currentCustomerIndex += 1;

        StartCoroutine(OutCustomerAnimationWait(customerMoveTime));
    }
    private IEnumerator OutCustomerAnimationWait(float time)
    {
        yield return new WaitForSeconds(time);

        VisitCustomer();
    }

    public void ShowCustomerScript()
    {
        int index = m_customerIndex[m_currentCustomerIndex];

        customerScriptBubble.gameObject.SetActive(true);
        customerScriptBubble.text.text = m_customerInfoList[index].Script;
    }

    public void HideCustomerScript()
    {
        customerScriptBubble.gameObject.SetActive(false);
    }

    public void ShowCustomerAdvice()
    {
        int index = m_customerIndex[m_currentCustomerIndex];

        advice.text = m_customerInfoList[index].Advice;
    }

    public void SendFoodToCustomer(string food)
    {
        int index = m_customerIndex[m_currentCustomerIndex];

        if (m_customerInfoList[index].Want == food)
        {
            m_customerInfoList[index].WantReview();
        }
        else if (m_customerInfoList[index].Need == food)
        {
            m_customerInfoList[index].NeedReview();
        }
        else
        {
            m_customerInfoList[index].OtherReview();
            customerScriptBubble.text.text = negativeReview[Random.Range(0, negativeReview.Length)];
        }

        // 포인트가 음수가 되어서 찾아오지 않게 되었을 때
        if (!m_customerInfoList[index].isVisit)
        {
        }

        UpdatePoint();
        OutCustomer();
    }

    // Debug
    public Text mantalText;
    public Text physicalText;
    public Text textName;
    private void UpdatePoint()
    {
        int index = m_customerIndex[m_currentCustomerIndex];

        textName.text = m_customerInfoList[index].name;
        mantalText.text = m_customerInfoList[index].mentalPoint.ToString();
        physicalText.text = m_customerInfoList[index].physicalPoint.ToString();
    }
}
