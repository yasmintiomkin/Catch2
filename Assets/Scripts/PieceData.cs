using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PieceData", menuName = "Piece Data", order = 51)]
public class PieceData : ScriptableObject
{
    public Sprite icon;
    public int score;
    public int cost;
}
