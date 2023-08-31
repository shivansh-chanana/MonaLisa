using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameDataScriptableObject> gameData;

    public RenderTexture primaryRendTex;
    public Transform primaryCardRendTexPos;
    public RenderTexture secondaryRendTex;
    public Transform secondaryCardRendTexPos;

    [Space]
    [Header("Script References")]
    public SpawnManager spawnManager;


    [HideInInspector]
    public UnityEvent<FoodTypeEnum,CardBaseScript> cardClickEvent;
    [HideInInspector]
    public UnityEvent cardJourneyCompleteEvent;
    [HideInInspector]
    public UnityEvent cardsMatchEvent;
    [HideInInspector]
    public UnityEvent cardsMisMatchEvent;

    #region Private Variables for Debugging in Editor
    [Header("Debug")]
    private int curLevel = 0;
    [SerializeField]
    private CardSelectionEnum curSelectionState;
    [SerializeField]
    private FoodTypeEnum curSelectionFoodType;
    [SerializeField]
    private List<CardBaseScript> curSelectedCards;
    #endregion

    #region
    public int GetCurLevel 
    {
        get { return curLevel; }
    }
    #endregion

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        cardClickEvent.AddListener(UpdateCardClickState);
    }

    private void OnDisable()
    {
        cardClickEvent.RemoveListener(UpdateCardClickState);
    }

    private void Start()
    {
        spawnManager.CardsSpawn();
    }

    void UpdateCardClickState(FoodTypeEnum selectedFoodType , CardBaseScript curCard)
    {
        curSelectedCards.Add(curCard);

        //Creating switch so that in Future if we want to add 3 or more turn based card matching game then it will be easier for us
        switch (curSelectionState)
        {
            case CardSelectionEnum.E_PrimaryCard:
                curSelectionState = CardSelectionEnum.E_SecondaryCard;
                SetPrimaryCardObj(curCard.cardData.foodItem);
                SetPrimaryCardRenderTex(curCard.renderImg);
                SetNewFoodType(selectedFoodType);
                break;
            case CardSelectionEnum.E_SecondaryCard:
                curSelectionState = CardSelectionEnum.E_PrimaryCard;
                SetSecondaryCardObj(curCard.cardData.foodItem);
                SetSecondaryCardRenderTex(curCard.renderImg);
                OnSecondaryClick(selectedFoodType);
                break;
        }
    }

    void SetPrimaryCardObj(GameObject foodItem) 
    {
        Instantiate(foodItem, primaryCardRendTexPos);
    }

    void SetPrimaryCardRenderTex(RawImage img)
    {
        img.texture = primaryRendTex;
    }


    void SetSecondaryCardObj(GameObject foodItem)
    {
        Instantiate(foodItem, secondaryCardRendTexPos);
    }

    void SetSecondaryCardRenderTex(RawImage img)
    {
        img.texture = secondaryRendTex;
    }

    void SetNewFoodType(FoodTypeEnum selectedFoodType) 
    {
        curSelectionFoodType = selectedFoodType;
    }

    void OnSecondaryClick(FoodTypeEnum selectedFoodType) 
    {
        //Checking if food type matches
        if (curSelectionFoodType == selectedFoodType)
        {
            OnCardsMatch();
        }
        else 
        {
            OnCardMisMatch();
        }

        //Card journey is done, Now reseting everything
        OnCardJourneyComplete();
    }

    void OnCardJourneyComplete() 
    {
        cardJourneyCompleteEvent.Invoke();

        //Clear Currently Selected Cards List
        curSelectedCards.Clear();
        curSelectionFoodType = FoodTypeEnum.E_None;

        //Destory 3D objects //convert to object pooling later
        Destroy(primaryCardRendTexPos.GetChild(0).gameObject);
        Destroy(secondaryCardRendTexPos.GetChild(0).gameObject);
    }

    void OnCardsMatch() 
    {
        cardsMatchEvent.Invoke();

        foreach (var item in curSelectedCards)
        {
            item.OnCardRemove();
        }
    }

    void OnCardMisMatch() 
    {
        foreach (var item in curSelectedCards)
        {
            item.OnCardReset();
        }

        cardsMisMatchEvent.Invoke();
    }
}
