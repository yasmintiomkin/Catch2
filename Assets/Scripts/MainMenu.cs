using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayEasyButtonClick()
    {
        DataSource.difficulty = 1;
        DataSource.PrepareNewSession();
        LoadLevel1();
    }

    public void OnPlayMediumButtonClick()
    {
        DataSource.difficulty = 2;
        DataSource.PrepareNewSession();
        LoadLevel1();
    }

    public void OnPlayHardButtonClick()
    {
        DataSource.difficulty = 3;
        DataSource.PrepareNewSession();
        LoadLevel1();
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
