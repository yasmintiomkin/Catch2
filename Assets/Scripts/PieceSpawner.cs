using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{


    public GameObject targetPiecePF;
    public GameObject nonTargetPiecePF;

    [SerializeField]
    List<GameObject> spawnPoints;

    DataSource.Difficulty difficulty = DataSource.Difficulty.easy;

    public void Spawn()
    {
        difficulty = DataSource.difficultyEnum;

        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        var indexGenerator = new SpawnerTargetPieceIndexGenerator();
        indexGenerator.Perform(difficulty);
        var spriteGenerator = new SpawnerSpriteGenerator(difficulty);
        spriteGenerator.Perform();

        for (int i = 0; i < indexGenerator.totalPieces; i++)
        {
            int index = i % spawnPoints.Count;
            GameObject spawnPoint = spawnPoints[index];
            Vector3 position = spawnPoint.transform.position;
            GameObject comp;

            if (Array.IndexOf(indexGenerator.targetPieceIndices, i) != -1)
            {
                comp = Instantiate(targetPiecePF, position, Quaternion.identity);
                if (spriteGenerator.targetSprite != null)
                {
                    comp.GetComponent<SpriteRenderer>().sprite = spriteGenerator.targetSprite;
                }
                comp.GetComponent<Rigidbody2D>().gravityScale *= (int)difficulty;
            }
            else
            {
                comp = Instantiate(nonTargetPiecePF, position, Quaternion.identity);
                Sprite nonTSprite = spriteGenerator.nonTSprite;
                if (nonTSprite != null)
                {
                    comp.GetComponent<SpriteRenderer>().sprite = spriteGenerator.nonTSprite;
                }
                comp.GetComponent<Rigidbody2D>().gravityScale *= (int)difficulty;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}

class SpawnerTargetPieceIndexGenerator
{
    public int totalPieces = 10;
    public int[] targetPieceIndices = { 0, 3 };

    public void Perform(DataSource.Difficulty difficulty)
    {
        switch (difficulty)
        {
            case DataSource.Difficulty.easy:
                targetPieceIndices = new int[] { 0, 3 };
                totalPieces = 10;
                break;

            case DataSource.Difficulty.medium:
                targetPieceIndices = new int[] { 3, 7 };
                totalPieces = 16;
                break;

            case DataSource.Difficulty.hard:
                targetPieceIndices = new int[] { 5, 11 };
                totalPieces = 20;
                break;
        }
    }

}

class SpawnerSpriteGenerator
{
    // array length must be EVEN
    string[] spriteNames = { "elephant", "giraffe", "hippo", "monkey", "panda", "parrot", "penguin", "pig", "rabbit", "snake" };
    DataSource.Difficulty difficulty;

    public Sprite targetSprite;

    private Sprite _nonTSprite;
    public Sprite nonTSprite
    {
        get
        {
            if (difficulty == DataSource.Difficulty.hard)
            {
                GenerateNextNonTSprite();
            }
            return _nonTSprite;
        }
    }

    public SpawnerSpriteGenerator(DataSource.Difficulty difficulty)
    {
        this.difficulty = difficulty;
    }

    public void Perform()
    {
        targetSprite = null;
        _nonTSprite = null;

        int nextSpriteIndex = DataSource.nextSpriteIndex;
        if (nextSpriteIndex >= spriteNames.Length - 1)
        {
            nextSpriteIndex = 0;
        }

        string targetSpriteName = spriteNames[nextSpriteIndex];
        string nonTSN = spriteNames[nextSpriteIndex + 1];
        DataSource.nextSpriteIndex = nextSpriteIndex + 2; // for next time...

        targetSprite = Resources.Load<Sprite>("Round without details (outline)/" + targetSpriteName);
        _nonTSprite = Resources.Load<Sprite>("Square without details (outline)/" + nonTSN);
    }

    void GenerateNextNonTSprite()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond); // make randow seem more random
        int nextSpriteIndex = UnityEngine.Random.Range(0, spriteNames.Count());
        string nonTSN = spriteNames[nextSpriteIndex];
        _nonTSprite = Resources.Load<Sprite>("Square without details (outline)/" + nonTSN);
    }
}
