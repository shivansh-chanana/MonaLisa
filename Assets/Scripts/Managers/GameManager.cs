using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

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
    [SerializeField]
    private CardSelectionEnum curSelectionState;
    [SerializeField]
    private FoodTypeEnum curSelectionFoodType;
    [SerializeField]
    private List<CardBaseScript> curSelectedCards;
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

    void UpdateCardClickState(FoodTypeEnum selectedFoodType , CardBaseScript curCard)
    {
        curSelectedCards.Add(curCard);

        //Creating switch so that in Future if we want to add 3 or more turn based card matching game then it will be easier for us
        switch (curSelectionState)
        {
            case CardSelectionEnum.E_PrimaryCard:
                curSelectionState = CardSelectionEnum.E_SecondaryCard;
                SetNewFoodType(selectedFoodType);
                break;
            case CardSelectionEnum.E_SecondaryCard:
                curSelectionState = CardSelectionEnum.E_PrimaryCard;
                OnSecondaryClick(selectedFoodType);
                break;
        }
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
        curSelectionFoodType = FoodTypeEnum.none;
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
