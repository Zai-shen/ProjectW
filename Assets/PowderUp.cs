using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowderUp", menuName = "PowderUp")]
public class PowderUp : ScriptableObject
{
    public new string name;
    public int bonus;
}
