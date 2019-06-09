using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void StartLv01()
    {
        SceneManager.LoadScene("Lv.01");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
