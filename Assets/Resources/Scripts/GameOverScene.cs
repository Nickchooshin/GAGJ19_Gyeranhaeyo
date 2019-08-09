using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text.text = string.Format("SCORE {0}", PlayerPrefs.GetInt("score", 0));
    }

    public void OnClickGameOver()
    {
        SceneManager.LoadScene(0);
    }
}
