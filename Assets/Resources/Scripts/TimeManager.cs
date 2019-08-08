using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float dayTime = 50.0f;

    private float m_deltaTime = 0.0f;

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

            yield return null;
        }
    }
}
