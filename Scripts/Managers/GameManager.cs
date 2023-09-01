using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Script References")]
    public SpawnManager spawnManager;


    [HideInInspector]
    public UnityEvent<FoodTypeEnum,CardScript> cardClickEvent;
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
    private Queue<CardBaseScript> curSelectedCards = new Queue<CardBaseScript>();
    [SerializeField]
    private GameDataScriptableObject gameData;
    #endregion

    #region Getter/Setter Values
    public GameDataScriptableObject GetGameData 
    {
        get { return gameData; }
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
        SaveLoadStruct loadStruct = new SaveLoadStruct();
        loadStruct = SaveLoadManager.instance.LoadGame();

        if (loadStruct.hasLoadData == 1) 
        {
            gameData = Resources.Load<GameDataScriptableObject>("ScriptableObjects/GameData/" + loadStruct.gameDataPath);
        }

        spawnManager.CardsSpawnFromLastState(loadStruct);
    }

    void UpdateCardClickState(FoodTypeEnum selectedFoodType , CardScript curCard)
    {
        curSelectedCards.Enqueue(curCard);

        //Creating switch so that in Future if we want to add 3 or more turn based card matching game then it will be easier for us
        switch (curSelectionState)
        {
            case CardSelectionEnum.E_PrimaryCard:
                curSelectionState = CardSelectionEnum.E_SecondaryCard;
                SetNewFoodType(selectedFoodType);
                break;
            case CardSelectionEnum.E_SecondaryCard:
                curSelectionState = CardSelectionEnum.E_PrimaryCard;
                StartCoroutine(OnSecondaryClick(selectedFoodType , curSelectionFoodType));
                break;
        }
    }

    void SetNewFoodType(FoodTypeEnum selectedFoodType) 
    {
        curSelectionFoodType = selectedFoodType;
    }

    IEnumerator OnSecondaryClick(FoodTypeEnum selectedFoodType , FoodTypeEnum foodTypeToCompare) 
    {
        yield return new WaitForSeconds(1f);

        //Checking if food type matches
        if (foodTypeToCompare == selectedFoodType)
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
    }

    void OnCardsMatch()
    {
        cardsMatchEvent.Invoke();

        //Remove top Currently Selected Cards from queue
        curSelectedCards.Dequeue().OnCardRemove();
        curSelectedCards.Dequeue().OnCardRemove();
    }

    void OnCardMisMatch() 
    {
        cardsMisMatchEvent.Invoke();

        //Remove top Currently Selected Cards from queue
        curSelectedCards.Dequeue().OnCardReset();
        curSelectedCards.Dequeue().OnCardReset();
    }
}
