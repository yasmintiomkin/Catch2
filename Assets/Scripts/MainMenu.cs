using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Toggle easyToggle;
    public Toggle mediumToggle;
    public Toggle hardToggle;

    bool ignoreValueChanged = false;
    int difficulty = 1;

    private void Start()
    {
        difficulty = DataSource.difficulty;
        
        // check saved selection
        Toggle[] arr = { easyToggle, mediumToggle, hardToggle };
        arr[difficulty - 1].isOn = true;
    }

    public void OnPlayEasyToggleValueChanged(bool value)
    {
        UpdateDifficultyLevel(1, easyToggle, mediumToggle, hardToggle);
    }   
    
    public void OnPlayMediumToggleValueChanged(bool value)
    {
        UpdateDifficultyLevel(2, mediumToggle, easyToggle, hardToggle);
    }
    
    public void OnPlayHardToggleValueChanged(bool value)
    {
        UpdateDifficultyLevel(3, hardToggle, mediumToggle, easyToggle);
    }

    public void OnPlayButtonClick()
    {
        DataSource.difficulty = difficulty; // save selection
        DataSource.PrepareNewSession();
        SceneManager.LoadScene("Level1");
    }

    void UpdateDifficultyLevel(int difficulty, Toggle selected, Toggle other1, Toggle other2)
    {
        if (ignoreValueChanged) return;
        ignoreValueChanged = true;
        if (this.difficulty == difficulty)
        {
            selected.isOn = true;
        }
        else
        {
            other1.isOn = false;
            other2.isOn = false;
            this.difficulty = difficulty;
        }
        Debug.Log("difficulty: " + difficulty);
        ignoreValueChanged = false;
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
