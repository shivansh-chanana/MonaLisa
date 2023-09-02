/*
 * Script for saving and loading game data for resume functionality
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    string hasLoadData = "HasLoadData";
    string remainingCards = "RemainingCards";
    string currentScore = "CurrentScore";
    string remainingTries = "RemainingTries";
    string prevMatches = "PrevMatches";
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
        PlayerPrefs.SetInt(remainingTries, curSaveLoadState.remainingTries);
        PlayerPrefs.SetInt(prevMatches,curSaveLoadState.prevMatches);
        PlayerPrefs.SetString(lastGameData, curSaveLoadState.gameDataPath);
    }

    public SaveLoadStruct LoadGame() 
    {
        SaveLoadStruct currentLoadState = new SaveLoadStruct();
        
        currentLoadState.hasLoadData = PlayerPrefs.GetInt(hasLoadData, 0);
        currentLoadState.remainingCards = PlayerPrefs.GetInt(remainingCards, 0);
        currentLoadState.currentScore = PlayerPrefs.GetInt(currentScore, 0);
        currentLoadState.remainingTries = PlayerPrefs.GetInt(remainingTries, 0);
        currentLoadState.prevMatches = PlayerPrefs.GetInt(prevMatches, 0);
        currentLoadState.gameDataPath = PlayerPrefs.GetString(lastGameData, "Easy 2x2/Easy_1");

        return currentLoadState;
    }

    public void RemoveAllSaveData()
    {
        PlayerPrefs.DeleteAll();
    }

}
