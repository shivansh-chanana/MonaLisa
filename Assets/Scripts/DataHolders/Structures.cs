using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public struct SaveLoadStruct 
{
    public int hasLoadData;
    public int remainingCards;
    public int currentScore;
    public int remainingTries;
    public int prevMatches;
    public string gameDataPath;
}

public class Structures : MonoBehaviour
{
}
