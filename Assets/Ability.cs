using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public Outline AOutline;

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
        DoAbility();
    }

    protected virtual void DoAbility()
    {
    }

}
