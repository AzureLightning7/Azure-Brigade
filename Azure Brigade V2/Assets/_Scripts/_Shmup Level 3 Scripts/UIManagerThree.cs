using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerThree : MonoBehaviour {

    public Sprite[] numLivesImage; // Array of images
    public Image livesDisplay; // Reference to UI element

    //Score variables
    public int score = 0;
    public Text scoreText;
    public Text endScoreText;

    //Game Over variables
    public GameObject gameOver;
    public GameObject gameOverScore;
    public GameObject mainScore;
    public int winScore;

    //Win Game variables
    public GameObject winText;

    //Objective variables
    public Text objective;
    public GameObject objectiveWord;
    public GameObject objectiveObject;

    void Start()
    {
        if (gameOver != null)
            gameOver.SetActive(false);
        gameOverScore.SetActive(false);
        mainScore.SetActive(true);
        objectiveObject.SetActive(true);
        objective.text = "Get Score: " + winScore;
    }

    void Update()
    {
        if (score >= winScore)
        {
            Invoke("WinGame", 0);
            Invoke("KillEnemies", 0);
        }
    }

    public void UpdateLives(int currentLives)
    {
        // update the lives Image
        livesDisplay.sprite = numLivesImage[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
        endScoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameOverScore.SetActive(true);
        mainScore.SetActive(false);
        objectiveObject.SetActive(false);
        objectiveWord.SetActive(false);
    }

    public void WinGame()
    {
        winText.SetActive(true);
        gameOverScore.SetActive(true);
        mainScore.SetActive(false);
        objectiveObject.SetActive(false);
        objectiveWord.SetActive(false);
        Invoke("LoadNextLevel", 5);
    }

    public void KillEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        foreach (GameObject powerup in GameObject.FindGameObjectsWithTag("Powerup"))
        {
            Destroy(powerup);
        }

        foreach (GameObject shield in GameObject.FindGameObjectsWithTag("ShieldPowerup"))
        {
            Destroy(shield);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
