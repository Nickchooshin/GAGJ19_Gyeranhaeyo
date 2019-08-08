using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public float fadeInOutTime = 0.5f;
    public float daysTextShowTime = 2.0f;
    public int nowDay = -1;
    public Image fade;
    public Text dayText;
    public Text dayText2;

    private GameManager()
    {
    }

    private void Start()
    {
        Instance = this;

        EndOfDay();
    }

    public void EndOfDay()
    {
        nowDay += 1;
        if (nowDay > 0)
            ScoreManager.Instance.CalcScoreToday();
        dayText2.text = string.Format("{0} Weeks", nowDay + 1);
        StartCoroutine(EndDay());
    }

    private IEnumerator EndDay()
    {
        const float alpha = 0.5f;
        float startTime = Time.time;

        fade.gameObject.SetActive(true);

        while (Time.time < startTime + fadeInOutTime)
        {
            float percent = (Time.time - startTime) / fadeInOutTime;

            fade.color = new Color(0.0f, 0.0f, 0.0f, percent * alpha);

            yield return null;
        }

        fade.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        dayText.gameObject.SetActive(true);
        dayText.text = string.Format("{0} Days", nowDay + 1);

        yield return new WaitForSeconds(daysTextShowTime);

        dayText.gameObject.SetActive(false);

        startTime = Time.time;

        while (Time.time < startTime + fadeInOutTime)
        {
            float percent = 1.0f - ((Time.time - startTime) / fadeInOutTime);

            fade.color = new Color(0, 0, 0, percent * alpha);

            yield return null;
        }

        fade.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        fade.gameObject.SetActive(false);

        CustomerManager.Instance.InitDayCustomer();
        TimeManager.Instance.StartTimer();
    }
}
