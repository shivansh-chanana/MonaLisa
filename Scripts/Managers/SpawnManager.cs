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

        RandomSpawnLogic((int)layoutCoordinates.x * (int)layoutCoordinates.y);

        //Getting cell size from GameData
        GridLayoutGroup gridLayout = spawnParent.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = GameManager.instance.gameData[GameManager.instance.GetCurLevel].cellSize;
        gridLayout.spacing = GameManager.instance.gameData[GameManager.instance.GetCurLevel].cellSpacing;

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = (int)layoutCoordinates.y;

        Invoke(nameof(OnCardsSpawnComplete), 2f);
    }

    public void RandomSpawnLogic(int totalElements)
    {
        //First Flow for creating random items
        List<CardScript> randomItemsFirstFlow = new List<CardScript>();
        for (int i = 0; i < totalElements/2; i++)
        {
            //spawn and update card data
            CardScript card = Instantiate(cardPrefab, spawnParent).GetComponent<CardScript>();
            card.cardData = cardsToSpawn[Random.Range(0, cardsToSpawn.Count - 1)];
            card.UpdateFoodTypeAndBg();

            randomItemsFirstFlow.Add(card);
        }

        //SecondFlow for creating items from previously created firstFlow //So that each item has it's own pair
        for (int i = 0; i < randomItemsFirstFlow.Count;)
        {
            //spawn and update card data
            CardScript card = Instantiate(cardPrefab, spawnParent).GetComponent<CardScript>();
            int randomIndex = Random.Range(0, randomItemsFirstFlow.Count);
            card.cardData = randomItemsFirstFlow[randomIndex].cardData;
            card.UpdateFoodTypeAndBg();

            randomItemsFirstFlow.RemoveAt(randomIndex);
        }

        //Memory leak safety measure
        randomItemsFirstFlow.Clear();
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
