using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardScriptableObject", order = 1)]
public class CardScriptableObject : ScriptableObject
{
    public GameObject foodItem;
    public FoodTypeEnum foodType;
    public float amount = 10;
    public Color bgColor;
}
