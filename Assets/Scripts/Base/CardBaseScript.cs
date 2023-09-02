/*
 * Card Base script is for Handling basics of every card,
 * We can create child scripts and modify or use the existing code
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBaseScript : MonoBehaviour, IPointerClickHandler
{
    public CardScriptableObject cardData;
    public FoodTypeEnum myFoodType;

    Animator anim;
    float animSpeed = 2.5f;
    string animShowStateName = "FlipRevelAnimation";
    string animHideStateName = "FlipHideAnimation";
    string animMatchedStateName = "MatchedAnimation";

    bool isAlreadyClicked = false;

    protected void Start()
    {
        anim= GetComponent<Animator>();
        anim.speed = animSpeed;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isAlreadyClicked)
            isAlreadyClicked = true;
        else
        {
            OnCardAlreadyClicked();
            return;
        }

        OnCardClick();
    }

    public virtual void OnCardClick() 
    {
        PlayAnimation(animShowStateName);
    }

    void OnCardAlreadyClicked() 
    {
        //If we want to unturn the selected card
    }

    public void OnCardReset() 
    {
        isAlreadyClicked = false;
        PlayAnimation(animHideStateName);
    }

    public void OnCardRemove() 
    {
        PlayAnimation(animMatchedStateName);
    }

    public void OnRevealAnimationDone()
    {
        //If we want to do something after reveal animation done
    }

    public void OnMatchedAnimationDone() 
    {
        enabled = false;
        gameObject.SetActive(false);
    }

    void PlayAnimation(string animationState) 
    {
        anim.Play(animationState);
    }
}
