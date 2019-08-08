using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayScene : MonoBehaviour
{
    public void OnClickHowToPlay()
    {
        SceneManager.LoadScene(0);
    }
}
