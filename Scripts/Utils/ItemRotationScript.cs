using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemRotationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 45), 8f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
    }
}
