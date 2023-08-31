using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBaseScript : MonoBehaviour, IPointerClickHandler
{
    public CardScriptableObject cardData;
    public FoodTypeEnum myFoodType;
    public RawImage renderImg;

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
        OnCardClick();
        Debug.Log("Card Clicked");
    }

    void OnCardClick() 
    {
        if (!isAlreadyClicked)
            isAlreadyClicked = true;
        else
        {
            OnCardAlreadyClicked();
            return;
        }

        anim.Play(animShowStateName);
    }

    void OnCardAlreadyClicked() 
    {

    }

    public void OnCardReset() 
    {
        Debug.Log("On Card Reset");
        isAlreadyClicked = false;
        anim.Play(animHideStateName);
    }

    public void OnCardRemove() 
    {
        anim.Play(animMatchedStateName);
    }

    public void OnRevealAnimationDone()
    {
        GameManager.instance.cardClickEvent.Invoke(myFoodType, this);
    }

    public void OnMatchedAnimationDone() 
    {
        gameObject.SetActive(false);
    }
}
