using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverRootCanvas : MonoBehaviour
{
    public TextMeshProUGUI titleTxt;
    public Button retryBtn;
    public Button menuBtn;

    private void Start()
    {
        retryBtn.onClick.AddListener(OnRetry);
        menuBtn.onClick.AddListener(OnMenu);

        GameManager.instance.gameWinEvent.AddListener(OnGameWon);
        GameManager.instance.gameLoseEvent.AddListener(OnGameLose);
    }

    void OnGameWon() 
    {
        titleTxt.text = "YOU WON!";
        ToggleGameOverCanvas(true);
    }

    void OnGameLose() 
    {
        titleTxt.text = "YOU LOSE!";
        ToggleGameOverCanvas(true);
    }

    void OnRetry() 
    {
        LevelManager.instance.ReloadScene();
    }

    void OnMenu() 
    {
        LevelManager.instance.LoadMenu();
    }

    void ToggleGameOverCanvas(bool value) 
    {
        GetComponent<Canvas>().enabled = value;
    }
}
