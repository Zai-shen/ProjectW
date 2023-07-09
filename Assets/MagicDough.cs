using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilities
{
    ATTACK = 1,
    SUCKER = 2,
    SHIELD = 3,
    ATTACKTWO = 4
}

public class MagicDough : MonoBehaviour
{
    public Ability Attack;
    public Ability Sucker;
    public Ability Shield;

    private Abilities _currentSelectedAbility = Abilities.ATTACK;
    private Dictionary<Abilities, Ability> _abilities = new Dictionary<Abilities, Ability>();

    private Camera m_camera;

    private void Start()
    {
        m_camera = Camera.main;
        _abilities.Add(Abilities.ATTACK, (Ability)Attack);
        _abilities.Add(Abilities.SUCKER, (Ability)Sucker);
        _abilities.Add(Abilities.SHIELD, (Ability)Shield);
        // _abilities.Add(Abilities.ATTACKTWO, A);
        
        Select(1);
    }

    
    private void Update()
    {
        LookAtMouse();
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Select(Abilities.ATTACK);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Select(Abilities.SUCKER);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Select(Abilities.SHIELD);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Select(Abilities.ATTACKTWO);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Doing selected thing");
            _abilities[_currentSelectedAbility].StartAbility();
        }
    }

    private void Select(int i)
    {
        Select((Abilities) i);
    }

    private void Select(Abilities ability)
    {
        _currentSelectedAbility = ability;
        HighlightCurrentAbility();
    }

    private void HighlightCurrentAbility()
    {
        foreach (Ability abi in _abilities.Values)
        {
            abi.Highlight(false);
        }

        _abilities[_currentSelectedAbility].Highlight(true);
    }

    private void LookAtMouse()
    {
        Vector3 lookAtPos = Input.mousePosition;
        lookAtPos.z = m_camera.transform.position.y - transform.position.y;
        lookAtPos = m_camera.ScreenToWorldPoint(lookAtPos);
        transform.forward = lookAtPos - transform.position;

        switch (_currentSelectedAbility)
        {
            case Abilities.ATTACK:
                transform.Rotate(Vector3.up, 90f);
                break;
            case Abilities.SUCKER:
                break;
            case Abilities.SHIELD:
                transform.Rotate(Vector3.up, -90f);
                break;
            case Abilities.ATTACKTWO:
                transform.Rotate(Vector3.up, 180f);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
    }
}
