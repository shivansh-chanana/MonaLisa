using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public struct SaveLoadStruct 
{
    public int hasLoadData;
    public int remainingTiles;
    public int currentScore;
    public int remainingTurns;
    public string gameDataPath;
}

public class Structures : MonoBehaviour
{
}
