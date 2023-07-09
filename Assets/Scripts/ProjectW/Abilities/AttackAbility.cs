using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AttackAbility : Ability
{
    public GameObject pivot;
    public float AttackDuration = 1f;
    public int Damage = 10;
    public float Length = 5f;
    
    protected override void DoAbility()
    {
        float startLength = transform.localScale.z;
        
        Sequence attack = DOTween.Sequence();
        Tweener scaleUp = pivot.transform.DOScaleZ(Length,AttackDuration/2f).SetEase(Ease.OutExpo);
        Tweener scaleDown = pivot.transform.DOScaleZ(startLength,AttackDuration/2f).SetEase(Ease.InExpo);
        attack.Append(scaleUp).Append(scaleDown);
        attack.Play();
        
        CoolDownFade();
    }
}
