/*
 * It's single responsibility is to rotate gameobject it has been attached to
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemRotationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0,0,30); 
        transform.DOLocalRotate(new Vector3(0, 360, 0), 8f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1,LoopType.Incremental);
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
