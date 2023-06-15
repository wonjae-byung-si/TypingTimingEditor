using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHandler : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] RectTransform barTransform;

    public void Lerp(float t)
    {
        Vector2 to = new Vector2((rect.rect.width * t) - rect.rect.width/2 , barTransform.localPosition.y);
        barTransform.localPosition = to;

        Debug.Log(to.x);
    }
}
