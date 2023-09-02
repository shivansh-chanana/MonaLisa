/*
 * This script is for managing everything on menu scene
 * and UI of menu as well
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenuManager : MonoBehaviour
{
    public Button continueBtn;
    [Space]
    public Button easyLevelBtn;
    public GameDataScriptableObject easyData;
    public Button mediumLevelBtn;
    public GameDataScriptableObject mediumData;
    public Button hardLevelBtn;
    public GameDataScriptableObject hardData;

    SaveLoadStruct saveLoadStruct;

    public void OnEnable()
    {
        continueBtn.onClick.AddListener(OnContinueBtnClick);
        easyLevelBtn.onClick.AddListener(LoadEasyLevel);
        mediumLevelBtn.onClick.AddListener(LoadMediumLevel);
        hardLevelBtn.onClick.AddListener(LoadHardLevel);
    }

    private void OnDisable()
    {
        continueBtn.onClick.RemoveListener(OnContinueBtnClick);
        easyLevelBtn.onClick.RemoveListener(LoadEasyLevel);
        mediumLevelBtn.onClick.RemoveListener(LoadMediumLevel);
        hardLevelBtn.onClick.RemoveListener(LoadHardLevel);
    }

    private void Start()
    {
        saveLoadStruct = SaveLoadManager.instance.LoadGame();

        if (saveLoadStruct.hasLoadData == 1 && !string.IsNullOrEmpty(saveLoadStruct.gameDataPath))
        {
            continueBtn.gameObject.SetActive(true);
        }
        else 
        {
            continueBtn.gameObject.SetActive(false);
        }
    }

    void OnContinueBtnClick() 
    {
        LevelManager.instance.gameData = Resources.Load<GameDataScriptableObject>
            ("ScriptableObjects/GameData/" + saveLoadStruct.gameDataPath);

        LoadBaseLevel();
    }

    void LoadBaseLevel()
    {
        SceneManager.LoadScene("GameplayBase");
    }

    void LoadEasyLevel() 
    {
        LevelManager.instance.gameData = easyData;
        SaveLoadManager.instance.RemoveAllSaveData();
        LoadBaseLevel();
    }

    void LoadMediumLevel()
    {
        LevelManager.instance.gameData = mediumData;
        SaveLoadManager.instance.RemoveAllSaveData();
        LoadBaseLevel();
    }

    void LoadHardLevel()
    {
        LevelManager.instance.gameData = hardData;
        SaveLoadManager.instance.RemoveAllSaveData();
        LoadBaseLevel();
    }
}
