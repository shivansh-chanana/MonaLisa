using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamaData", menuName = "ScriptableObjects/GameDataScriptableObject", order = 1)]
public class GameDataScriptableObject : ScriptableObject
{
    public string levelName;
    public LayoutType layoutType;
    [Space]
    public Vector2 cellSize;
    public Vector2 cellSpacing;
}