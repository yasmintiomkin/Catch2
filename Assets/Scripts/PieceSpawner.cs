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
    // Round without details (outline)
    // Square without details (outline)

    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            int index = i % spawnPoints.Count;
            GameObject spawnPoint = spawnPoints[index];
            Vector3 position = spawnPoint.transform.position;
            GameObject comp;

            //var sprite = Resources.Load<Sprite>("Round without details (outline)/pig");


            if (i == 0 || i == 3)
            {
                comp = Instantiate(targetPiecePF, position, Quaternion.identity);
                //comp.GetComponent<SpriteRenderer>().sprite = sprite;
            }
            else
            {
                comp = Instantiate(nonTargetPiecePF, position, Quaternion.identity);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
