using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfo : MonoBehaviour
{
    public PowderUp ThisPowderUp;
    public Text NameText;
    public Text BonusText;
    // Start is called before the first frame update
    void Start()
    {
        NameText.text= ThisPowderUp.name;
        BonusText.text = ThisPowderUp.bonus.ToString();
        
    }

}
