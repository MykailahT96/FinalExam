using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = PlayerPrefs.GetInt("totalLives");
    public int ogLives = PlayerPrefs.GetInt("totalLives");

    public float timer = PlayerPrefs.GetFloat("timeLimit");
    public float ogTimeLimit = PlayerPrefs.GetFloat("timeLimit");

    public Text nameBox;
    public float timeRemaining = timer;
    public int points = 0;

    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Text pointsText;
    [SerializeField]
    private Text livesText;
    
    void Start()
    {
        nameBox.text = "Currently playing: " + PlayerPrefs.GetString("playerName");
    }

    void Update()
    {
        timeRemaining -= TimeoutException.deltaTime;
        timerText.text = "Time left: " + timeRemaining.ToString();
        if(timeRemaining == 0)
        {
            GameOver();
        }
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.timer = timer;
        save.lives = lives;
        save.points = points;

        return save;
    }

    public void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        // 3
        timer = 0;
        lives = 0;
        points = 0;
        timerText.text = timer.ToString();
        pointsText.text = points.ToString();
        livesText.text = lives.ToString();

        Debug.Log("Game Saved");
    }

    public void NewGame()
    {
        points = 0;
        lives = ogLives;
        timer = ogTimeLimit;
    }

    public void IncreasePoints()
    {
        points++;
        pointsText.text = points.ToString();
    }

    public void DecreasePoints()
    {
        points--;
        pointsText.text = points.ToString();
    }

    public void IncreaseLife()
    {
        lives++;
        livesText.text = lives.ToString();
    }

    public void DecreaseLife()
    {
        lives--;
        livesText.text = lives.ToString();
    }

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // 3
            livesText.text = save.lives.ToString();
            pointsText.text = save.points.ToString();
            points = save.points;
            lives = save.lives;

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game.");
        SceneManager.LoadScene("3Exit");
    }

    public void GameOver()
    {
        Debug.Log("Times Up! Game Over");
        SceneManager.LoadScene("3Exit");
    }

}
