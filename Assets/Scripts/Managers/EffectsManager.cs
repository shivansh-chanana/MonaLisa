/*
 * Handles Effects of UI
 */

using UnityEngine;
using DG.Tweening;

public class EffectsManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.cardsMisMatchEvent.AddListener(ShakeComboBar);
        GameManager.instance.cardsMatchEvent.AddListener(ScaleComboBar);
    }

    public void ShakeComboBar() 
    {
        GameManager.instance.scoreManager.comboBarParent.DOShakePosition(1f,8f);
    }

    public void ScaleComboBar() 
    {
        GameManager.instance.scoreManager.comboBarFill.transform.DOPunchScale(Vector3.one * 0.1f,2f);
    }
}
