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
    public Button ButtonMetaATKSpeedLevel;  
    public Button ButtonMetaKnockBackLevel;  
    public Button ButtonMetaHPLevel;  
    public Button ButtonMetaSuckerRadiusLevel;  
    public Button ButtonMetaXPGainSpeedLevel;  


    void Awake()
    {
        // add all buttons on this page
        ButtonList.Add(ButtonMetaDMGLevel);
        ButtonList.Add(ButtonMetaATKSpeedLevel);
        ButtonList.Add(ButtonMetaKnockBackLevel);
        ButtonList.Add(ButtonMetaHPLevel);
        ButtonList.Add(ButtonMetaSuckerRadiusLevel);
        ButtonList.Add(ButtonMetaXPGainSpeedLevel);


        Debug.Log("Before get com metashop");

        MetaShop ms = GetComponent<MetaShop>();
        
        foreach (Button bt in ButtonList){
            LevelManagment lvMg=bt.GetComponent<LevelManagment>();
            lvMg.registerPanel(ms);
            Debug.Log("in button lister");
        }
        _currentBread = PlayerPrefs.GetInt("BakedBread");
        BreadScoreText.text = "You Got \n" + _currentBread.ToString() + "\n Pieces of Bread";


        tmpint= PlayerPrefs.GetInt("BakedBread")+1000;
        PlayerPrefs.SetInt("BakedBread",tmpint);
    }    


    public void updateButtons(){
        _currentBread = PlayerPrefs.GetInt("BakedBread");
        BreadScoreText.text = "You Got \n" + _currentBread.ToString() + "\n Pieces of Bread";

        foreach (Button bt in ButtonList){
            LevelManagment lvMg=bt.GetComponent<LevelManagment>();
            lvMg.checkAffordable();
        }
    }





    



}
