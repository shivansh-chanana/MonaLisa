using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI triesLeftText;
    public TextMeshProUGUI matchesText;
    public TextMeshProUGUI scoreText;

    private int triesLeft;
    private int matches;
    private int scoreLeft;

    private void Start()
    {
        UpdateMatches();

        GameManager.instance.cardsMatchEvent.AddListener(AddNewMatch);
    }

    void AddNewMatch() 
    {
        matches++;
        UpdateMatches();
    }

    void UpdateMatches() 
    {
        DOTween.To(() => matches, x => matches = x, 52, 1).OnUpdate(() => 
        {
            matchesText.text = matches.ToString();
        });
    }
}
