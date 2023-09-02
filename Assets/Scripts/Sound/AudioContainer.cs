using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioContainer : MonoBehaviour
{
    public AudioClip misMatchAudioClip;
    public AudioClip matchAudioClip;
    public AudioClip flippingAudioClip;
    public AudioClip gameOverAudioClip;

    private void Start()
    {
        GameManager.instance.cardFlipEvent.AddListener(PlayCardsFlipSound);
        GameManager.instance.cardsMatchEvent.AddListener(PlayCardsMatchSound);
        GameManager.instance.cardsMisMatchEvent.AddListener(PlayCardsMisMatchSound);
        GameManager.instance.gameWinEvent.AddListener(PlayGameOverSound);
        GameManager.instance.gameLoseEvent.AddListener(PlayGameOverSound);
    }

    void PlayCardsFlipSound()
    {
        SoundManager.PlaySound(flippingAudioClip);
    }

    void PlayCardsMatchSound() 
    {
        SoundManager.PlaySound(matchAudioClip);
    }

    void PlayCardsMisMatchSound() 
    {
        SoundManager.PlaySound(misMatchAudioClip);
    }

    void PlayGameOverSound() 
    {
        SoundManager.PlaySound(gameOverAudioClip);
    }
}
