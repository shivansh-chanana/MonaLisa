using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]
    GameState curGameState;

    public GameState GameState 
    { 
        get { return curGameState; }
    }

    [HideInInspector]
    public UnityEvent<GameState> UpdateGameStateEvent;

    public static GameStateManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(gameObject);
    }

    public void OnEnable()
    {
        UpdateGameStateEvent.AddListener(UpdateGameState);
    }

    public void OnDisable() 
    {
        UpdateGameStateEvent.RemoveListener(UpdateGameState);
    }

    void UpdateGameState(GameState newGameState) 
    {
        curGameState = newGameState;
    }
}
