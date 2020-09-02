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
    IEnumerator coroutine;

    public void Spawn()
    {
        difficulty = DataSource.difficultyEnum;

        if (coroutine != null)
        {
            // stop previous spawning
            StopCoroutine(coroutine);
        }

        coroutine = SpawnCoroutine();
        StartCoroutine(coroutine);
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
                comp.GetComponent<Rigidbody2D>().gravityScale *= spriteGenerator.gravityScaleMultiplier;//(int)difficulty;
            }
            else
            {
                comp = Instantiate(nonTargetPiecePF, position, Quaternion.identity);
                Sprite nonTSprite = spriteGenerator.nonTSprite;
                if (nonTSprite != null)
                {
                    comp.GetComponent<SpriteRenderer>().sprite = spriteGenerator.nonTSprite;
                }
                comp.GetComponent<Rigidbody2D>().gravityScale *= spriteGenerator.gravityScaleMultiplier;//(int)difficulty;
            }

            yield return new WaitForSeconds(0.5f / (int)difficulty);
        }
    }
}

class SpawnerTargetPieceIndexGenerator
{
    public int totalPieces = 10;
    public int[] targetPieceIndices = { 0, 5 };

    public void Perform(DataSource.Difficulty difficulty)
    {
        int index1 = 0, index2 = 5;
        switch (difficulty)
        {
            case DataSource.Difficulty.easy:
                totalPieces = 10;
                index1 = 0;
                index2 = 5;
                break;

            case DataSource.Difficulty.medium:
                totalPieces = 16;
                index1 = Rand(2, totalPieces - 4);
                index2 = Rand(index1 + 1, totalPieces);
                targetPieceIndices = new int[] { index1, index2 };
                break;

            case DataSource.Difficulty.hard:
                totalPieces = 20;
                index1 = Rand(3, totalPieces - 6);
                index2 = Rand(index1 + 1, totalPieces);
                targetPieceIndices = new int[] { index1, index2 };
                break;
        }

        targetPieceIndices = new int[] { index1, index2 };
    }

    int Rand(int min, int max)
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond + max); // make randow seem more random
        int rand = UnityEngine.Random.Range(min, max);
        return rand;
    }
}

class SpawnerSpriteGenerator
{
    // array length must be EVEN
    string[] spriteNames = { "elephant", "giraffe", "hippo", "monkey", "panda", "parrot", "penguin", "pig", "rabbit", "snake" };
    DataSource.Difficulty difficulty;

    public float gravityScaleMultiplier = 1.0f;

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

        switch (difficulty)
        {
            case DataSource.Difficulty.easy: gravityScaleMultiplier = 1.0f; break;
            case DataSource.Difficulty.medium: gravityScaleMultiplier = 5.0f; break;
            case DataSource.Difficulty.hard: gravityScaleMultiplier = 8.0f; break;
        }
    }

    void GenerateNextNonTSprite()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond); // make randow seem more random
        int nextSpriteIndex = UnityEngine.Random.Range(0, spriteNames.Count());
        string nonTSN = spriteNames[nextSpriteIndex];
        _nonTSprite = Resources.Load<Sprite>("Square without details (outline)/" + nonTSN);
    }
}
