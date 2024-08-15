using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LetterRotation : MonoBehaviour
{
    public RectTransform rectTransform;
    [SerializeField] private float duration;
    public AnimationCurve animationCurve;
    
    [ContextMenu("rotate")]
    
    public void Rotate()
    {
        rectTransform.DORotate(new Vector3(0,360,0), duration).SetEase(animationCurve).Play();
    }
   
}
