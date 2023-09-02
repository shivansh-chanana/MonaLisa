using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayRootCanvas : MonoBehaviour
{
    public Button backToMenuBtn;
    public Transform cardContainer;

    private void Start()
    {
        backToMenuBtn.onClick.AddListener(BackToMenu);
    }

    void BackToMenu() 
    {
        LevelManager.instance.LoadMenu();
    }
}
