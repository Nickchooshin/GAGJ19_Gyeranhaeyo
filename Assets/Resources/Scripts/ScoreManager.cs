using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int m_score = 0;

    public Text score;

    public static ScoreManager Instance = null;

    private void Start()
    {
        Instance = this;
    }

    public void CalcScoreToday()
    {
        m_score += TimeManager.Instance.GetTimerScore();
        m_score += CustomerManager.Instance.GetCustomerScore();

        UpdateScore();
    }

    private void UpdateScore()
    {
        score.text = m_score.ToString();
    }
}
