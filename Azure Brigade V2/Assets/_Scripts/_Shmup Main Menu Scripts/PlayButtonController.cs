using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour {

    public void GotoMainScene()
    {
        SceneManager.LoadScene("Shmup Level 1");
    }

    public void GotoSecondScene()
    {
        SceneManager.LoadScene("Shmup Level 2");
    }

    public void GotoThirdScene()
    {
        SceneManager.LoadScene("Shmup Level 3");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GotoLevelScene()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
