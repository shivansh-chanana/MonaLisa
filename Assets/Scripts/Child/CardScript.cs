/*
 * Card script for managing cards and can be changed to any super card or different kind
 * of card as it inherits from CardBase
 */

#pragma warning disable CS0108

using UnityEngine;
using UnityEngine.UI;

public class CardScript : CardBaseScript
{
    public RawImage renderImg;
    public Image renderBg;
    public Camera myRendCamera;

    GameObject foodItem;
    RenderTexture myRendTex;

    private void Start()
    {
        base.Start();

        myRendTex = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        myRendTex.anisoLevel = 2;

        myRendCamera.targetTexture = myRendTex;
        renderImg.texture = myRendTex;

        myRendCamera.gameObject.SetActive(false);

        foodItem = Instantiate(cardData.foodItem, myRendCamera.transform);
        foodItem.transform.position += new Vector3(0,0f,6);
        foodItem.transform.localScale = Vector3.one * 1f;
        foodItem.transform.eulerAngles += new Vector3(30, 0, 0);
    }

    public void UpdateFoodTypeAndBg()
    {
        myFoodType = cardData.foodType;
        renderBg.color = cardData.bgColor;
    }

    public override void OnCardClick()
    {
        base.OnCardClick();
        EnableRenderItems();

        GameManager.instance.cardClickEvent.Invoke(myFoodType, this);
        GameManager.instance.cardFlipEvent.Invoke();
    }

    void EnableRenderItems() 
    {
        myRendCamera.gameObject.SetActive(true);
    }

}
