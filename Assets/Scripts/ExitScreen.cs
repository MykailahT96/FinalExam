using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagment;

public class ExitScreen : MonoBehaviour
{
public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("1Intro");
    }
}
