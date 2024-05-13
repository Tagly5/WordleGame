using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopTweener : MonoBehaviour
{
    [SerializeField] private bool popOnEnable;
    [SerializeField] private Transform root;
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private Vector3 initialScale;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;
    [SerializeField] private bool useAnimationCurve;
    [SerializeField] private AnimationCurve animationCurve;


    private void Awake()
    {
        popOnEnable = true;
    }

    [ContextMenu("Pop")]
    public void Pop()
    {
        root.localScale = initialScale;
        if (useAnimationCurve)
        {
            root.DOScale(finalScale, duration).SetEase(animationCurve).Play();
        }
        else
        {
            root.DOScale(finalScale, duration).SetEase(ease).Play();

        }
    }

    private void OnEnable()
    {
        if (popOnEnable)
        {
            Pop();
        }
    }
    
}
