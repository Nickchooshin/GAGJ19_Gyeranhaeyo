using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickHowToPlay()
    {
        SceneManager.LoadScene(3);
    }

    public void OnClickCredit()
    {
        SceneManager.LoadScene(2);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
