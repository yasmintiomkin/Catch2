using UnityEngine;

public class Level1Logic : MonoBehaviour
{
    [SerializeField]
    Level1UI level1UI;

    [SerializeField]
    PieceSpawner spawner;

    public int numTargetClicksToWin = 2;

    private int score = 0;
    private int numTargetClicks = 0;


    public void Start()
    {
        ResumeGame(); // just in case it was previously paused...

        spawner.Spawn();
    }

    void UpdateScore(int score)
    {
        this.score += score;
        level1UI.SetScore(this.score);

        DataSource.updateTopScores(this.score);
    }

    public void OnTargetClicked(PieceData data)
    {
        UpdateScore(data.score);
        numTargetClicks++;

        if (numTargetClicks == numTargetClicksToWin)
        {
            level1UI.GameWon(true);
            DataSource.addNewWinInSession();
            PauseGame();
        }
    }

    public void OnTargetFallFromBottom(PieceData data)
    {
        level1UI.GameWon(false);
        PauseGame();
    }

    public void OnNonTargetClicked(PieceData data)
    {
        UpdateScore(data.score);
    }

    public void OnNonTargetFallFromBottom(PieceData data)
    {
        // we don't care
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
