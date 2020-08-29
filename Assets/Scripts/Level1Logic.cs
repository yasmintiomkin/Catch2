using UnityEngine;

public class Level1Logic : MonoBehaviour
{
    [SerializeField]
    GameObject ui;
    private Level1UI level1UI;

    public int numTargetClicksToWin = 2;

    private int score = 0;
    private int numTargetClicks = 0;

    public void Start()
    {
        ResumeGame(); // just in case it was previously paused...

        level1UI = ui.GetComponent<Level1UI>();
    }

    void UpdateScore(int score)
    {
        this.score += score;
        level1UI.SetScore(this.score);
    }

    public void OnTargetClicked(PieceData data)
    {
        UpdateScore(data.score);
        numTargetClicks++;

        if (numTargetClicks == numTargetClicksToWin)
        {
            level1UI.GameWon(true);
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
        Debug.Log("OnNonTargetFallFromBottom");
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
