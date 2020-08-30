using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public GameObject targetPiecePF;
    public GameObject nonTargetPiecePF;

    [SerializeField]
    List<GameObject> spawnPoints;

    
    public void Spawn()
    {
         StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        var spriteGenerator = new SpawnerSpriteGenerator();
        spriteGenerator.Next();

        int[] targetPieceIndices = { 0, 3 };

        for (int i = 0; i < 10; i++)
        {
            int index = i % spawnPoints.Count;
            GameObject spawnPoint = spawnPoints[index];
            Vector3 position = spawnPoint.transform.position;
            GameObject comp;

            if (Array.IndexOf(targetPieceIndices, i) != -1) 
            {
                comp = Instantiate(targetPiecePF, position, Quaternion.identity);
                if (spriteGenerator.targetSprite != null)
                {
                    comp.GetComponent<SpriteRenderer>().sprite = spriteGenerator.targetSprite;
                }
            }
            else
            {
                comp = Instantiate(nonTargetPiecePF, position, Quaternion.identity);
                if (spriteGenerator.nonTSprite != null)
                {
                    comp.GetComponent<SpriteRenderer>().sprite = spriteGenerator.nonTSprite;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}

class SpawnerTargetPieceIndexGenerator
{

}

class SpawnerSpriteGenerator
{
    // array length must be EVEN
    string[] spriteNames = { "elephant", "giraffe", "hippo", "monkey", "panda", "parrot", "penguin", "pig", "rabbit", "snake" };

    public Sprite targetSprite;
    public Sprite nonTSprite;

    public void Next()
    {
        targetSprite = null;
        nonTSprite = null;

        int nextSpriteIndex = DataSource.nextSpriteIndex;
        if (nextSpriteIndex >= spriteNames.Length - 1)
        {
            nextSpriteIndex = 0;
        }

        string targetSpriteName = spriteNames[nextSpriteIndex];
        string nonTSN = spriteNames[nextSpriteIndex + 1];
        DataSource.nextSpriteIndex = nextSpriteIndex + 2; // for next time...

        targetSprite = Resources.Load<Sprite>("Round without details (outline)/" + targetSpriteName);
        nonTSprite = Resources.Load<Sprite>("Square without details (outline)/" + nonTSN);

    }

}
