using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class LetterMove : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve animationCurve;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        Vector2 finalPos = this.transform.localPosition;
        root.DOLocalMove(finalPos + offset, duration).SetEase(animationCurve).Play();
    }
}
