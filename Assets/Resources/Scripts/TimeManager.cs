using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float dayTime = 50.0f;
    public Text timer;

    private float m_deltaTime = 0.0f;

    public static TimeManager Instance = null;

    private TimeManager()
    {
    }

    private void Start()
    {
        Instance = this;

        timer.text = string.Format("{0:0.00}", dayTime);
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    public bool IsTimeOver()
    {
        return m_deltaTime >= dayTime;
    }

    private IEnumerator Timer()
    {
        m_deltaTime = 0.0f;

        while (m_deltaTime < dayTime) 
        {
            m_deltaTime += Time.deltaTime;
            timer.text = string.Format("{0:0.00}", (dayTime - m_deltaTime));

            yield return null;
        }

        timer.text = string.Format("{0:0.00}", dayTime);
        GameManager.Instance.EndOfDay();
    }
}
