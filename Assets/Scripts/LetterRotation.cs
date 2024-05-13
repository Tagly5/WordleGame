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
        rectTransform.DORotate(Vector3.zero, duration).SetEase(animationCurve).Play();
    }
   
}
