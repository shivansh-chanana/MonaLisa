/*
 * Handles Level Loading for all the scenes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameDataScriptableObject gameData;

    public static LevelManager instance;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        Invoke(nameof(LoadMenu),2f);
    }

    public void LoadMenu() 
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        if (current == SceneManager.GetSceneByName("Menu"))
        {
            LoadGameLevel();
        }
    }

    void LoadGameLevel() 
    {
        SceneManager.LoadScene("Level_Food", LoadSceneMode.Additive);
    }

    public void ReloadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SaveLoadManager.instance.RemoveAllSaveData();
    }
}
