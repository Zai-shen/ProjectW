using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagment : MonoBehaviour
{
    public string _buttonName= "DMG";

    private int _currentBread;

    private int _level1Price= 100;
    private int _level2Price= 150;
    private int _level3Price= 200;

    private int _currentPrice= 100;

    private int _maxLevel = 3;

    private int _currentMetaLevel;
    private int _nextBonus=0;


    public Text PowerTitle;
    public Text CostNLevel; 
    public Text BonusAmount; 



    public Button ButtonMetaDMGLevel;    

    public string LevelText = "LVL";
    public string MaxedLeveled = "Maxed Out";
    private int tmpint;
    private MetaShop _ms;


    void Awake()
    {
        _currentBread = PlayerPrefs.GetInt("BakedBread");

        _currentMetaLevel=PlayerPrefs.GetInt(_buttonName);


        Button thisButton = GetComponent<Button>();

        switch(_currentMetaLevel) 
        {
            case 0:
                _currentPrice = _level1Price;
                break;
            case 1:
                _currentPrice = _level2Price;
                break;
            case 2:
                _currentPrice = _level3Price;
                break;
            case 3:
                _currentPrice = 1000000;
                break;
            default:
                // code block
                break;
        }

        tmpint= _currentMetaLevel +1;
        PowerTitle.text = _buttonName;

        if (_currentMetaLevel>= _maxLevel){
            CostNLevel.text = MaxedLeveled;
            BonusAmount.text = "None";

        } else {

            CostNLevel.text = _currentPrice.ToString()+ " Bread for "+LevelText + tmpint.ToString();
            BonusAmount.text = _nextBonus.ToString() + " % Bonus";
        }


        



        if ((_currentBread < _currentPrice) |( _currentMetaLevel>= _maxLevel)){
            thisButton.enabled = false;
        } else {
            thisButton.enabled = true;
        }


        

    }    

    void metaLevelUp () {
        // reduce breadcount and save to player pref
        _currentBread-= _currentPrice;
        PlayerPrefs.SetInt("BakedBread",_currentBread);

        // level up Metalevel and save to playerpref
        _currentMetaLevel +=1;
        PlayerPrefs.SetInt(_buttonName,_currentMetaLevel);

        // increase current price to next level
                switch(_currentMetaLevel) 
        {
            case 0:
                _currentPrice = _level1Price;
                break;
            case 1:
                _currentPrice = _level2Price;
                break;
            case 2:
                _currentPrice = _level3Price;
                break;
            case 3:
                _currentPrice = 1000000;
                break;
            default:
                // code block
                break;
        }
        PlayerPrefs.Save();

        checkAffordable();







        // for all buttons check if price is still high enough

    }


    public void CheckCash(){
        Button thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(metaLevelUp);

    }

    public void checkAffordable(){
        Button thisButton = GetComponent<Button>();

        if ((_currentBread < _currentPrice) |( _currentMetaLevel>= _maxLevel)){
            thisButton.enabled = false;
        } else {
            thisButton.enabled = true;
        }
    }

    public void registerPanel(MetaShop ms){
        _ms= ms;
    }







    



}
