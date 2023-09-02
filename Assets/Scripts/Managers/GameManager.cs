using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Script References")]
    public SpawnManager spawnManager;
    [HideInInspector]
    public ScoreManager scoreManager;

    [HideInInspector]
    public UnityEvent<FoodTypeEnum , CardScript> cardClickEvent;
    [HideInInspector]
    public UnityEvent cardFlipEvent;
    [HideInInspector]
    public UnityEvent cardJourneyCompleteEvent;
    [HideInInspector]
    public UnityEvent cardsMatchEvent;
    [HideInInspector]
    public UnityEvent cardsMisMatchEvent;
    [HideInInspector]
    public UnityEvent gameWinEvent;
    [HideInInspector]
    public UnityEvent gameLoseEvent;


    #region Private Variables for Debugging in Editor
    [Header("Debug")]
    [SerializeField]
    private int totalRemainingCards;
    [SerializeField]
    private GameDataScriptableObject gameData;
    [Space]
    private SaveLoadStruct savedData;
    [Space]
    [SerializeField]
    private CardSelectionEnum curSelectionState;
    [SerializeField]
    private FoodTypeEnum curSelectionFoodType;
    [SerializeField]
    private Queue<CardBaseScript> curSelectedCards = new Queue<CardBaseScript>();
    #endregion

    #region Getter/Setter Values
    public GameDataScriptableObject GetGameData 
    {
        get { return gameData; }
    }
    public int GetRemainingCards 
    {
        get { return totalRemainingCards; }
    }
    #endregion

    public static GameManager instance;
    private void Awake()
    {
        if(instance)
            Destroy(gameObject);
        else 
            instance = this;
    }

    private void OnEnable()
    {
        cardClickEvent.AddListener(UpdateCardClickState);
        spawnManager.OnCardsCreateEvent.AddListener(UpdateTotalRemainingCards);
    }

    private void OnDisable()
    {
        cardClickEvent.RemoveListener(UpdateCardClickState);
        spawnManager.OnCardsCreateEvent.RemoveListener(UpdateTotalRemainingCards);
    }

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        savedData = TryLoadLastGame();

        //If no previous data then add from level manager
        if (savedData.hasLoadData == 0)
        {
            gameData = LevelManager.instance.gameData;
            scoreManager.UpdateTriesEvent.Invoke(GetGameData.totalTries);
        }
       
        //Create cards now
        spawnManager.CardsSpawn(savedData);

        //Add Listeners 
        //Observer pattern
        scoreManager.TriesFinishedEvent.AddListener(OnGameOver);

        //Update Game State to Game Started
        GameStateManager.instance.UpdateGameStateEvent.Invoke(GameState.E_GameStarted);
    }

    //Try to load last saved game if any
    SaveLoadStruct TryLoadLastGame() 
    {
        SaveLoadStruct loadStruct = new SaveLoadStruct();
        loadStruct = SaveLoadManager.instance.LoadGame();

        if (loadStruct.hasLoadData == 1)
        {
            gameData = Resources.Load<GameDataScriptableObject>("ScriptableObjects/GameData/" + loadStruct.gameDataPath);

            //update score, tries and matches
            scoreManager.UpdateMatchEvent.Invoke(loadStruct.prevMatches);
            scoreManager.UpdateTriesEvent.Invoke(loadStruct.remainingTries);
            scoreManager.UpdateScoreEvent.Invoke(loadStruct.currentScore);
        }

        return loadStruct;
    }

    void UpdateTotalRemainingCards(int amount) 
    {
        totalRemainingCards += amount;
        CheckCardsFinished();
    }

    void CheckCardsFinished() 
    {
        if (totalRemainingCards <= 0) 
        {
            GameStateManager.instance.UpdateGameStateEvent.Invoke(GameState.E_GameWon);
            gameWinEvent.Invoke();
            SaveLoadManager.instance.RemoveAllSaveData();
        }
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
        if(totalRemainingCards > 0) scoreManager.UpdateTriesEvent.Invoke(-1);

        //Card Journey Complete , we can save game now
        SaveLoadStruct saveStruct = new SaveLoadStruct();
        saveStruct.hasLoadData = 1;
        saveStruct.remainingCards = totalRemainingCards;
        saveStruct.remainingTries = scoreManager.TriesLeft;
        saveStruct.currentScore = scoreManager.Score;
        saveStruct.prevMatches = scoreManager.Matches;
        saveStruct.gameDataPath = GetGameData.gameDataPath + "/" + GetGameData.name;

        SaveLoadManager.instance.SaveGame(saveStruct);
    }

    void OnCardsMatch()
    {
        cardsMatchEvent.Invoke();
        scoreManager.UpdateMatchEvent.Invoke(1);
        scoreManager.StartComboEvent.Invoke();

        //Remove top Currently Selected Cards from queue 
        //update score from primary card
        CardBaseScript card = curSelectedCards.Dequeue();
        scoreManager.UpdateScoreEvent.Invoke((card.cardData.amount));
        card.OnCardRemove();

        curSelectedCards.Dequeue().OnCardRemove();

        UpdateTotalRemainingCards(-2);
    }

    void OnCardMisMatch() 
    {
        cardsMisMatchEvent.Invoke();

        //Remove top Currently Selected Cards from queue
        curSelectedCards.Dequeue().OnCardReset();
        curSelectedCards.Dequeue().OnCardReset();
    }

    void OnGameOver() 
    {
        //Update GameState to GameOver
        GameStateManager.instance.UpdateGameStateEvent.Invoke(GameState.E_GameLose);
        gameLoseEvent.Invoke();
        SaveLoadManager.instance.RemoveAllSaveData();
        Debug.Log("Game Over");
    }
}
