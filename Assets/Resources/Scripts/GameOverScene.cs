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
        text.text = PlayerPrefs.GetInt("score", 0).ToString();
    }

    public void OnClickGameOver()
    {
        SceneManager.LoadScene(0);
    }
}
