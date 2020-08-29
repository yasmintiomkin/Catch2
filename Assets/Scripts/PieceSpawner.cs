using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    /*
    public PairPieceController pairPiecePF;
    public DontPairPieceController dontPairPiecePF;

    public GameLogic gameLogic;

    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 position = new Vector3(-1.5f + i / 10.0f, 3.4f, 0);
            PieceController comp;

            if (i == 0 || i == 8)
            {
                comp = Instantiate(pairPiecePF, position, Quaternion.identity);
            }
            else
            {
                comp = Instantiate(dontPairPiecePF, position, Quaternion.identity);
            }

            comp.gameLogic = gameLogic;

            yield return new WaitForSeconds(1f);
        }
    }
    */
}
