#pragma warning disable CS0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : CardBaseScript
{
    public CardScriptableObject cardData;

    public void UpdateFoodType() 
    {
        myFoodType = cardData.foodType;
    }

}
