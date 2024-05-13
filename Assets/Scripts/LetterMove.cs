using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class LetterMove : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Vector2 finalPosition;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve animationCurve;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        root.DOLocalMove(finalPosition, duration).SetEase(animationCurve).Play();
    }
}
