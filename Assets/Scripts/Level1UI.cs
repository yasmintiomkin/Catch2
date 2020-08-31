using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1UI : MonoBehaviour
{
    [SerializeField]
    GameObject winBackground;

    [SerializeField]
    GameObject restartButton;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text topScoreInSessionText, topScoreText;

    [SerializeField]
    Text resultText;

    public void Start()
    {
        ShowResultUI(false);
    }

    public void SetScore(int score)
    {
        scoreText.text = "" + score;
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

        topScoreInSessionText.text = "Best Score Today:\n" + DataSource.topScoreInSession;
        topScoreText.text = "Best of the Best:\n" + DataSource.topScore;

        ShowResultUI(true);
    }

    void ShowResultUI(bool show)
    {
        winBackground.SetActive(show);
        resultText.gameObject.SetActive(show);
        topScoreInSessionText.gameObject.SetActive(show);
        topScoreText.gameObject.SetActive(show);
        restartButton.SetActive(show);
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
