using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public int nowDay = -1;
    public Image fade;
    public Text dayText;

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
        StartCoroutine(EndDay(0.5f));
    }

    private IEnumerator EndDay(float time)
    {
        const float alpha = 0.5f;
        float startTime = Time.time;

        fade.gameObject.SetActive(true);

        while (Time.time < startTime + time)
        {
            float percent = (Time.time - startTime) / time;

            fade.color = new Color(0.0f, 0.0f, 0.0f, percent * alpha);

            yield return null;
        }

        fade.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        dayText.gameObject.SetActive(true);
        dayText.text = string.Format("{0} Days", nowDay + 1);

        yield return new WaitForSeconds(2.0f);

        dayText.gameObject.SetActive(false);

        startTime = Time.time;

        while (Time.time < startTime + time)
        {
            float percent = 1.0f - ((Time.time - startTime) / time);

            fade.color = new Color(0, 0, 0, percent * alpha);

            yield return null;
        }

        fade.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        fade.gameObject.SetActive(false);

        CustomerManager.Instance.InitDayCustomer();
    }
}
