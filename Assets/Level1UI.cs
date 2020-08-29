using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1UI : MonoBehaviour
{
    [SerializeField]
    GameObject restartButton;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text resultText;

    public void Start()
    {
        HideRestartButton(true);
        resultText.gameObject.SetActive(false);
    }

    public void SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void HideRestartButton(bool hide)
    {
        restartButton.SetActive(!hide);
    }

    public void GameWon(bool isWin)
    {
        if (isWin)
        {
            resultText.text = "YOU WON!!";
        }
        else
        {
            resultText.text = "You Lost :(";
        }

        resultText.gameObject.SetActive(true);
        HideRestartButton(false);
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuButtonClick()
    {
        // TODO - implement button in UI
        SceneManager.LoadScene(nameof(MainMenu));
    }
}
