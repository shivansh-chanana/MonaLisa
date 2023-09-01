using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : FoodBaseScript
{
    void Start()
    {
        gameObject.AddComponent<ItemRotationScript>();
    }
}
