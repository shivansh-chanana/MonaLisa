using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    string hasLoadData = "HasLoadData";
    string remainingCards = "RemainingCards";
    string currentScore = "CurrentScore";
    string remainingTurns = "RemainingTurns";
    string lastGameData = "LastGameData";

    public static SaveLoadManager instance;

    void Awake()
    {
        if(instance)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame(SaveLoadStruct curSaveLoadState) 
    {
        PlayerPrefs.SetInt(hasLoadData, curSaveLoadState.hasLoadData);
        PlayerPrefs.SetInt(remainingCards, curSaveLoadState.remainingCards);
        PlayerPrefs.SetInt(currentScore, curSaveLoadState.currentScore);
        PlayerPrefs.SetInt(remainingTurns, curSaveLoadState.remainingTurns);

        PlayerPrefs.SetString(lastGameData, curSaveLoadState.gameDataPath);

        Debug.Log("Game Saved with values | " + lastGameData + " : " + curSaveLoadState.gameDataPath);
    }

    public SaveLoadStruct LoadGame() 
    {
        SaveLoadStruct currentLoadState = new SaveLoadStruct();
        
        currentLoadState.hasLoadData = PlayerPrefs.GetInt(hasLoadData, 0);
        currentLoadState.remainingCards = PlayerPrefs.GetInt(remainingCards, 0);
        currentLoadState.currentScore = PlayerPrefs.GetInt(currentScore, 0);
        currentLoadState.remainingTurns = PlayerPrefs.GetInt(remainingTurns, 0);
        currentLoadState.gameDataPath = PlayerPrefs.GetString(lastGameData, "Easy 2x2/Easy_1");

        return currentLoadState;
    }
}
