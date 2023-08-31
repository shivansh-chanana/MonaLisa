using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBaseScript : MonoBehaviour, IPointerClickHandler
{
    public FoodTypeEnum myFoodType;

    bool isAlreadyClicked = false;

    protected void Start()
    {
        GameManager.instance.cardJourneyCompleteEvent.AddListener(OnCardReset);
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

        GameManager.instance.cardClickEvent.Invoke(myFoodType , this);
    }

    void OnCardAlreadyClicked() 
    {

    }

    void OnCardReset() 
    {
        Debug.Log("On Card Reset");
        isAlreadyClicked = false;
    }

    public void OnCardRemove() 
    {
        gameObject.SetActive(false);
    }
}
