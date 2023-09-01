using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI triesLeftText;
    public TextMeshProUGUI matchesText;
    public TextMeshProUGUI scoreText;

    public Image comboBarFill;

    private int triesLeft = 0;
    private int matches = 0;
    private int score = 0;

    #region Getter/Setter

    public int TriesLeft 
    {
        get { return triesLeft; }
        set { triesLeft = value; }
    }

    public int Matches
    {
        get { return matches; }
        set { matches = value; }
    }

    public int Score 
    {
        get { return score; }
        set { score = value; }
    }

    #endregion

    [HideInInspector]
    public UnityEvent TriesFinishedEvent;
    [HideInInspector]
    public UnityEvent<int> UpdateMatchEvent;
    [HideInInspector]
    public UnityEvent<int> UpdateTriesEvent;
    [HideInInspector]
    public UnityEvent<int> UpdateScoreEvent;

    private void Start()
    {
        UpdateMatchEvent.AddListener(UpdateMatches);
        UpdateTriesEvent.AddListener(UpdateTries);
        UpdateScoreEvent.AddListener(UpdateScore);

        SetDefaulTextValues();
    }

    void SetDefaulTextValues() 
    {
        matchesText.text = matches.ToString();
        triesLeftText.text = triesLeft.ToString();
        scoreText.text = score.ToString();
    }

    void UpdateMatches(int amount)
    {
        matches +=amount;
        matchesText.text = matches.ToString();
    }

    void UpdateTries(int amount) 
    {
        triesLeft += amount;
        triesLeftText.text = triesLeft.ToString();

        if (triesLeft <= 0) 
        {
            TriesFinishedEvent.Invoke();
        }
    }

    void UpdateScore(int amount) 
    {
        int prevScore = score;
        score += amount;

        DOTween.To(() => prevScore, x => prevScore = x, score, 0.5f).OnUpdate(() =>
            {
                scoreText.text = prevScore.ToString();
            });
    }
}
