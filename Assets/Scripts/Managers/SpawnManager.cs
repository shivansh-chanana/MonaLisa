using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public List<CardScriptableObject> cardsToSpawn;
    public Transform spawnParent;

    public void CardsSpawn()
    {
        Vector2 layoutCoordinates = GetLayoutCoordinates();

        //To spawn cards according to layout
        for (int i = 0; i < layoutCoordinates.x; ++i) 
        {
            for (int j = 0; j < layoutCoordinates.y; j++)
            {
                //spawn and update card data
                CardScript card = Instantiate(cardPrefab, spawnParent).GetComponent<CardScript>();
                card.cardData = cardsToSpawn[Random.Range(0,cardsToSpawn.Count-1)];
                card.UpdateFoodType();
            }
        }

        //Getting cell size from GameData
        GridLayoutGroup gridLayout = spawnParent.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = GameManager.instance.gameData[GameManager.instance.GetCurLevel].cellSize;

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = (int)layoutCoordinates.y;

        Invoke(nameof(OnCardsSpawnComplete), 2f);
    }

    void OnCardsSpawnComplete()
    {
        //No need for grid anymore //will interfare with Animations if enabled
        spawnParent.GetComponent<GridLayoutGroup>().enabled = false;
    }

    Vector2 GetLayoutCoordinates() 
    {
        Vector2 layoutCoordinates = new Vector2();

        switch (GameManager.instance.gameData[GameManager.instance.GetCurLevel].layoutType)
        {
            case LayoutType.E_2x2:
                layoutCoordinates = new Vector2(2,2);
                break;
            case LayoutType.E_2x3:
                layoutCoordinates = new Vector2(2, 3);
                break;
            case LayoutType.E_5x6:
                layoutCoordinates = new Vector2(5, 6);
                break;
        }
        return layoutCoordinates;
    }
}
