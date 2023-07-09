using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public Outline AOutline;
    protected Color outlineColor;
    public float CoolDown = 2f;
    protected bool OnCoolDown = false;
    private float currentTime = 0f;
    
    private void Awake()
    {
        outlineColor = AOutline.OutlineColor;
    }

    private void Start()
    {
        Highlight(false);
    }

    public void Highlight(bool state)
    {
        AOutline.enabled = state;
    }

    public void StartAbility()
    {
        if (OnCoolDown)
            return;
        
        OnCoolDown = true;
        DoAbility();
    }

    protected virtual void DoAbility()
    {
    }

    protected void CoolDownFade()
    {
        Tweener colorCooldown = DOVirtual.Color(Color.red, outlineColor, CoolDown, (value) =>
        {
            AOutline.OutlineColor = value;
        });
        colorCooldown.Play();
    }
    
    private void Update()
    {
        if (OnCoolDown)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= CoolDown)
            {
                currentTime -= CoolDown;
                OnCoolDown = false;
            }
        }
    }
}
