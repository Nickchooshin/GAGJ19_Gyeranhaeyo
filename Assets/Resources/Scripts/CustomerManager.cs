using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class CustomerManager : MonoBehaviour
{
    private string[] wantReview = null;
    private string[] needReview = null;
    private string[] otherReview = null;
    private int[] m_customerIndex = null;
    private int m_customerCount = 0;
    private int m_leftCustomerCount = 0;
    private int m_reviewCustomerCount = 0;
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
    public AdviceBubble advice;
    public Text name;
    public ScanBar scanBar;
    public ScanningTextBlink scanning;
    public FoodSelectUI foodSelectUI;
    public AttentionPanel attentionPanel;
    public AudioSource gibberish;

    public static CustomerManager Instance = null;

    private CustomerManager()
    {
    }

    private void Start()
    {
        Instance = this;

        Init();
        foodSelectUI.SetInteractable(false);
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
    }

    public void InitDayCustomer()
    {
        m_reviewCustomerCount = 0;

        InitCustomerIndex();
        InitReview();
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
            m_customerInfoList[i].SetRandomType();
        }

        m_currentCustomerIndex = 0;
    }

    private void InitReview()
    {
        InitWantReview();
        InitNeedReview();
        InitOtherReview();
    }

    private void InitWantReview()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/want");
        string jsonString = textAsset.text;
        JSONNode node = JSON.Parse(jsonString);

        int count = node.Count;
        wantReview = new string[count];
        for (int i = 0; i < count; i++)
            wantReview[i] = node[i].Value;
    }

    private void InitNeedReview()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/need");
        string jsonString = textAsset.text;
        JSONNode node = JSON.Parse(jsonString);

        int count = node.Count;
        needReview = new string[count];
        for (int i = 0; i < count; i++)
            needReview[i] = node[i].Value;
    }

    private void InitOtherReview()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/other");
        string jsonString = textAsset.text;
        JSONNode node = JSON.Parse(jsonString);

        int count = node.Count;
        otherReview = new string[count];
        for (int i = 0; i < count; i++)
            otherReview[i] = node[i].Value;
    }

    private void VisitCustomer()
    {
        if (m_currentCustomerIndex >= m_customerCount)
        {
            // End of Day
            TimeManager.Instance.StopAllCoroutines();
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

        if (m_currentCustomer != null)
            Destroy(m_currentCustomer.gameObject);
        m_currentCustomer = Instantiate<Customer>(prefabCustomer);
        m_currentCustomer.Init(m_customerInfoList[index]);
        m_currentCustomer.transform.position = createCustomerPosition;
        m_currentCustomer.MoveToPosition(customerPosition, customerMoveTime);

        StartCoroutine(VisitCustomerAnimationWait(customerMoveTime));
    }

    private IEnumerator VisitCustomerAnimationWait(float time)
    {
        yield return new WaitForSeconds(time);

        Function func = () =>
        {
            ShowCustomerScript();
            ShowCustomerAdvice();
            ShowCustomerName();
            foodSelectUI.SetInteractable(true);
            scanning.gameObject.SetActive(false);
        };

        scanning.gameObject.SetActive(true);
        scanning.StartAnimation();
        scanBar.gameObject.SetActive(true);
        scanBar.StartScan(func);
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

    public void StopAll()
    {
        StopAllCoroutines();
        scanBar.gameObject.SetActive(false);
        HideCustomerScript();
        HideCustomerAdvice();
        HideCustomerName();
        foodSelectUI.SetInteractable(false);
    }

    public void ShowCustomerScript()
    {
        int index = m_customerIndex[m_currentCustomerIndex];

        customerScriptBubble.gameObject.SetActive(true);
        customerScriptBubble.text.text = m_customerInfoList[index].Script;
        gibberish.Play();
    }

    public void HideCustomerScript()
    {
        customerScriptBubble.gameObject.SetActive(false);
        gibberish.Stop();

    }

    public void ShowCustomerAdvice()
    {
        int index = m_customerIndex[m_currentCustomerIndex];

        advice.gameObject.SetActive(true);
        advice.text.text = m_customerInfoList[index].Advice;
    }

    public void HideCustomerAdvice()
    {
        advice.gameObject.SetActive(false);
    }

    public void ShowCustomerName()
    {
        int index = m_customerIndex[m_currentCustomerIndex];

        name.text = m_customerInfoList[index].name;
    }

    public void HideCustomerName()
    {
        name.text = "";
    }

    public void SendFoodToCustomer(string food)
    {
        int index = m_customerIndex[m_currentCustomerIndex];

        if (m_customerInfoList[index].Want == food)
        {
            m_customerInfoList[index].WantReview();
            customerScriptBubble.text.text = wantReview[Random.Range(0, wantReview.Length)];
        }
        else if (m_customerInfoList[index].Need == food)
        {
            m_customerInfoList[index].NeedReview();
            customerScriptBubble.text.text = needReview[Random.Range(0, needReview.Length)];
        }
        else
        {
            m_customerInfoList[index].OtherReview();
            customerScriptBubble.text.text = otherReview[Random.Range(0, otherReview.Length)];
        }

        if (m_customerInfoList[index].IsWarning())
        {
            attentionPanel.ShowWarningPanel();
        }

        // 포인트가 음수가 되어서 찾아오지 않게 되었을 때
        if (!m_customerInfoList[index].isVisit)
        {
            // 내래이션? 띄우기
            attentionPanel.ShowAttentionPanel();
            m_leftCustomerCount += 1;

            // 손님이 다 안올 경우 게임오버
            if (m_leftCustomerCount >= m_customerCount)
            {
            }
        }

        m_reviewCustomerCount += 1;

        HideCustomerAdvice();
        HideCustomerName();
        OutCustomer();
        foodSelectUI.SetInteractable(false);
    }

    public int GetCustomerScore()
    {
        int score = 0;

        for (int i = 0; i < m_customerInfoList.Length; i++)
        {
            if (m_customerInfoList[i].isVisit)
            {
                score += m_customerInfoList[i].mentalPoint;
                score += m_customerInfoList[i].physicalPoint;
            }
        }

        // 스코어 점수 공식
        score = (int)(score / 2.0f * ((float)m_reviewCustomerCount / m_customerCount));

        return score;
    }
}
