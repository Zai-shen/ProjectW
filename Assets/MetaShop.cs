using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetaShop : MonoBehaviour
{
    public int currentBread;
    public Text BreadScoreText;
    void Awake()
    {
        currentBread = PlayerPrefs.GetInt("BakedBread");
        BreadScoreText.text = "You Got " + currentBread.ToString() + " Pieces of Bread";
    }    

}
