using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetaShop : MonoBehaviour
{
    private int _currentBread;
    public Text BreadScoreText;

    public string LevelText = "LVL: ";
    public string MaxedLeveled = "Maxed Out";
    private int tmpint;

    List<Button> ButtonList = new List<Button>();

    public Button ButtonMetaDMGLevel;    


    void Awake()
    {
        // add all buttons on this page
        ButtonList.Add(ButtonMetaDMGLevel);

        MetaShop ms = GetComponent<MetaShop>();
        
        foreach (Button bt in ButtonList){
            LevelManagment lvMg=bt.GetComponent<LevelManagment>();
            lvMg.registerPanel(ms);
        }
        _currentBread = PlayerPrefs.GetInt("BakedBread");
        BreadScoreText.text = "You Got \n" + _currentBread.ToString() + "\n Pieces of Bread";
    }    


    public void updateButtons(){
        _currentBread = PlayerPrefs.GetInt("BakedBread");
        BreadScoreText.text = "You Got " + _currentBread.ToString() + " Pieces of Bread";

        foreach (Button bt in ButtonList){
            LevelManagment lvMg=bt.GetComponent<LevelManagment>();
            lvMg.checkAffordable();
        }
    }





    



}
