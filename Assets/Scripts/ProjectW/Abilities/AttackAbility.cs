using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : Ability
{
    public GameObject pivot;
    public float CoolDown = 2f;
    public float Damage = 10f;
    public float Length = 5f;
    
    protected override void DoAbility()
    {
        Debug.Log("Iam aattacking");
        // Do extend StartCoroutine(Extend(Length));
    }
}
