using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Dropdown livesSelection;
    public InputField textNameBox;
    public Slider timeSlider;
    public GameObject NextButtonUI;
    
    public static int totalLives;
    public static float timeLimit;
    

    public void SetTime()
    {
        timeLimit = timeSlider.value;
        PlayerPrefs.SetFloat("timeLimit", timeLimit);
        Debug.Log(PlayerPrefs.GetFloat("timeLimit"));
    }

    public void SetLives()
    {
       totalLives = livesSelection.value;
       if (livesSelection.value == 0)
       {
          NextButtonUI.SetActive(false);
       }
       else
       {
           NextButtonUI.SetActive(true);
       }
        PlayerPrefs.SetInt("totalLives", totalLives);
        Debug.Log(PlayerPrefs.GetInt("totalLives")); 
    }

    public void SetName()
    {
        PlayerPrefs.SetString("playerName", textNameBox.text);
        Debug.Log(PlayerPrefs.GetString("playerName"));
    }

    public void StartGame()
    {
        SceneManager.LoadScene("2Game");
    }
}
