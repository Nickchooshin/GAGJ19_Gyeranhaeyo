using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float dayTime = 50.0f;
    public float subTimePerStage = 5.0f;
    public float minimumTime = 30.0f;
    public Text timer;

    private float m_deltaTime = 0.0f;

    public static TimeManager Instance = null;

    private TimeManager()
    {
    }

    private void Start()
    {
        Instance = this;

        timer.text = string.Format("{0:00.00}", dayTime);
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        m_deltaTime = 0.0f;

        while (m_deltaTime < GetTimerTime()) 
        {
            m_deltaTime += Time.deltaTime;
            timer.text = string.Format("{0:00.00}", (GetTimerTime() - m_deltaTime));

            yield return null;
        }

        CustomerManager.Instance.StopAll();
        GameManager.Instance.EndOfDay();
        timer.text = string.Format("{0:00.00}", GetTimerTime());
    }

    public int GetTimerScore()
    {
        return (int)(dayTime - m_deltaTime);
    }

    private float GetTimerTime()
    {
        float time = dayTime;
        float sub = subTimePerStage * GameManager.Instance.nowDay;

        time -= sub;

        if (time < minimumTime)
            time = minimumTime;

        return time;
    }
}
